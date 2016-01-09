using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;

namespace CsharpBattleship
{
    public partial class MainForm : Form
    {
        ArrayList forms;


        public MainForm()
        {
            InitializeComponent();
            forms = new ArrayList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 temp = new Form1((forms.Count+1).ToString());
            temp.Show();
            forms.Add(temp);
        }
    }
}
