﻿using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace Duplicate_Cleaner_Pro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        bool beginMove = false;//初始化鼠标位置  
        int currentXPosition;
        int currentYPosition;

        private void Label2_Click(object sender, EventArgs e) => Close();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标  
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标  
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x  
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标  
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态  
                currentYPosition = 0;
                beginMove = false;
            }
        }

        /*private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
            string hostspath = @"C:\WINDOWS\system32\drivers\etc\hosts";
            File.SetAttributes(hostspath, FileAttributes.Normal);
            FileStream hosts = new FileStream(hostspath, FileMode.Append);
            StreamWriter sw = new StreamWriter(hosts);
            sw.WriteLine("127.0.0.1    platform.wondershare.com");
            sw.WriteLine("127.0.0.1    support.wondershare.net");
            sw.WriteLine("127.0.0.1    cbs.wondershare.com");
            sw.WriteLine("127.0.0.1    www.wondershare.net");
            sw.Close();
            hosts.Close();
            MessageBox.Show("屏蔽成功！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("https://www.muruoxi.com/");
            }
            catch
            {
                MessageBox.Show("屏蔽失败！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }*/

        private void Button2_Click(object sender, EventArgs e)
        {
            
            RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
           
                try { 
                if (Environment.Is64BitOperatingSystem) {
                    var.path = reg.OpenSubKey(@"SOFTWARE\WOW6432Node\Wondershare\Wondershare PDFelement 6 Pro").GetValue("appsetuppath").ToString();
                }
                else
                {
                    var.path = reg.OpenSubKey(@"SOFTWARE\Wondershare\Wondershare PDFelement 6 Pro").GetValue("appsetuppath").ToString();
                }
            }
            catch
            {
                MessageBox.Show("请先安装PDFelement 6 Pro产品！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reg.Close();
            }

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
            FileStream fs;
            BinaryWriter bw;
            byte[] temp;

            System.IO.Stream dll1 = asm.GetManifestResourceStream("PDFelement_6_Pro.Resources.PEStudio.PDFElement.Base.dll");
            temp = new byte[dll1.Length];
            dll1.Read(temp, 0, temp.Length);
            dll1.Seek(0, SeekOrigin.Begin);
            
            try
            {
                fs = new FileStream(var.path + @"\PEStudio.PDFElement.Base.dll", FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(temp);
                fs.Close();
                bw.Close();
            }
            catch
            {
                MessageBox.Show("设置失败！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            System.IO.Stream dll2 = asm.GetManifestResourceStream("PDFelement_6_Pro.Resources.ProductAuthor.dll");
            temp = new byte[dll2.Length];
            dll2.Read(temp, 0, temp.Length);
            dll2.Seek(0, SeekOrigin.Begin);
            
            try
            {
                fs = new FileStream(var.path + @"\ProductAuthor.dll", FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(temp);
                fs.Close();
                bw.Close();
            }
            catch
            {
                MessageBox.Show("设置失败！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            } 

            MessageBox.Show("设置成功！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("https://www.muruoxi.com/");
        }

       /* private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
            string hostspath = @"C:\WINDOWS\system32\drivers\etc\hosts";
            File.SetAttributes(hostspath, FileAttributes.Normal);
            FileStream hosts = new FileStream(hostspath, FileMode.Open,FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(hosts,Encoding.GetEncoding("gb2312"));
            StreamReader sr = new StreamReader(hosts,Encoding.GetEncoding("gb2312"));
            string contents = sr.ReadToEnd();
            contents = Regex.Replace(contents, @"127.0.0.1\s*platform.wondershare.com", "", RegexOptions.IgnoreCase);
            contents = Regex.Replace(contents, @"127.0.0.1\s*support.wondershare.net", "", RegexOptions.IgnoreCase);
            contents = Regex.Replace(contents, @"127.0.0.1\s*cbs.wondershare.com", "", RegexOptions.IgnoreCase);
            contents = Regex.Replace(contents, @"127.0.0.1\s*www.wondershare.net", "", RegexOptions.IgnoreCase);
            hosts.SetLength(0);
            sw.Write(contents);
            sw.Close();
            hosts.Close();
            MessageBox.Show("恢复成功！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("https://www.muruoxi.com/");
            }
            catch
            {
                MessageBox.Show("恢复失败！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public class var {
            public static string path;
        }
        
    }
}
