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

namespace FamilyBudget.Лабораторные_работы
{
    public partial class AddForm : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FamilyDbN;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public int FamMamberId { get; set; }

        public AddForm()
        {
            InitializeComponent();

            string ComString = "SELECT Id, Name  FROM FamilyMembers As FM";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(ComString, connectionString);

            var FM = new DataTable();
            dataAdapter.Fill(FM);

            comboBox1.DataSource = FM;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Amount = double.Parse(textBox1.Text);
            FamMamberId = (int)comboBox1.SelectedValue;
            Date = dateTimePicker1.Value.ToUniversalTime();

            Close();
        }
    }
}
