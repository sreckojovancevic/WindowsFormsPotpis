using System;
using System.Collections.Generic;
using SysPath = System.IO.Path;
using System.IO; // For file handling
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Newtonsoft.Json;
using System.Security.Cryptography;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Geom; // For iText Rectangle
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using WindowsFormsPotpis.Models;

namespace WindowsFormsPotpis;

public partial class Form1 : Form
{
    private string? selectedPdfFile = null;
    private string? selectedPkcs11Lib;
    private string? selectedSignatureImage = null;
    private List<IObjectHandle> availableCertificates = new List<IObjectHandle>();
    private iText.Kernel.Geom.Rectangle signatureRect = new iText.Kernel.Geom.Rectangle(150, 100, 300, 120); // Default position
    private Button btnChooseOutput;
    private TextBox txtOutputFile;
    private string outputPdfPath;
    private ComboBox cbPkcs11Certificates;
    private SignatureStyleOptions currentSignatureStyle = new SignatureStyleOptions();



    private Pkcs11Signer pkcs11Signer;
    private WindowsStoreSigner windowsStoreSigner;

    public Form1()
    {
        InitializeComponent();
        LoadAvailablePkcs11Libs();
        pkcs11Signer = new Pkcs11Signer();
        windowsStoreSigner = new WindowsStoreSigner();
    }

    private void LoadAvailablePkcs11Libs()
    {
        cbPkcs11Libs.Items.Clear();
        cbPkcs11Libs.Items.Add(@"C:\Program Files\MUP RS\Celik\netsetpkcs11_x64.dll");
        cbPkcs11Libs.SelectedIndex = 0;
    }

    private void LoadCertificates()

    {

        cbCertificates.Items.Clear();

        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

        store.Open(OpenFlags.ReadOnly);


        foreach (var cert in store.Certificates)

        {

            cbCertificates.Items.Add(cert);

        }


        store.Close();

    }


    private void btnRefreshCertificates_Click(object sender, EventArgs e)
    {
        LoadCertificates(); // Call the method to load certificates
        txtLog.AppendText("🔄 Certificates refreshed.\n");
    }

