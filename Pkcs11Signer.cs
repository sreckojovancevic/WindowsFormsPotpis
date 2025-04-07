using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Org.BouncyCastle.X509;
using iText.Kernel.Pdf;
using iText.Signatures;
using iText.Kernel.Geom;
using iText.Commons;
using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle.Cert;

namespace WindowsFormsPotpis
{
    public class Pkcs11Signer
    {
        public void SignPdfWithPkcs11(
            string inputPdfPath,
            string pkcs11LibPath,
            string pin,
            string outputPdfPath,
            TextBox txtLog,
            bool includeTimestamp,
            iText.Kernel.Geom.Rectangle signatureRect)
        {
            try
            {
                using (IPkcs11Library pkcs11Library = new Pkcs11InteropFactories().Pkcs11LibraryFactory.LoadPkcs11Library(
                    new Pkcs11InteropFactories(), pkcs11LibPath, AppType.MultiThreaded))
                {
                    var slots = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent);
                    if (slots.Count == 0)
                    {
                        txtLog.AppendText("❌ Nema dostupnih smart kartica.\n");
                        return;
                    }

                    using (ISession session = slots[0].OpenSession(SessionType.ReadWrite))
                    {
                        session.Login(CKU.CKU_USER, pin);
                        txtLog.AppendText("✅ PIN prihvaćen, prijavljen na smart karticu.\n");

                        var certificateObjects = session.FindAllObjects(new List<IObjectAttribute>
                        {
                            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
                        });

                        if (certificateObjects.Count == 0)
                        {
                            txtLog.AppendText("❌ Nema dostupnih sertifikata na kartici.\n");
                            return;
                        }

                        byte[] certBytes = session.GetAttributeValue(certificateObjects[0], new List<CKA> { CKA.CKA_VALUE })[0].GetValueAsByteArray();
                        var parser = new X509CertificateParser();
                        var cert = parser.ReadCertificate(certBytes);

                        var privateKeyObjects = session.FindAllObjects(new List<IObjectAttribute>
                        {
                            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY)
                        });

                        if (privateKeyObjects.Count == 0)
                        {
                            txtLog.AppendText("❌ Privatni ključ nije pronađen na smart kartici.\n");
                            return;
                        }

                        IObjectHandle privateKey = privateKeyObjects.First();
                        IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_SHA512_RSA_PKCS);

                        //IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_RSA_PKCS);

                        using (var reader = new PdfReader(inputPdfPath))
                        using (var output = new FileStream(outputPdfPath, FileMode.Create))
                        {
                            var signer = new PdfSigner(reader, output, new StampingProperties());

                            var signerProps = new SignerProperties()
                                .SetReason("Potpisano u aplikaciji ePotpis")
                                .SetLocation("Beograd, Srbija")
                                .SetPageRect(signatureRect)
                                .SetPageNumber(1);

                            signer.SetSignerProperties(signerProps);

                            var certChain = new IX509Certificate[] { new X509CertificateBC(cert) };
                            IExternalSignature externalSignature = new Pkcs11Signature(session, privateKey, mechanism);
                            IExternalDigest digest = new BouncyCastleDigest();

                            var crlClients = new List<ICrlClient> { new CrlClientOnline() };
                            IOcspClient ocspClient = new OcspClientBouncyCastle();
                            ITSAClient tsaClient = includeTimestamp
                                ? new TSAClientBouncyCastle("https://freetsa.org/tsr")
                                : null;

                            signer.SignDetached(
                                digest,
                                externalSignature,
                                certChain,
                                crlClients,
                                ocspClient,
                                tsaClient,
                                0,
                                PdfSigner.CryptoStandard.CADES);
                        }

                        txtLog.AppendText($"✅ PDF uspešno potpisan: {outputPdfPath}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"❌ Greška pri potpisivanju: {ex.Message}\n");
            }
        }
    }
}
