using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ContractDll;

namespace HostApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        List<IPlugin> modules = new List<IPlugin>();

        private AppDomain appDomain;

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            // appDomain = AppDomain.CreateDomain("ext_appdomain");
        }

        private void Load() {
            appDomain = AppDomain.CreateDomain("ext_appdomain");

            var files = Directory.GetFiles("./ext", "*.dll");

            foreach (var item in files) {
                var asm = appDomain.Load(AssemblyName.GetAssemblyName(item).FullName);
                var types = asm.GetExportedTypes().Where(t => {
                    return t.IsClass && typeof(IPlugin).IsAssignableFrom(t);
                });
                foreach (var type in types) {
                    var plugin = Activator.CreateInstance(type) as IPlugin;
                    modules.Add(plugin);
                    var btn = new Button {
                        Content = plugin.Name
                    };
                    btn.Click += (s, e) => {
                        converted.Text = plugin.Encode(text.Text, 42);
                    };
                    toolBar.Items.Add(btn);
                }
            }
        }
        private void Unload() {
            this.toolBar.Items.Clear();
            modules.Clear();
            AppDomain.Unload(appDomain);
            appDomain = null;
        }


        private void Exit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e) {
            // var asms = appDomain.GetAssemblies();
            this.connectButton.IsEnabled = true;
            Unload();
            this.disconnectButton.IsEnabled = false;
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e) {
            this.connectButton.IsEnabled = false;
            Load();
            this.disconnectButton.IsEnabled = true;
        }

        private void HakunaMatata_Click(object sender, RoutedEventArgs e) {
            var ad = AppDomain.CreateDomain("newProc");
            ad.ExecuteAssembly("TPL_WPF.exe");
        }


        //private void Foo() {


        //}

    }
}
