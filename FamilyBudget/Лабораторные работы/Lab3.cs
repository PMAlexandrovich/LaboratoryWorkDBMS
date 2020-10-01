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
    public partial class Lab3 : Form
    {
        SqlDataAdapter dataAdapter;
        SqlDataAdapter secondAdapter;

        DataTable Incomes;
        DataTable Members;

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FamilyDbN;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Lab3()
        {
            InitializeComponent();

            Connect();
        }

        private void Connect()
        {
            string ComString = "SELECT I.Id, I.Amount, I.Date, FM.Id As FamMamId, FM.Name  FROM Incomes AS I JOIN FamilyMembers As FM ON I.Earned = FM.Id";

            dataAdapter = new SqlDataAdapter(ComString, connectionString);

            Incomes = new DataTable();
            dataAdapter.Fill(Incomes);
            dataGridView1.DataSource = Incomes;



            string strCmd = "SELECT * FROM FamilyMembers";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            var comm = new SqlCommand(strCmd, sqlConnection);
            secondAdapter = new SqlDataAdapter(comm);
            Members = new DataTable();
            secondAdapter.Fill(Members);
            dataGridView2.DataSource = Members;
            SqlCommandBuilder builder = new SqlCommandBuilder(secondAdapter);

        }

        //Insert
        private void button1_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog(this);
            addForm.Dispose();
            string addStr = $"INSERT INTO Incomes (Amount, Date, Earned) VALUES ({addForm.Amount}, '{addForm.Date.ToString("yyyyMMdd")}', {addForm.FamMamberId})";
            MessageBox.Show(addStr);
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand addComm = new SqlCommand(addStr,conn);
                addComm.ExecuteNonQuery();

                // Вывод данных обновленной таблицы
                Incomes.Clear();
                dataAdapter.Fill(Incomes);
                conn.Close();
            }
        }

        //Update
        private void button2_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm();

            editForm.Id = (int)dataGridView1.SelectedCells[0].OwningRow.Cells["Id"].Value;
            editForm.Amount = (double)(decimal)dataGridView1.SelectedCells[0].OwningRow.Cells["Amount"].Value;
            editForm.FamMamberId = (int)dataGridView1.SelectedCells[0].OwningRow.Cells["FamMamId"].Value;
            editForm.Date = (DateTime)dataGridView1.SelectedCells[0].OwningRow.Cells["Date"].Value;

            editForm.ShowDialog(this);

            editForm.Dispose();
            string editStr = $"UPDATE Incomes SET Amount = {editForm.Amount}, Date = '{editForm.Date.ToString("yyyy-MM-ddTHH:mm:ss")}', Earned = {editForm.FamMamberId} WHERE Id = {editForm.Id}";
            MessageBox.Show(editStr);
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand editComm = new SqlCommand(editStr, conn);
                editComm.ExecuteNonQuery();

                // Вывод данных обновленной таблицы
                Incomes.Clear();
                dataAdapter.Fill(Incomes);
                conn.Close();
            }
        }

        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            var id = (int)dataGridView1.SelectedCells[0].OwningRow.Cells["Id"].Value;

            string delStr = $"DELETE FROM Incomes WHERE Id = {id}";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand editComm = new SqlCommand(delStr, conn);
                editComm.ExecuteNonQuery();

                // Вывод данных обновленной таблицы
                Incomes.Clear();
                dataAdapter.Fill(Incomes);
                conn.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog(this);
            addForm.Dispose();
            string addStr = $"INSERT INTO Incomes (Amount, Date, Earned) VALUES (@Amount, @Date, @FMID)";
            MessageBox.Show(addStr);
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand addComm = new SqlCommand(addStr, conn);

                addComm.Parameters.Add("@Amount", SqlDbType.Decimal);
                addComm.Parameters.Add("@Date", SqlDbType.DateTime);
                addComm.Parameters.Add("@FMID", SqlDbType.Int);

                addComm.Parameters["@Amount"].Value = addForm.Amount;
                addComm.Parameters["@Date"].Value = addForm.Date;
                addComm.Parameters["@FMID"].Value = addForm.FamMamberId;

                addComm.ExecuteNonQuery();

                // Вывод данных обновленной таблицы
                Incomes.Clear();
                dataAdapter.Fill(Incomes);
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm();

            editForm.Id = (int)dataGridView1.SelectedCells[0].OwningRow.Cells["Id"].Value;
            editForm.Amount = (double)(decimal)dataGridView1.SelectedCells[0].OwningRow.Cells["Amount"].Value;
            editForm.FamMamberId = (int)dataGridView1.SelectedCells[0].OwningRow.Cells["FamMamId"].Value;
            editForm.Date = (DateTime)dataGridView1.SelectedCells[0].OwningRow.Cells["Date"].Value;

            editForm.ShowDialog(this);

            editForm.Dispose();
            string editStr = $"UPDATE Incomes SET Amount = @Amount, Date = @Date, Earned = @FMID WHERE Id = @Id";
            MessageBox.Show(editStr);

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand editComm = new SqlCommand(editStr, conn);

                editComm.Parameters.Add("@Id", SqlDbType.Int);
                editComm.Parameters.Add("@Amount", SqlDbType.Decimal);
                editComm.Parameters.Add("@Date", SqlDbType.DateTime);
                editComm.Parameters.Add("@FMID", SqlDbType.Int);

                editComm.Parameters["@Id"].Value = editForm.Id;
                editComm.Parameters["@Amount"].Value = editForm.Amount;
                editComm.Parameters["@Date"].Value = editForm.Date;
                editComm.Parameters["@FMID"].Value = editForm.FamMamberId;

                editComm.ExecuteNonQuery();

                // Вывод данных обновленной таблицы
                Incomes.Clear();
                dataAdapter.Fill(Incomes);
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int i = 0;
            string str;
            foreach (DataRow person in Members.Rows)
            {
                i = i + 1;
                str = i.ToString() + ": ";
                str = str + person.RowState.ToString();
                listBox1.Items.Add(str);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            secondAdapter.Update(Members);
        }
    }
}
