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
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Family;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataAdapter sqlDataAdapter;
        DataTable Expenses;
        DataTable Incomes;

        BindingSource bs;

        //Лабораторная 2
        string filterField = "Name";

        public Form1()
        {
            InitializeComponent();

            //Первый способ
            ConnectToTypeExpense();
            //Второй способ
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string commandStr = "SELECT E.Id, T.Title, E.Amount, E.Date FROM Expenses AS E JOIN TypeExpense AS T On E.ExpenseType = T.Id";
            sqlCommand = new SqlCommand(commandStr, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            Expenses = new DataTable();
            sqlDataAdapter.Fill(Expenses);
            dataGridView1.DataSource = Expenses;

            //Добавление таблицы на панель Лабораторная 2
            string cmd = "SELECT Id, Amount, Date FROM INCOMES";
            sqlCommand = new SqlCommand(cmd, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            Incomes = new DataTable();
            sqlDataAdapter.Fill(Incomes);
            dataGridView4.DataSource = Incomes;

            //Среднее расходов
            double Sum = 0, Sr;
            foreach (DataRow person in Expenses.Rows)
            {
                Sum = Sum + Convert.ToDouble(person["Amount"]);
            }

            Sr = Sum / Expenses.Rows.Count;
            textBox5.Text = Convert.ToString(Sr);

            //Третий способ
            string ComString = "SELECT I.Id, FM.Name, I.Amount, I.Date  FROM Incomes AS I JOIN FamilyMembers As FM ON I.Earned = FM.Id";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(ComString, connectionString);

            var IncomesTable = new DataTable();
            dataAdapter.Fill(IncomesTable);
            bs = new BindingSource();
            bs.DataSource = IncomesTable;
            dataGridView3.DataSource = bs;
        }

        //Первый способ
        private void ConnectToTypeExpense()
        {
            string ComString = "SELECT * FROM TypeExpense";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(ComString, connectionString);

            var ExpensesTable = new DataTable();
            dataAdapter.Fill(ExpensesTable);
            dataGridView2.DataSource = ExpensesTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Выбор текущей записи
            DataGridViewRow curRow = dataGridView1.CurrentRow;
            textBox1.Text = curRow.Cells["Amount"].Value.ToString();
            textBox2.Text = curRow.Cells["Date"].Value.ToString();

            // Выбор текущей ячейки
            DataGridViewCell curCell = dataGridView1.CurrentCell;
            textBox3.Text = curCell.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
            textBox4.Text = bs.Position.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.Position = int.Parse(textBox4.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
            textBox4.Text = bs.Position.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataRow = (DataRowView)bs.Current;
            textBox7.Text = dataRow["Amount"].ToString();
            textBox6.Text = dataRow["Date"].ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date  FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Amount*0.87 > 5000";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Amount = 5000";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Date LIKE '[К-Р%]'";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Id IN (1, 3, 5)";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Date BETWEEN '5.01.2020' AND '10.01.2020' ";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "SELECT I.Id, FM.Name, I.Amount, I.Date, I.Amount*0.87 AS AmountWithPercent FROM Incomes AS I JOIN FamilyMembers AS FM ON I.Earned = FM.Id WHERE I.Id NOT IN (1, 3, 5)";
            Incomes.Clear();
            sqlDataAdapter.Fill(Incomes);
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            bs.Filter = filterField + " LIKE '" + textBox1.Text + "%'";
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            filterField = "Name";
            bs.Filter = filterField + " LIKE '" + textBox1.Text + "%'";
            textBox8.Focus();
        }
    }
}
