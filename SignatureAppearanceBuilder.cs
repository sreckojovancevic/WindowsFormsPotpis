using iText.Kernel.Colors;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;

namespace WindowsFormsPotpis;

public class SignatureAppearanceBuilder
{
    private iText.Kernel.Colors.Color backgroundColor = ColorConstants.LIGHT_GRAY;
    private iText.Kernel.Colors.Color borderColor = ColorConstants.BLACK;
    private float borderWidth = 1f;
    private float fontSize = 10f;
    private iText.Kernel.Colors.Color fontColor = ColorConstants.BLACK;
    private TextAlignment textAlignment = TextAlignment.LEFT;
    private string signerName = "Potpis";

    public SignatureAppearanceBuilder SetBackgroundColor(iText.Kernel.Colors.Color color)
    {
        backgroundColor = color;
        return this;
    }

    public SignatureAppearanceBuilder SetBorderColor(iText.Kernel.Colors.Color color)
    {
        borderColor = color;
        return this;
    }

    public SignatureAppearanceBuilder SetBorderWidth(float width)
    {
        borderWidth = width;
        return this;
    }

    public SignatureAppearanceBuilder SetFontSize(float size)
    {
        fontSize = size;
        return this;
    }

    public SignatureAppearanceBuilder SetFontColor(iText.Kernel.Colors.Color color)
    {
        fontColor = color;
        return this;
    }

    public SignatureAppearanceBuilder SetTextAlignment(TextAlignment alignment)
    {
        textAlignment = alignment;
        return this;
    }

    public SignatureAppearanceBuilder SetSignerName(string name)
    {
        signerName = name;
        return this;
    }

    public Paragraph Build()
    {
        return new Paragraph($"Potpisao: {signerName}")
            .SetBackgroundColor(backgroundColor)
            .SetBorder(new SolidBorder(borderColor, borderWidth))
            .SetFontSize(fontSize)
            .SetFontColor(fontColor)
            .SetTextAlignment(textAlignment);
    }
}
