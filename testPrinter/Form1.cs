using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testPrinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            show(comboBox1.SelectedItem.ToString());
        }
        private void show(string printer)
        {
            try
            {
                MyPrintDocument pd = new MyPrintDocument();
                pd.PrinterSettings.PrinterName = printer;
                pd.QueryPageSettings += Pd_QueryPageSettings;
                pd.Print();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Pd_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            string msg = "HardMarginX:" + e.PageSettings.HardMarginX + "\r\n";
            msg += "HardMarginY:" + e.PageSettings.HardMarginY + "\r\n";
            msg += "PrintableArea.X:" + e.PageSettings.PrintableArea.X + "\r\n";
            msg += "PrintableArea.Y:" + e.PageSettings.PrintableArea.Y + "\r\n";
            msg += "PrintableArea.Width:" + e.PageSettings.PrintableArea.Width + "\r\n";
            msg += "PrintableArea.Height:" + e.PageSettings.PrintableArea.Height + "\r\n";
            this.richTextBox1.Text = msg;
        }

        private class MyPrintDocument : PrintDocument
        {
            protected override void OnPrintPage(PrintPageEventArgs e)
            {
                //base.OnPrintPage(e);
                e.Cancel = true;
            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refresh();
        }
        private void refresh()
        {
            PrintDocument pd = new PrintDocument();
            foreach (string item in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(item);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
