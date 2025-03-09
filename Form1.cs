using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using iText.Bouncycastle.X509;
using Microsoft.VisualBasic;
using System.Linq;
using Org.BouncyCastle.Asn1;

namespace WindowsFormsPotpis
{
    public partial class Form1 : Form
    {
        private string? selectedPdfFile = null;
        private string? selectedPkcs11Lib = @"C:\Program Files\MUP RS\Celik\netsetpkcs11_x64.dll";

        public Form1()
        {
            InitializeComponent();
        }

        private void ShowCertificateDetails(ISession session, IObjectHandle certObject)
        {
            try
            {
                byte[] certBytes = session.GetAttributeValue(certObject, new List<CKA> { CKA.CKA_VALUE })[0].GetValueAsByteArray();
                X509CertificateParser parser = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate cert = parser.ReadCertificate(certBytes);

                string subject = cert.SubjectDN.ToString();
                string issuer = cert.IssuerDN.ToString();
                string serial = cert.SerialNumber.ToString(16).ToUpper();
                string ekuInfo = "Nepoznato (Nema EKU)";

                // Provera Extended Key Usage
                if (cert.GetExtendedKeyUsage() != null)
                {
                    if (cert.GetExtendedKeyUsage().Contains(new DerObjectIdentifier("1.3.6.1.5.5.7.3.2")))
                    {
                        ekuInfo = "AUTH";
                    }
                    if (cert.GetExtendedKeyUsage().Contains(new DerObjectIdentifier("1.3.6.1.5.5.7.3.3")))
                    {
                        ekuInfo = "SIGN";
                    }
                }

                string details = $"📜 Sertifikat:\n" +
                                 $"👤 Subject: {subject}\n" +
                                 $"🏛️ Issuer: {issuer}\n" +
                                 $"🔢 Serijski broj: {serial}\n" +
                                 $"🔑 EKU: {ekuInfo}\n";

                MessageBox.Show(details, "Detalji sertifikata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Greška pri učitavanju detalja sertifikata: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbPkcs11Libs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPkcs11Libs.SelectedItem == null)
                return;

            try
            {
                using (IPkcs11Library pkcs11Library = new Pkcs11InteropFactories().Pkcs11LibraryFactory.LoadPkcs11Library(
                    new Pkcs11InteropFactories(), selectedPkcs11Lib, AppType.MultiThreaded))
                {
                    List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent);
                    if (slots.Count == 0)
                    {
                        txtLog.AppendText("❌ Nema dostupnih smart kartica.\n");
                        return;
                    }

                    using (ISession session = slots[0].OpenSession(SessionType.ReadWrite))
                    {
                        session.Login(CKU.CKU_USER, PromptForPIN());

                        var certificateObjects = session.FindAllObjects(new List<IObjectAttribute>
                        {
                            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
                        });

                        int selectedIndex = cbPkcs11Libs.SelectedIndex;
                        if (selectedIndex >= 0 && selectedIndex < certificateObjects.Count)
                        {
                            ShowCertificateDetails(session, certificateObjects[selectedIndex]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Greška pri prikazu sertifikata: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
                Title = "Odaberi PDF fajl za potpisivanje"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedPdfFile = openFileDialog.FileName;
                txtSelectedFile.Text = selectedPdfFile;
            }
        }

        private void Sign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedPdfFile))
            {
                txtLog.AppendText("❌ Morate izabrati PDF fajl!\n");
                return;
            }

            string pin = PromptForPIN();
            if (string.IsNullOrEmpty(pin))
            {
                txtLog.AppendText("❌ PIN nije unesen.\n");
                return;
            }

            try
            {
                using (IPkcs11Library pkcs11Library = new Pkcs11InteropFactories().Pkcs11LibraryFactory.LoadPkcs11Library(
                    new Pkcs11InteropFactories(), selectedPkcs11Lib, AppType.MultiThreaded))
                {
                    List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent);
                    if (slots.Count == 0)
                    {
                        txtLog.AppendText("❌ Nema dostupnih smart kartica.\n");
                        return;
                    }

                    txtLog.AppendText($"✅ Smart kartica pronađena u čitaču.\n");

                    using (ISession session = slots[0].OpenSession(SessionType.ReadWrite))
                    {
                        session.Login(CKU.CKU_USER, pin);
                        txtLog.AppendText("✅ PIN prihvaćen, prijavljen na karticu.\n");

                        LoadCertificates(session);
                    }
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"❌ Greška pri prijavi na smart karticu: {ex.Message}\n");
            }
        }

        private void LoadCertificates(ISession session)
        {
            try
            {
                cbPkcs11Libs.Items.Clear();

                var certificateObjects = session.FindAllObjects(new List<IObjectAttribute>
                {
                    session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
                });

                if (certificateObjects.Count == 0)
                {
                    txtLog.AppendText("❌ Nema sertifikata na smart kartici.\n");
                    return;
                }

                foreach (var certObject in certificateObjects)
                {
                    byte[] certBytes = session.GetAttributeValue(certObject, new List<CKA> { CKA.CKA_VALUE })[0].GetValueAsByteArray();
                    X509CertificateParser parser = new X509CertificateParser();
                    Org.BouncyCastle.X509.X509Certificate cert = parser.ReadCertificate(certBytes);

                    string ekuInfo = "Nepoznato (Nema EKU)";
                    if (cert.GetExtendedKeyUsage() != null)
                    {
                        if (cert.GetExtendedKeyUsage().Contains(new DerObjectIdentifier("1.3.6.1.5.5.7.3.2")))
                        {
                            ekuInfo = "AUTH";
                        }
                        if (cert.GetExtendedKeyUsage().Contains(new DerObjectIdentifier("1.3.6.1.5.5.7.3.3")))
                        {
                            ekuInfo = "SIGN";
                        }
                    }

                    cbPkcs11Libs.Items.Add($"{cert.SubjectDN.ToString()} ({ekuInfo})");
                }

                if (cbPkcs11Libs.Items.Count > 0)
                {
                    cbPkcs11Libs.SelectedIndex = 0;
                    txtLog.AppendText($"✅ Učitano {cbPkcs11Libs.Items.Count} sertifikata.\n");
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"❌ Greška pri učitavanju sertifikata: {ex.Message}\n");
            }
        }

        private void ChooseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtSelectedFolder.Text = folderDialog.SelectedPath;
                txtLog.AppendText($"📂 Odabran folder: {txtSelectedFolder.Text}\n");
            }
        }

        private string PromptForPIN()
        {
            return Microsoft.VisualBasic.Interaction.InputBox("Unesite PIN za Smart Karticu:", "PIN za Smart Karticu", "", -1, -1);
        }

        private void Pkcs11Lib_Changed(object sender, EventArgs e)
        {
            selectedPkcs11Lib = cbPkcs11Libs.SelectedItem?.ToString();
            txtLog.AppendText("🔄 Izabrana PKCS#11 biblioteka promenjena.\n");
        }
    }
}