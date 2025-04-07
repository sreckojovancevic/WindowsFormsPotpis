using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsPotpis
{
    public class PKCS11Authenticator
    {
        public static bool AuthenticateWithPkcs11(string pkcs11LibPath, out string pin)
        {
            pin = string.Empty;

            try
            {
                using (IPkcs11Library pkcs11Library = new Pkcs11InteropFactories().Pkcs11LibraryFactory.LoadPkcs11Library(
                    new Pkcs11InteropFactories(), pkcs11LibPath, AppType.MultiThreaded))
                {
                    List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent);
                    if (slots.Count == 0)
                    {
                        MessageBox.Show("❌ Nema dostupnih smart kartica.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    using (ISession session = slots[0].OpenSession(SessionType.ReadWrite))
                    {
                        using (PinPromptForm pinForm = new PinPromptForm())
                        {
                            if (pinForm.ShowDialog() == DialogResult.OK)
                            {
                                pin = pinForm.EnteredPin;
                                session.Login(CKU.CKU_USER, pin);
                                MessageBox.Show("✅ PIN prihvaćen kroz PKCS#11.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Greška pri autentifikaciji: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
        public static PKCS11SessionResult? AuthenticateAndOpenSession(string pkcs11LibPath)
        {
            try
            {
                var factories = new Pkcs11InteropFactories();
                IPkcs11Library library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(
                    factories, pkcs11LibPath, AppType.MultiThreaded);

                List<ISlot> slots = library.GetSlotList(SlotsType.WithTokenPresent);
                if (slots.Count == 0)
                {
                    MessageBox.Show("❌ Nema dostupnih smart kartica.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                ISession session = slots[0].OpenSession(SessionType.ReadWrite);

                using (PinPromptForm pinForm = new PinPromptForm())
                {
                    if (pinForm.ShowDialog() == DialogResult.OK)
                    {
                        string pin = pinForm.EnteredPin;
                        session.Login(CKU.CKU_USER, pin);

                        MessageBox.Show("✅ PIN prihvaćen kroz PKCS#11.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return new PKCS11SessionResult
                        {
                            Session = session,
                            Pin = pin,
                            Library = library
                        };
                    }
                }

                session.Dispose();
                library.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Greška pri autentifikaciji: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

    }
}
