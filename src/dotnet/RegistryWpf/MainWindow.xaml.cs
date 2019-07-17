using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace RegistryWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var my = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MyAwesomeSoft");
            var str = (string)my.GetValue("config");

            dynamic config = JsonConvert.DeserializeObject(str);
            Height = config.Height;
            Width = config.Width;
            Left = config.Left;
            Top = config.Top;

            //Height = (int)my.GetValue("height", 450);
            //Width = (int)my.GetValue("width", 800);
            //Left = (int)my.GetValue("left", 500);
            //Top = (int)my.GetValue("top", 500);
        }

        private void OnClose(object sender, EventArgs e) {
            var sw = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            var my = sw.CreateSubKey("MyAwesomeSoft", true);
            //my.SetValue("height", (int)Height, RegistryValueKind.DWord);
            //my.SetValue("width", (int)Width, RegistryValueKind.DWord);
            //my.SetValue("left", (int)Left, RegistryValueKind.DWord);
            //my.SetValue("top", (int)Top, RegistryValueKind.DWord);


            my.SetValue("config", JsonConvert.SerializeObject(new {
                Left,
                Top,
                Height,
                Width
            }));

            // fail (no double type in registry)
            //my.SetValue("height", Height);
            //my.SetValue("width", Width);
            //my.SetValue("left", Left);
            //my.SetValue("top", Top);

            my.Close();


            //var software = Registry.LocalMachine.OpenSubKey("");
            //var subfolders = software.GetSubKeyNames();

            //var items = subfolders.Select(sf => $"[{sf}] = {software.GetValue(sf)}");
            //listBox.ItemsSource = items;
        }
    }
}
