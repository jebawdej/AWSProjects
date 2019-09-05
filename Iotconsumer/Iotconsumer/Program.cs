using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using AWSConfigLibrary;

namespace Iotconsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            AWSConfig awsConfig = new AWSConfig(System.Environment.CurrentDirectory + "\\DVSA\\" + "DVSAConfigFile.cfg");

            //string IotEndPoint = "a1e4diayuptetn.iot.eu-west-1.amazonaws.com";
            string IotEndPoint = awsConfig.ReadConfigValue("AWSConfig", "clientEndpoint");
            if (String.IsNullOrEmpty(IotEndPoint))
                Console.WriteLine("[ERROR]: Can't find clientEndpoint");
            int BrokerPort = 8883;

            string topicPrefix = awsConfig.ReadConfigValue("AWSConfig", "topicPrefix");
            string siteNumber = awsConfig.ReadConfigValue("AWSConfig", "siteNumber");


            string topicIn = String.Format("{0}/in/{1}", topicPrefix, siteNumber);
            string topicOut = String.Format("{0}/out/{1}", topicPrefix, siteNumber);

            var CaCert = X509Certificate.CreateFromCertFile(@"..\..\..\..\Certificates\client180.cert.pem");
            //var clientCert = new X509Certificate2(@"..\..\..\..\Certificates\devicecertificateClient180.pfx", "vlt#1234");//pfx file produced with openSSL
            var clientCert = new X509Certificate2(@"..\..\..\..\Certificates\Client180.pfx", "vlt#1234"); //pfx file produced with chilkat in pfxCreator project.
            MqttClient IotClient = null;
            string ClientID = "client180";
            try
            {
                IotClient = new MqttClient(IotEndPoint, BrokerPort, true, CaCert, clientCert, MqttSslProtocols.TLSv1_2);
                IotClient.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                IotClient.MqttMsgSubscribed += Client_MqttMsgSubscribed;

                IotClient.Connect(ClientID);
                Console.WriteLine("Connected");
                IotClient.Subscribe(new string[] { topicIn }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: Could not connect/subscribe to the IotEndPoint");
                Console.WriteLine("ERROR message: {0}",e.Message);
                if(e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException);
                }
            }

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
            IotClient.Disconnect();
        }

        private static void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Message subscribed");
        }

        private static void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Message Received is      " + System.Text.Encoding.UTF8.GetString(e.Message));
        }
    }
}