    private void cbPkcs11Libs_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedPkcs11Lib = cbPkcs11Libs.SelectedItem.ToString();
        txtLog.AppendText($"🔹 Izabrana PKCS#11 biblioteka: {selectedPkcs11Lib}\n");
    }

    private void cbCertificates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbCertificates.SelectedItem is X509Certificate2 cert)
        {
            txtLog.AppendText($"🔹 Izabran sertifikat: {cert.Subject}\n");
        }
    }

    private void btnChooseFile_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "PDF files (*.pdf)|*.pdf",
            Title = "Odaberi PDF fajl za potpisivanje"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            selectedPdfFile = openFileDialog.FileName;
            txtSelectedFile.Text = selectedPdfFile;
            txtLog.AppendText("✔ PDF fajl izabran: " + selectedPdfFile + "\n");
        }
    }

    private void btnChooseSignature_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedPdfFile))
        {
            txtLog.AppendText("❌ Morate prvo izabrati PDF fajl!\n");
            return;
        }

        SignaturePdfPreviewForm preview = new SignaturePdfPreviewForm(selectedPdfFile);
        if (preview.ShowDialog() == DialogResult.OK)
        {
            var box = preview.SelectedRectangle;
            signatureRect = new iText.Kernel.Geom.Rectangle(box.X, box.Y, box.Width, box.Height);
            txtLog.AppendText("✔ Pozicija potpisa podešena kroz PDF preview.\n");
        }
    }


    private void btnSign_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedPdfFile))
        {
            txtLog.AppendText("❌ Morate izabrati PDF fajl!\n");
            return;
        }

        if (string.IsNullOrEmpty(outputPdfPath))
        {
            txtLog.AppendText("❌ Morate izabrati gde da sačuvate potpisani PDF (koristite 'Sačuvaj kao').\n");
            return;
        }

        string tempWithImage = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(outputPdfPath), "temp_with_signature_image.pdf");

        // ✅ Ako je izabrana slika - dodajemo je
        if (!string.IsNullOrEmpty(selectedSignatureImage))
        {
            HandwrittenSignatureAdder.AddSignature(selectedPdfFile, tempWithImage, selectedSignatureImage, signatureRect);
            txtLog.AppendText("🖊️ Dodata slika potpisa.\n");
        }
        else
        {
            File.Copy(selectedPdfFile, tempWithImage, true);
            txtLog.AppendText("ℹ️ Nema slike potpisa – koristi se samo digitalni potpis.\n");
        }

        if (cbCertificates.SelectedItem is X509Certificate2 selectedCert)
        {
            windowsStoreSigner.SignPdf(
                tempWithImage,
                selectedCert,
                outputPdfPath,
                txtLog,
                chkTimestamp.Checked,
                signatureRect);
        }
        else
        {
            txtLog.AppendText("❌ Niste izabrali sertifikat.\n");
        }
    }

    private void btnCustomizeSignatureStyle_Click(object sender, EventArgs e)
    {
        using (var styleForm = new SignatureStyleForm())
        {
            if (styleForm.ShowDialog() == DialogResult.OK)
            {
                // Ovde preuzimaš izabrane opcije
                var selectedOptions = styleForm.SelectedOptions;

                // Primer logike: čuvanje u polje, log prikaz, itd.
                txtLog.AppendText("🎨 Korisnički stil potpisa sačuvan.\n");
                // Možeš čuvati i u form-level varijablu
                this.currentSignatureStyle = selectedOptions;
            }
            else
            {
                txtLog.AppendText("ℹ️ Personalizacija potpisa otkazana.\n");
            }
        }
    }

    private void btnSetSignaturePosition_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedPdfFile))
        {
            txtLog.AppendText("❌ Prvo izaberite PDF fajl.\n");
            return;
        }

        SignaturePdfPreviewForm preview = new SignaturePdfPreviewForm(selectedPdfFile);
        if (preview.ShowDialog() == DialogResult.OK)
        {
            var box = preview.SelectedRectangle;
            signatureRect = new iText.Kernel.Geom.Rectangle(box.X, box.Y, box.Width, box.Height);
            txtLog.AppendText($"✔ Pozicija potpisa postavljena na X:{box.X}, Y:{box.Y}, Width:{box.Width}, Height:{box.Height}\n");
        }
    }





    private string GetPinFromUser()
    {
        using (PinPromptForm pinForm = new PinPromptForm())
        {
            return pinForm.ShowDialog() == DialogResult.OK ? pinForm.EnteredPin : string.Empty;
        }
    }

    
    private void Form1_Load(object sender, EventArgs e)
    {
        txtLog.AppendText("📂 Application Loaded.\n");
    }

    private void btnSavePath_Click(object sender, EventArgs e)
    {
        txtLog.AppendText("📌 Path Saved.\n");
    }

    private void btnRefreshPkcs11Libs_Click(object sender, EventArgs e)
    {
        LoadAvailablePkcs11Libs();
        txtLog.AppendText("🔄 PKCS#11 Libraries Refreshed.\n");
    }

    private void btnChoosePkcs11Lib_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "PKCS#11 Libraries (*.dll)|*.dll",
            Title = "Select PKCS#11 Library"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            selectedPkcs11Lib = openFileDialog.FileName;
            txtPkcs11LibPath.Text = selectedPkcs11Lib;
        }
    }

    private void btnChooseOutput_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "PDF files (*.pdf)|*.pdf",
            Title = "Izaberite lokaciju za čuvanje potpisanog PDF-a"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            outputPdfPath = saveFileDialog.FileName;
            txtOutputFile.Text = outputPdfPath;
        }
    }

    private void btnRefreshPkcs11Certificates_Click(object sender, EventArgs e)
    {
        cbPkcs11Certificates.Items.Clear();

        if (string.IsNullOrEmpty(selectedPkcs11Lib))
        {
            txtLog.AppendText("❌ Niste izabrali PKCS#11 biblioteku.\n");
            return;
        }

        var result = PKCS11Authenticator.AuthenticateAndOpenSession(selectedPkcs11Lib);
        if (result == null)
        {
            txtLog.AppendText("❌ Autentifikacija neuspešna.\n");
            return;
        }

        try
        {
            var certObjects = result.Session.FindAllObjects(new List<IObjectAttribute>
        {
            result.Session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
        });

            foreach (var obj in certObjects)
            {
                // 🔍 Učitaj sertifikat iz kartice
                byte[] rawData = result.Session.GetAttributeValue(obj, new List<CKA> { CKA.CKA_VALUE })[0].GetValueAsByteArray();
                var parser = new Org.BouncyCastle.X509.X509CertificateParser();
                var cert = parser.ReadCertificate(rawData);

                string subject = cert.SubjectDN.ToString();

                string display = subject.Contains("Sign")
                    ? "✍️ SIGN: " + subject
                    : subject.Contains("Auth")
                        ? "🔐 AUTH: " + subject
                        : "📄 UNKNOWN: " + subject;

                cbPkcs11Certificates.Items.Add(new ComboBoxItem
                {
                    DisplayText = display,
                    CertObject = obj
                });
            }

            if (cbPkcs11Certificates.Items.Count > 0)
            {
                cbPkcs11Certificates.SelectedIndex = 0;
                txtLog.AppendText("✅ PKCS#11 sertifikati učitani: " + cbPkcs11Certificates.Items.Count + "\n");
            }
            else
            {
                txtLog.AppendText("⚠️ Nema dostupnih sertifikata na kartici.\n");
            }
        }
        catch (Exception ex)
        {
            txtLog.AppendText("❌ Greška pri čitanju sertifikata: " + ex.Message + "\n");
        }
        finally
        {
            result.Session.Logout();
            result.Session.Dispose();
            result.Library.Dispose();
        }
    }


    private void btnSignWithPkcs11_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedPdfFile))
        {
            txtLog.AppendText("❌ Morate izabrati PDF fajl!\n");
            return;
        }

        if (string.IsNullOrEmpty(outputPdfPath))
        {
            txtLog.AppendText("❌ Morate izabrati gde da sačuvate potpisani PDF (koristite 'Sačuvaj kao').\n");
            return;
        }

        if (string.IsNullOrEmpty(selectedPkcs11Lib))
        {
            txtLog.AppendText("❌ Niste izabrali PKCS#11 biblioteku.\n");
            return;
        }

        if (!PKCS11Authenticator.AuthenticateWithPkcs11(selectedPkcs11Lib, out string pin))
        {
            txtLog.AppendText("❌ Autentifikacija nije uspela.\n");
            return;
        }

        string tempWithImage = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(outputPdfPath), "temp_with_signature_image.pdf");

        // ✅ Ako je slika izabrana, ubacujemo je
        if (!string.IsNullOrEmpty(selectedSignatureImage))
        {
            HandwrittenSignatureAdder.AddSignature(selectedPdfFile, tempWithImage, selectedSignatureImage, signatureRect);
            txtLog.AppendText("🖊️ Dodata slika potpisa.\n");
        }
        else
        {
            File.Copy(selectedPdfFile, tempWithImage, true);
            txtLog.AppendText("ℹ️ Nema slike potpisa – koristi se samo digitalni potpis.\n");
        }

        pkcs11Signer.SignPdfWithPkcs11(
            tempWithImage,              // 👈 koristi fajl sa slikom (ili bez nje)
            selectedPkcs11Lib,
            pin,
            outputPdfPath,
            txtLog,
            chkTimestamp.Checked,
            signatureRect
        );
    }



}
