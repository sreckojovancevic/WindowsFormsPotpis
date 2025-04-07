using Net.Pkcs11Interop.HighLevelAPI;

namespace WindowsFormsPotpis
{
    public class ComboBoxItem
    {
        public string DisplayText { get; set; } = string.Empty;
        public IObjectHandle CertObject { get; set; }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
