using System;
using Net.Pkcs11Interop.HighLevelAPI;
using iText.Signatures;
namespace WindowsFormsPotpis;
public class Pkcs11Signature : IExternalSignature
{
    private readonly ISession _session;
    private readonly IObjectHandle _privateKey;
    private readonly IMechanism _mechanism;

    public Pkcs11Signature(ISession session, IObjectHandle privateKey, IMechanism mechanism)
    {
        _session = session;
        _privateKey = privateKey;
        _mechanism = mechanism;
    }

    public string GetDigestAlgorithmName() => "SHA-512";
    public string GetSignatureAlgorithmName() => "SHA512withRSA";
    public string GetEncryptionAlgorithm() => "RSA";
    public ISignatureMechanismParams? GetSignatureMechanismParameters() => null;

    public byte[] Sign(byte[] message)
    {
        return _session.Sign(_mechanism, _privateKey, message);
    }
}
