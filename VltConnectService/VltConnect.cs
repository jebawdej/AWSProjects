using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace VltConnectService
{
    public partial class VltConnect : ServiceBase
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VltConnect()
        {
            log.Info("VltVonnect created by constructor");
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Service started");
            log.Info("Service started");
        }

        protected override void OnStop()
        {
            Console.WriteLine("Service stopped");
            log.Info("Service stopped");
        }
    }
}
