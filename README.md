# WindowsFormsPotpis

A Windows Forms application for digitally signing PDF documents using:

- **Windows Certificate Store** (CSP/KSP)
- **PKCS#11 compatible tokens/smartcards**
- Optional **visual signature layer** (text + facsimile image)
- **Batch signing support**
- **Custom positioning** of signature via GUI
- **Optional timestamp**

## ✨ Features

- ✅ Sign a single PDF file with a certificate from Windows Store or PKCS#11 token.
- ✅ Batch-sign all PDF files in a folder.
- ✅ Add a **visible signature layer** with:
  - Styled text
  - Custom font, colors
  - Optional **facsimile image** (e.g. scanned signature)
- ✅ Visually **select the signature rectangle** by clicking on the PDF.
- ✅ Supports **timestamping** (optional).
- ✅ Modern UI with tooltips and error messages.

---

## 🔧 Requirements

- .NET 6.0 or newer
- Visual Studio (or any compatible IDE)
- iText 9.1.0
- PKCS#11 middleware for your smartcard (e.g., `netsetpkcs11_x64.dll`)
- Optional: BouncyCastle, Newtonsoft.Json

---

## 📦 Installation & Running

1. **Clone the repo**:
   ```bash
   git clone https://github.com/yourusername/WindowsFormsPotpis.git
   cd WindowsFormsPotpis
