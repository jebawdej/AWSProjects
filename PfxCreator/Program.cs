using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PfxCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Chilkat.Cert cert = new Chilkat.Cert();

            //bool success = cert.LoadFromFile("client180.cert.pem");
            bool success = cert.LoadFromFile("2c27fb8e91-certificate.pem.crt");
            if (success != true)
            {
                Console.WriteLine(cert.LastErrorText);
                return;
            }
            //  Get it as a certificate chain
            Chilkat.CertChain certChain = cert.GetCertChain();
            if (cert.LastMethodSuccess != true)
            {
                Console.WriteLine(cert.LastErrorText);
                return;
            }

            //  Next, load the corresponding private key from a PEM.
            Chilkat.PrivateKey privKey = new Chilkat.PrivateKey();
           // success = privKey.LoadPemFile("client180.private.key");
            success = privKey.LoadPemFile("2c27fb8e91-private.pem.key");
            if (success != true)
            {
                Console.WriteLine(privKey.LastErrorText);

                return;
            }

            //  Create a PFX object instance, and add the private key + cert chain.
            Chilkat.Pfx pfx = new Chilkat.Pfx();
            success = pfx.AddPrivateKey(privKey, certChain);
            if (success != true)
            {
                Console.WriteLine(pfx.LastErrorText);

                return;
            }

            //  Finally, write the PFX w/ a password.
            success = pfx.ToFile("vlt#1234", "2c27fb8e91.pfx");
            if (success != true)
            {
                Console.WriteLine(pfx.LastErrorText);
                return;
            }

            Console.WriteLine("Success.");
            Console.ReadLine();
        }
    }
}
