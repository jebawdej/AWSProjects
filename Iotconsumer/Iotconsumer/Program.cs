using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Security;
using System.Security.Cryptography.X509Certificates;


namespace Iotconsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string IotEndPoint = "112358906604.iot.us-east-1.amazonaws.com";

            int BrokerPort = 8883;
            string Topic = "My_Iot_SNS_Topic/TutorialX1";

            var CaCert = X509Certificate.CreateFromCertFile(@"..\..\..\..\Certificates\f489713a83-certificate.pem.crt");
            var clientCert = new X509Certificate2(@"..\..\..\..\Certificates\devicecertificateinpfxformat.pfx", "Wim#1234");


            string ClientID = Guid.NewGuid().ToString();

            var IotClient = new MqttClient(IotEndPoint, BrokerPort, true, CaCert, clientCert, MqttSslProtocols.TLSv1_2);
            IotClient.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            IotClient.MqttMsgSubscribed += Client_MqttMsgSubscribed;

            IotClient.Connect(ClientID);
            Console.WriteLine("Connected");
            IotClient.Subscribe(new string[] { Topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            while (true)
            {
                //keeping the main thread alive for the event call backs
            }

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
