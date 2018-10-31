using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        contactClass c = new contactClass();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            //Insert data into database using method created 
            bool success = c.Insert(c);
            if (success)
            {
                MessageBox.Show("Contact Interted successfully");
                //Call method 
                Clear();
            }
            else
                MessageBox.Show("Failed to add New Contact");


            //Display data on data grid 
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Method to clear all fields 

        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNo.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get Data from textBoxes

            c.ContactID = int.Parse(txtboxContactID.Text);

            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            //update data in DB 
            bool success = c.Update(c);
            if (success)
            {
                MessageBox.Show("Contact has been updatesd");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call clear method 
                Clear();
            }
            else
                MessageBox.Show("Failed to update");
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from data grid view it to the text boxes 

            // identify the row where moue is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
            


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call clear method 
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get Data from the textboxes 
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);

            bool success = c.Delete(c);

            if (success)
            {
                MessageBox.Show("ContactDeleted");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
                MessageBox.Show("Not Deleted");
        }

       static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Get the value from textBox 
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE'%" + keyword + "%' OR Address LIKE '%" + keyword + "%'",conn );
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
