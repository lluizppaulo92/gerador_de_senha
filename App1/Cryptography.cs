using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace App1
{
    class Cryptography
    {
        public string Encrypt(string Text)
        {
            int I;
            string encr = "";
            for (I = 1; I <= Text.Length; I++)
            {
                encr = encr + Strings.Chr((Strings.Asc(Strings.Mid(Text, I, 1))) + 100);
            }
            return encr;

        }
        public string Decrypt(string Text)
        {
            int I;
            string dcr = "";
            for (I = 1; I <= Text.Length; I++)
            {
                dcr = dcr + Strings.Chr((Strings.Asc(Microsoft.VisualBasic.Strings.Mid(Text, I, 1))) - 100);
            }
            return dcr;
        }
    }
}
