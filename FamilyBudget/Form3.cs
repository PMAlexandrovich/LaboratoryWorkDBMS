using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FamilyBudget
{
    public partial class Form3 : Form
    {
        //Лабораторная 2
        //string filterField = "Name";
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Family;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataAdapter sqlDataAdapter;
        DataTable Incomes;

        public Form3()
        {
            InitializeComponent();
            //Добавление таблицы
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string cmd = "SELECT Id, Amount, Date FROM INCOMES";
            sqlCommand = new SqlCommand(cmd, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            Incomes = new DataTable();
            sqlDataAdapter.Fill(Incomes);
            dataGridView1.DataSource = Incomes;
        }

        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}

        //private void Button2_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}

        //private void Button3_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Amount*0.87 > 5000";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}

        //private void Button4_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Amount = 5000";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}
        //private void Button5_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE FM.Name LIKE N'[К-Р]%'";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}
        //private void Button6_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Id IN (1, 3, 5) ";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}

        //private void Button7_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Date BETWEEN '01.05.2020' AND '01.10.2020' ";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}

        //private void Button8_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Id NOT IN (1, 3, 5)";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}
        ////не работает
        //private void TextBox8_TextChanged(object sender, EventArgs e)
        //{
        //    bs.Filter = filterField + " LIKE '" + textBox1.Text + "%'";
        //}
        //private void Button9_Click(object sender, EventArgs e)
        //{
        //    filterField = "Name";
        //    bs.Filter = filterField + " LIKE '" + textBox1.Text + "%'";
        //    textBox8.Focus();
        //}

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Amount IS NULL";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}

        //private void button11_Click(object sender, EventArgs e)
        //{
        //    sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE (I.Amount IS NOT NULL AND I.Amount > 5000)";
        //    Incomes.Clear();
        //    sqlDataAdapter.Fill(Incomes);
        //}
    }
}
