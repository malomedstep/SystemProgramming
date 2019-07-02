using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var wc = new WebClient();
            button1.Enabled = false;

            var th = new Thread(() => {
                Thread.Sleep(1000);
                var text = wc.DownloadString("http://mystat.itstep.org");
                this.Invoke(new MethodInvoker(() =>
                {
                    this.textBox1.Text = text;
                    button1.Enabled = !false;
                }));

                //this.Dispatcher.Invoke(() =>
                //{

                //});

            });
            th.Start();

        }
    }
}
