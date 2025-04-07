using iText.Kernel.Geom;
using iText.Signatures;
using iText.Forms.Form.Element;
using iText.Kernel.Pdf;
using iText.Kernel.Colors;
using iText.Forms.Fields.Properties;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Layout.Borders;
using Rectangle = iText.Kernel.Geom.Rectangle;

namespace WindowsFormsPotpis.Helpers
{
    public static class VisualSignatureHelper
    {
        private const string IMAGE_PATH = "path/to/image.png"; // Set your image path
        private const string BOLD = "path/to/font.ttf"; // Set your font path

        public static SignerProperties CreateSignerProperties()
        {
            var signerProperties = new SignerProperties().SetFieldName("Signature1");

            // Create the appearance instance and set the signature content to be shown and different appearance properties.
            var appearance = new SignatureFieldAppearance(signerProperties.GetFieldName())
                .SetContent(new SignedAppearanceText().SetReasonLine("Customized reason: Reason")
                    .SetLocationLine("Customized location: Location"), ImageDataFactory.Create(IMAGE_PATH))
                .SetBorder(new SolidBorder(ColorConstants.DARK_GRAY, 2))
                .SetFont(PdfFontFactory.CreateFont(BOLD, PdfEncodings.IDENTITY_H));

            // Set created signature appearance and other signer properties.
            signerProperties
                .SetSignatureAppearance(appearance)
                .SetPageNumber(1)
                .SetPageRect(new Rectangle(50, 650, 200, 100))
                .SetReason("Reason")
                .SetLocation("Location");

            return signerProperties;
        }
    }
}