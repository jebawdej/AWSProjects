using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AmazonLambdaClient client = new AmazonLambdaClient("AKIAIEY6BKXHNKMI3EAA", "44MGomaQMrM+cy9BP1IiZMO80zYfWiV/JYHFl0L+", RegionEndpoint.USEast1);


            InvokeRequest ir = new InvokeRequest
            {
                FunctionName = "TutorialX1",
                InvocationType = InvocationType.RequestResponse,
                Payload = "{\"Name\": \"Wim de Jong\", \"Email\": \"wim@email.nl\"}"
            };

            InvokeResponse response = client.Invoke(ir);

            var sr = new StreamReader(response.Payload);
            JsonReader reader = new JsonTextReader(sr);

            var serilizer = new JsonSerializer();
            var op = serilizer.Deserialize(reader);

            Console.WriteLine(op);
            Console.ReadLine();
        }
    }
}
