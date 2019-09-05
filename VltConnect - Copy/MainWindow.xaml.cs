using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VltConnect.ViewModel;

namespace VltConnect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu niContextMenu;
        private enum menuItems { Show, Hide, Reset, Exit }

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuItem menuItemShow;
        private System.Windows.Forms.MenuItem menuItemHide;
        private System.Windows.Forms.MenuItem menuItemReset;
        private System.Windows.Forms.MenuItem menuItemExit;
        public bool IsStarted = false;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            this.StateChanged += MainWindow_StateChanged;
            this.Loaded += MyWindow_Loaded;
            //itemData = new EgwItemDataViewModel();
            //this.DataContext = itemData;
            InitSystemTray();
        }

        private void InitSystemTray()
        {
            string basepath = Environment.CurrentDirectory;
            if (!basepath.EndsWith(@"\"))
                basepath += @"\";

            this.components = new System.ComponentModel.Container();
            this.niContextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemShow = new System.Windows.Forms.MenuItem();
            this.menuItemHide = new System.Windows.Forms.MenuItem();
            this.menuItemReset = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();

            // Initialize contextMenu1
            this.niContextMenu.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItemShow, this.menuItemHide, this.menuItemReset, this.menuItemExit });

            // Initialize menuItemShow
            this.menuItemShow.Index = (int)menuItems.Show;
            this.menuItemShow.Text = "S&how";
            this.menuItemShow.Click += new System.EventHandler(this.menuItem_Click);

            // Initialize menuItemHide
            this.menuItemHide.Index = (int)menuItems.Hide;
            this.menuItemHide.Text = "H&ide";
            this.menuItemHide.Click += new System.EventHandler(this.menuItem_Click);

            // Initialize menuItemReset
            this.menuItemReset.Index = (int)menuItems.Reset;
            this.menuItemReset.Text = "R&eset";
            this.menuItemReset.Click += new System.EventHandler(this.menuItem_Click);

            // Initialize menuItemExit
            this.menuItemExit.Index = (int)menuItems.Exit;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItem_Click);

            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            //string version = fvi.ProductVersion;

            AssemblyFileVersionAttribute objVers =
            (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute));

            string SW_Version = String.Empty;
            if (objVers.Version.LastIndexOf('.') != -1)
            {
                int endPos = objVers.Version.LastIndexOf('.');
                SW_Version = objVers.Version.Substring(0, endPos);
            }
            else
                SW_Version = objVers.Version;

            // Set up how the form should be displayed.
            this.Title = String.Format("VLT Equip GateWay Application V{0}", SW_Version);

            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new System.Drawing.Icon(basepath + "Images\\VLT-yellowbg-icon-32.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenu = this.niContextMenu;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "VLT EquipGateWay";
            notifyIcon1.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (IsStarted)// Prevent cancel to close, in case if an instance is already running
            {
                e.Cancel = (System.Windows.MessageBox.Show("are you sure to close the EquipGateway application?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.No);
                if (e.Cancel == false)
                {
                    notifyIcon1.Visible = false;
                    notifyIcon1.Dispose();
                    //foreach (GatewayChannel gwc in _gatewayChannels.Values)
                    //{
                    //    gwc.Stop();
                    //}
                }
            }

        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Minimized:
                    menuItemShow.Visible = true;
                    menuItemHide.Visible = false;
                    break;

                default:
                    menuItemShow.Visible = false;
                    menuItemHide.Visible = true;
                    break;
            }
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // IsStarted will help to make it not possible to cancel close. Needed when already an instance of this application is running
            IsStarted = true;
            // In constructor of MainWindow, the state changed event will not be fired. So do this call in MyWindow_Loaded
            this.WindowState = WindowState.Minimized;
            Console.WriteLine("MyWindow_Loaded");
            //LoadGWCAndStart();
        }

        private void menuItem_Click(object Sender, EventArgs e)
        {
            System.Windows.Forms.MenuItem mi = Sender as System.Windows.Forms.MenuItem;

            if (mi != null)
            {
                switch (mi.Index)
                {
                    case (int)menuItems.Show:
                        Console.WriteLine("menuItem_Click: Show");
                        this.Show();
                        this.WindowState = WindowState.Normal; break;

                    case (int)menuItems.Hide:
                        Console.WriteLine("menuItem_Click: Hide");
                        this.WindowState = WindowState.Minimized; break;

                    case (int)menuItems.Exit:
                        Console.WriteLine("menuItem_Click: Exit");
                        // Try to close the MainWindow, by shutdown the current application.
                        this.Close(); break;

                    case (int)menuItems.Reset:
                        Console.WriteLine("menuItem_Click: Reset");
                        WriteToStatusbar("Restarting the application");
                        //RestartCommunication();
                        break;
                }
            }
        }
        private void notifyIcon_DoubleClick(object Sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == WindowState.Minimized)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            }

            // Activate the form.
            this.Activate();
        }
        private void WriteToStatusbar(string text)
        {
            //StatusText = text;
        }
    }
}
