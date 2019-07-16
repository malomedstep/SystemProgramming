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
        public static AppDomain ad;

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
        }

        private void Load() {
            ad = AppDomain.CreateDomain("ext_appdomain");

            var files = Directory.GetFiles("./ext", "*.dll");

            foreach (var item in files) {
                var asm = ad.Load(AssemblyName.GetAssemblyName(item).FullName);
                var types = asm.GetExportedTypes().Where(t => {
                    return t.IsClass && typeof(IPlugin).IsAssignableFrom(t);
                });
                foreach (var type in types) {
                    var agent = Activator.CreateInstance(type) as IPlugin;
                    modules.Add(agent);
                    var btn = new Button {
                        Content = agent.Name
                    };
                    btn.Click += (s, e) => {
                        converted.Text = agent.Encode(text.Text, 42);
                    };
                    toolBar.Items.Add(btn);
                }
            }
        }
        private void Unload() {
            this.toolBar.Items.Clear();
            modules.Clear();
            AppDomain.Unload(ad);
            ad = null;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e) {
            this.connectButton.IsEnabled = true;
            Unload();
            this.disconnectButton.IsEnabled = false;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e) {
            this.connectButton.IsEnabled = false;
            Load();
            this.disconnectButton.IsEnabled = true;
        }
    }
}
