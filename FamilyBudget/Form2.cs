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
    public partial class Form2 : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FamilyDbN;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataAdapter sqlDataAdapter;
        DataTable Expenses;
        DataTable Incomes;

        BindingSource bs;

        public Form2()
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
    }
}
