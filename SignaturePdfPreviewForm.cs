using System;
using System.Drawing;
using System.Windows.Forms;
using PdfiumViewer;

namespace WindowsFormsPotpis
{
    public class SignaturePdfPreviewForm : Form
    {
        private PdfViewer pdfViewer;
        private Button btnOk;
        private Panel signatureBox;

        public Rectangle SelectedRectangle => signatureBox.Bounds;

        public SignaturePdfPreviewForm(string pdfPath)
        {
            InitializeComponent();
            LoadPdf(pdfPath);
        }

        private void InitializeComponent()
        {
            this.Text = "Pozicioniranje potpisa";
            this.Size = new Size(800, 600);

            pdfViewer = new PdfViewer
            {
                Dock = DockStyle.Fill,
                ZoomMode = PdfViewerZoomMode.FitWidth
            };

            btnOk = new Button
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            btnOk.Click += BtnOk_Click;

            // 📦 Panel koji prikazuje marker za potpis
            signatureBox = new Panel
            {
                Size = new Size(150, 50),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(100, 100)
            };

            signatureBox.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, signatureBox.ClientRectangle,
                    Color.Blue, 2, ButtonBorderStyle.Solid,
                    Color.Blue, 2, ButtonBorderStyle.Solid,
                    Color.Blue, 2, ButtonBorderStyle.Solid,
                    Color.Blue, 2, ButtonBorderStyle.Solid);
            };

            // Omogući "drag" marker panela
            signatureBox.MouseDown += SignatureBox_MouseDown;
            signatureBox.MouseMove += SignatureBox_MouseMove;

            this.Controls.Add(signatureBox);
            this.Controls.Add(pdfViewer);
            this.Controls.Add(btnOk);
        }

        private void LoadPdf(string pdfPath)
        {
            pdfViewer.Document?.Dispose();
            pdfViewer.Document = PdfDocument.Load(pdfPath);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Drag funkcionalnost
        private Point dragOffset;

        private void SignatureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                dragOffset = e.Location;
        }

        private void SignatureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var newPos = signatureBox.Location;
                newPos.X += e.X - dragOffset.X;
                newPos.Y += e.Y - dragOffset.Y;
                signatureBox.Location = newPos;
            }
        }
    }
}
