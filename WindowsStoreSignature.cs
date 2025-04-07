using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using iText.Signatures;

namespace WindowsFormsPotpis;
public class WindowsStoreSignature : IExternalSignature
{
    private readonly X509Certificate2 _certificate;

    public WindowsStoreSignature(X509Certificate2 certificate)
    {
        _certificate = certificate;
    }

    public string GetDigestAlgorithmName() => "SHA-512";
    public string GetSignatureAlgorithmName() => "SHA512withRSA";
    public string GetEncryptionAlgorithm() => "RSA";
    public ISignatureMechanismParams? GetSignatureMechanismParameters() => null;

    public byte[] Sign(byte[] message)
    {
        using (var rsa = _certificate.GetRSAPrivateKey())
        {
            return rsa.SignData(message, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
        }
    }
}
