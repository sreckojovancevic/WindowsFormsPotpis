using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Geom;

namespace WindowsFormsPotpis;

public static class HandwrittenSignatureAdder
{
    public static void AddSignature(string pdfPath, string outputPdfPath, string signatureImagePath, iText.Kernel.Geom.Rectangle rect)
    {
        try
        {
            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdfPath), new PdfWriter(outputPdfPath)))
            {
                // ✅ Uzimanje prve strane dokumenta
                PdfCanvas canvas = new PdfCanvas(pdfDoc.GetFirstPage());

                // ✅ Učitavanje slike potpisa
                ImageData signatureImage = ImageDataFactory.Create(signatureImagePath);

                // ✅ Kreiranje XObject-a za sliku
                PdfFormXObject imgXObject = new(new PdfStream(signatureImage.GetData()));

                // ✅ Dodavanje slike na stranicu na određenu poziciju
                canvas.AddXObjectAt(imgXObject, rect.GetX(), rect.GetY());

                pdfDoc.Close();
            }

            Console.WriteLine("✔ Ručni potpis uspešno dodat: " + outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Greška pri dodavanju ručnog potpisa: " + ex.Message);
        }
    }
}
