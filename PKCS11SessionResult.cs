using Net.Pkcs11Interop.HighLevelAPI;

namespace WindowsFormsPotpis
{
    public class PKCS11SessionResult
    {
        public ISession Session { get; set; } = null!;
        public string Pin { get; set; } = string.Empty;
        public IPkcs11Library Library { get; set; } = null!;
    }
}
