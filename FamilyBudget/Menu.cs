using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyBudget
{
    public partial class Menu : Form
    {
        

        public Menu()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Lab1 form2 = new Lab1();
            form2.Show();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Lab3 lab3 = new Lab3();
            lab3.Show();
        }
        
        private void Button2_Click(object sender, EventArgs e)
        {
            Lab2 form3 = new Lab2();
            form3.Show();
        }
    }
}
