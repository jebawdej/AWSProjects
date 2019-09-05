using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSConfigLibrary
{
    public class AwsConfigModel
    {
        public string clientEndpoint;
        public string clientId;
        public string certificateFile;
        public string privateKeyFile;
        public string siteNumber;
        public string topicPrefix;
        public string awsRootCaCertFile;
    }
}
