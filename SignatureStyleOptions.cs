using iText.Kernel.Colors;
using Color = iText.Kernel.Colors.Color;

namespace WindowsFormsPotpis.Models
{
    public class SignatureStyleOptions
    {
        public float FontSize { get; set; } = 10f;
        public float BorderWidth { get; set; } = 1f;

        public Color FontColor { get; set; } = ColorConstants.BLACK;
        public Color BackgroundColor { get; set; } = ColorConstants.WHITE;
        public Color BorderColor { get; set; } = ColorConstants.BLACK;

        public string Reason { get; set; } = "Digitalni potpis";
        public string Location { get; set; } = "Beograd";

        // Helper za konverziju iz System.Drawing u iText boju
        public static Color ConvertToITextColor(System.Drawing.Color color)
        {
            return new DeviceRgb(color.R, color.G, color.B);
        }
    }
}
