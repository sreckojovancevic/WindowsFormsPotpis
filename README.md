# WindowsFormsPotpis

A Windows Forms application for digitally signing PDF files using either Windows Certificate Store or PKCS#11 smart cards.  
Supports visible signature layer with custom styles, signature preview, and batch signing mode.

---

## ✨ Features

- Sign PDF documents with Windows or PKCS#11 certificates
- Add a visible signature layer (text, facsimile, custom fonts/colors)
- Preview and set signature position visually
- Batch-sign multiple PDF files at once
- Timestamping support (optional)
- Certificate selection and PIN input for PKCS#11

---

## 🛠️ Technologies

- C# .NET (Windows Forms)
- [iText 9.1.0](https://itextpdf.com) for PDF manipulation
- [Pkcs11Interop](https://github.com/Pkcs11Interop/Pkcs11Interop) for smart card access
- Newtonsoft.Json

---

## 📸 Screenshot

*(Insert an optional screenshot here)*

---

## 🤝 Contributors

- **Srecko Jovancevic ** – Creator & Maintainer  
- **ChatGPT / Code Assistant (OpenAI)** – Assistant for implementation & guidance  

---

## 📄 License

This project is licensed under the **MIT License** – see the [LICENSE](./LICENSE) file for details.  
⚠️ It also uses **iText 9.1.0**, which is **AGPL v3 licensed**. Redistribution requires open-sourcing your full application or obtaining a commercial iText license.

