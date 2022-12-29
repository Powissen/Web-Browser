using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace WebTest
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint ="CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEllipse
            );

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitBrowser();
            progressBar1.ForeColor = Color.Gray;
            progressBar1.Style = ProgressBarStyle.Continuous;
        }
        private async Task initizated()
        {
            await webView.EnsureCoreWebView2Async(null);
        }
        public async void InitBrowser()
        {
            await initizated();
            webView.CoreWebView2.Navigate("https://www.google.com/");
        }




        private void pictureBox3_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.Navigate("https://www.google.com/");
            textBox1.Text = "https://www.google.com/";
            defaultScreen.Visible = true;
            defaultScreenSearch.Visible = true;
            defaultScreenSearch.ForeColor = Color.DimGray;
            defaultScreenSearch.Text = "Search or Type URL";
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            webView.GoForward();
            LoadingAnimation();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            webView.GoBack();
            LoadingAnimation();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.StartsWith("http") && textBox1.Text.Contains(".") || textBox1.Text.StartsWith("https") && textBox1.Text.Contains("."))
                {
                    webView.CoreWebView2.Navigate(textBox1.Text);
                }
                else if (textBox1.Text.Contains("."))
                {
                    int count = 0;
                    foreach (char i in textBox1.Text)
                    {
                        if (i == '.')
                            count++;
                    }
                    if (count == 1)
                    {
                        webView.CoreWebView2.Navigate("https://" + textBox1.Text);
                        textBox1.Text = "https://" + textBox1.Text;
                    }
                    else
                    {
                        webView.CoreWebView2.Navigate("https://www.google.com/search?q=" + textBox1.Text);
                        textBox1.Text = "https://www.google.com/search?q=" + textBox1.Text;
                    }
                }
                else
                {
                    webView.CoreWebView2.Navigate("https://www.google.com/search?q=" + textBox1.Text);
                    textBox1.Text = "https://www.google.com/search?q=" + textBox1.Text;
                }

                LoadingAnimation();
            }
        }
        
        private void defaultScreenSearch_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (defaultScreenSearch.Text.StartsWith("http") && defaultScreenSearch.Text.Contains(":") || defaultScreenSearch.Text.StartsWith("https") && defaultScreenSearch.Text.Contains(":"))
                {
                    webView.CoreWebView2.Navigate(defaultScreenSearch.Text);
                    textBox1.Text = defaultScreenSearch.Text;
                }
                else if (defaultScreenSearch.Text.Contains("."))
                {
                    int count = 0;
                    foreach (char i in defaultScreenSearch.Text)
                    {
                        if (i == '.')
                            count++;
                    }
                    if (count == 1)
                    {
                        webView.CoreWebView2.Navigate("https://" + defaultScreenSearch.Text);
                        textBox1.Text = "https://" + defaultScreenSearch.Text;
                    }
                    else
                    {
                        webView.CoreWebView2.Navigate("https://www.google.com/search?q=" + defaultScreenSearch.Text);
                        textBox1.Text = "https://www.google.com/search?q=" + defaultScreenSearch.Text;
                    }
                }
                else
                {
                    webView.CoreWebView2.Navigate("https://www.google.com/search?q=" + defaultScreenSearch.Text);
                    textBox1.Text = "https://www.google.com/search?q=" + defaultScreenSearch.Text;
                }
                textBox1.SelectionStart = 0;

                LoadingAnimation();

                defaultScreen.Visible = false;
                defaultScreenSearch.Visible = false;
            }
        }

        private void defaultScreenSearch_MouseClick(object sender, MouseEventArgs e)
        {
            defaultScreenSearch.ForeColor = Color.Black;
            defaultScreenSearch.Text = "";
        }

        void LoadingAnimation()
        {
            progressBar1.Visible = true;
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(2);
            }
            progressBar1.Visible = false;
        }
    }
}
