using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsPotpis.Models;
using iText.Kernel.Colors;
using Color = iText.Kernel.Colors.Color;

namespace WindowsFormsPotpis
{
    public partial class SignatureStyleForm : Form
    {
        private SignatureStyleOptions styleOptions = new SignatureStyleOptions();
        public SignatureStyleOptions SelectedOptions => styleOptions;

        public SignatureStyleForm()
        {
            InitializeComponent();
            UpdatePreview();

            btnFontColor.Click += btnFontColor_Click;
            btnBackgroundColor.Click += btnBackgroundColor_Click;
            btnBorderColor.Click += btnBorderColor_Click;
            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;

            numFontSize.ValueChanged += (_, _) => UpdatePreview();
            numBorderWidth.ValueChanged += (_, _) => UpdatePreview();
            txtReason.TextChanged += (_, _) => UpdatePreview();
            txtLocation.TextChanged += (_, _) => UpdatePreview();
        }

        private void UpdatePreview()
        {
            lblPreview.Text = $"Potpisao: Primer Imena\n{txtReason.Text}, {txtLocation.Text}";
            lblPreview.Font = new Font("Arial", (float)numFontSize.Value);
            lblPreview.ForeColor = ConvertToDrawingColor(styleOptions.FontColor);
            lblPreview.BackColor = ConvertToDrawingColor(styleOptions.BackgroundColor);
            lblPreview.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                styleOptions.FontColor = SignatureStyleOptions.ConvertToITextColor(colorDialog1.Color);
            UpdatePreview();
        }

        private void btnBackgroundColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                styleOptions.BackgroundColor = SignatureStyleOptions.ConvertToITextColor(colorDialog1.Color);
            UpdatePreview();
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                styleOptions.BorderColor = SignatureStyleOptions.ConvertToITextColor(colorDialog1.Color);
            UpdatePreview();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            styleOptions.FontSize = (float)numFontSize.Value;
            styleOptions.BorderWidth = (float)numBorderWidth.Value;
            styleOptions.Reason = txtReason.Text;
            styleOptions.Location = txtLocation.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private System.Drawing.Color ConvertToDrawingColor(Color color)
        {
            var rgb = color.GetColorValue();
            return System.Drawing.Color.FromArgb((int)rgb[0], (int)rgb[1], (int)rgb[2]);
        }
    }
}
