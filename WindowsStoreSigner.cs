using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using iText.IO.Image;
using System.Windows.Forms;
using iText.Kernel.Geom;
using Rectangle = iText.Kernel.Geom.Rectangle;

namespace WindowsFormsPotpis
{
    public class WindowsStoreSigner
    {
        public void SignPdf(string inputPdfPath, X509Certificate2 cert, string outputPdfPath, TextBox txtLog, bool includeTimestamp, Rectangle signatureRect)
        {
            try
            {
                // Create a PdfReader to read the input PDF
                using (var reader = new PdfReader(inputPdfPath))
                using (var output = new FileStream(outputPdfPath, FileMode.Create))
                {
                    // Create a PdfSigner instance
                    PdfSigner signer = new PdfSigner(reader, output, new StampingProperties());

                    // Create an external signature using the Windows Store certificate
                    IExternalSignature externalSignature = new WindowsStoreSignature(cert);
                    IExternalDigest digest = new BouncyCastleDigest();

                    // Convert the X509Certificate2 to the BouncyCastle format
                    var bouncyCastleCert = ConvertToBouncyCastleCertificate(cert);

                    // Create an iText-compatible certificate from the BouncyCastle certificate
                    var iTextCert = new iText.Bouncycastle.X509.X509CertificateBC(bouncyCastleCert);

                    // Sign the PDF
                    signer.SignDetached(
                        digest,
                        externalSignature,
                        new[] { iTextCert }, // Use the converted BouncyCastle certificate
                        null, // CRL (Certificate Revocation List) - can be null if not used
                        null, // OCSP (Online Certificate Status Protocol) - can be null if not used
                        null, // Timestamp - can be null if not used
                        0,    // Reason for the signature
                        PdfSigner.CryptoStandard.CADES // Signature standard
                    );

                    txtLog.AppendText($"✅ PDF uspešno potpisan: {outputPdfPath}\n");
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"❌ Error while signing: {ex.Message}\n");
            }
        }

        private Org.BouncyCastle.X509.X509Certificate ConvertToBouncyCastleCertificate(X509Certificate2 cert)
        {
            // Get the raw data of the certificate
            byte[] rawData = cert.Export(X509ContentType.Cert);
            // Create a BouncyCastle X509Certificate from the raw data
            return new X509CertificateParser().ReadCertificate(rawData);
        }
    }
}