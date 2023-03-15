using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VersionControl_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Resource1.LastName; // label1
            textBox2.Text = Resource1.FirstName; // label2
            button1.Text = Resource1.Add; // button1
        }
    }
}
