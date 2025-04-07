using iText.Forms.Form.Element;
using iText.Forms.Fields.Properties;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Layout.Borders;

namespace WindowsFormsPotpis.Helpers;

public static class SignatureFieldBuilder
{
    public static SignatureFieldAppearance Build(string signerName, string? imagePath = null)
    {
        var appearance = new SignatureFieldAppearance("Signature1")
            .SetContent(new SignedAppearanceText()
                .SetReasonLine($"Potpisao: {signerName}")
                .SetLocationLine("Lokacija: Beograd"))
            .SetBorder(new SolidBorder(ColorConstants.DARK_GRAY, 1))
            .SetBackgroundColor(ColorConstants.LIGHT_GRAY);

        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
        {
            var imageData = ImageDataFactory.Create(imagePath);
            appearance.SetContent(
                new SignedAppearanceText()
                    .SetReasonLine($"Potpisao: {signerName}")
                    .SetLocationLine("Lokacija: Beograd"),
                imageData
            );
        }

        return appearance;
    }
}
