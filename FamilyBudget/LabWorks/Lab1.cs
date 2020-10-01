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
    public partial class Lab1 : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FamilyDbN;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataAdapter sqlDataAdapter;
        DataTable Expenses;
        
        BindingSource bs;

        public Lab1()
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

            //Среднее расходов
            double Sum = 0, Sr;
            foreach (DataRow person in Expenses.Rows)
            {
                Sum = Sum + Convert.ToDouble(person["Amount"]);
            }

            Sr = Sum / Expenses.Rows.Count;
            textBox6.Text = Convert.ToString(Sr);

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
            textBox7.Text = bs.Position.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
            textBox7.Text = bs.Position.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.Position = int.Parse(textBox7.Text);
        }

        private void dataGridView3_CellClick(object sender, EventArgs e)
        {
            var dataRow = (DataRowView)bs.Current;
            textBox4.Text = dataRow["Amount"].ToString();
            textBox5.Text = dataRow["Date"].ToString();
        }
    }
}
