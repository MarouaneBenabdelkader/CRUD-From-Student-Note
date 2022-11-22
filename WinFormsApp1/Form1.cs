using MySql.Data.MySqlClient;
using System.Data;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        static string server = "localhost";
        static string database = "etudiant";
        static string username = "root";
        static string password = "";
        static string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
        static MySqlConnection conx = new MySqlConnection(constring);
        static MySqlCommand cmd = new MySqlCommand();
        static MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        private int index;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void ComoboxFill()
        {
            conx.Open();
            cmd.CommandText = "select * from etudiant;";
            cmd.Connection = conx;
            DataTable data = new DataTable();
            adapter.Fill(data);
            comboBox.DataSource = data;
            comboBox.ValueMember = "CNE";
            comboBox.DisplayMember = "CNE";
            conx.Close();
        }

        private void InitialCase()
        {
            this.Text = "Intial";

            ComoboxFill();
            // Default state of comBox
            comboBox.SelectedItem = null;
            comboBox.SelectedText = "Click to select";

            // bloquer les bouttons

            BtnCancel.Enabled = false;
            BtnDelete.Enabled = false;
            BtnInsert.Enabled = false;
            BtnSave.Enabled = false;
            BtnDelete.Enabled = false;
            BtnUpdate.Enabled = false;
            
        }

        private void Selection()
        {
            if (comboBox.SelectedItem == null)
            {
                this.Text = "Intial Case Base de donnee Empty";
            }
            else
            {
                this.Text = "Select case";
            }

            this.index = comboBox.SelectedIndex;

            comboBox.Enabled = true;
            TxtNote1.Enabled = false;
            BtnInsert.Enabled = true;
            BtnUpdate.Enabled = true;
            BtnSave.Enabled = false;
            BtnDelete.Enabled = true;
            BtnCancel.Enabled = false;

        }

        private void Insert()
        {
            this.Text = "Insertion";
            TxtNote1.Text = "";

            comboBox.Enabled = false;
            TxtNote1.Enabled = true;
            BtnCancel.Enabled = true;
            BtnSave.Enabled = true;
            BtnDelete.Enabled = false;
            BtnInsert.Enabled = false;
            BtnUpdate.Enabled = false;
        }

        private void Uppdate()
        {
            this.Text = "Updating";

            comboBox.Enabled = false;
            TxtNote1.Enabled = true;
            BtnCancel.Enabled = true;
            BtnSave.Enabled = true;
            BtnDelete.Enabled = false;
            BtnInsert.Enabled = false;
            BtnDelete.Enabled = false;

        }

        private void Delete()
        {
            this.Text = "Deleting";
            comboBox.Enabled = false;
            TxtNote1.Enabled = false;
            BtnCancel.Enabled = true;
            BtnSave.Enabled = true;
            BtnDelete.Enabled = false;
            BtnInsert.Enabled = false;
            BtnDelete.Enabled = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitialCase();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uppdate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.Text.Equals("Insertion"))
            {
                conx.Open();
                cmd.Connection = conx;
                cmd.CommandText = "insert into etudiant(CNE,Note1) values ('"+ comboBox.Text +"','" + TxtNote1.Text + "')";
                cmd.ExecuteNonQuery();
                conx.Close();
                ComoboxFill();
                Selection();
                TxtNote1.Text = string.Empty;
                comboBox.SelectedIndex = comboBox.Items.Count - 1;
            }else if (this.Text.Equals("Updating"))
            {
                conx.Open();
                cmd.Connection = conx;
                cmd.CommandText = "update etudiant set Note1 = '" + TxtNote1.Text + "' where CNE = '" + comboBox.Text + "'";
                cmd.ExecuteNonQuery(); 
                conx.Close();

                ComoboxFill();
                Selection();
                TxtNote1.Text = string.Empty;
                comboBox.SelectedIndex = this.index;
            }else if (this.Text.Equals("Deleting"))
            {
                if(MessageBox.Show("Are you sure to delete ?","Delete Etudiant",MessageBoxButtons.YesNo) == DialogResult.Yes) { 
                    conx.Open();
                    cmd.Connection = conx;
                    cmd.CommandText = "delete from etudiant where CNE = '" + comboBox.Text + "';";
                    cmd.ExecuteNonQuery();
                    conx.Close();
                    MessageBox.Show("Seccessfully Deleted");
                    ComoboxFill();
                    Selection();
                }
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selection();
            TxtNote1.Text= comboBox.Text;
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            Insert();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (this.Text.Equals("Insertion"))
            {
                InitialCase();
            }else if(this.Text.Equals("Updating") || this.Text.Equals("Deleting"))
            {
                Selection();

            }
        }
    }
}