using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Devoir2Azure
{
    public partial class EditProduct1 : UserControl
    {
        public EditProduct1()
        {
            InitializeComponent();
        }

        SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["azure"].ConnectionString);
        List<int> listIds = new List<int>();
        List<int> listCategoryIds = new List<int>();
        private void EditProduct1_Load(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("select ProductId, Name from SalesLT.Product", cnx);
            SqlDataReader reader = null;
            try
            {
                if(cnx.State != ConnectionState.Open)
                    cnx.Open();
                reader = query.ExecuteReader();
                //listIds.Clear();
                //productsCombo.Items.Clear();
                while (reader.Read())
                {
                    int i = reader.GetInt32(0) == null ? 0 : reader.GetInt32(0);
                    string nom = reader.GetString(1) == null ? "" : reader.GetString(1);
                    productsCombo.Items.Add(i + " - " + nom);
                    listIds.Add(i);
                }
                cnx.Close();
                reader.Close();

                query = new SqlCommand("select ProductCategoryId, Name from SalesLT.ProductCategory", cnx);
                try
                {
                    if (cnx.State != ConnectionState.Open)
                        cnx.Open();
                    reader = query.ExecuteReader();
                    //pCategoryCombo.Items.Clear();
                    //listCategoryIds.Clear();
                    while (reader.Read())
                    {
                        listCategoryIds.Add(reader.GetInt32(0));
                        pCategoryCombo.Items.Add(reader.GetString(1));
                    }
                    cnx.Close();
                    reader.Close();
                    reader = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void productsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = listIds[productsCombo.SelectedIndex];
            SqlCommand query = new SqlCommand("Select p.Name, p.ProductNumber, p.Color, pc.Name from SalesLT.Product p join SalesLT.ProductCategory pc on p.ProductCategoryID = pc.ProductCategoryID where p.ProductId =@id", cnx);
            query.Parameters.AddWithValue("id", id);
            SqlDataReader reader = null;
            try
            {
                if(cnx.State != ConnectionState.Open)
                    cnx.Open();
                reader = query.ExecuteReader();
                reader.Read();
                nameTxt.Text = reader.GetString(0) == null ? "" : reader.GetString(0);
                pNumberTxt.Text = reader.GetString(1) == null ? "" : reader.GetString(1);
                pCategoryCombo.SelectedItem = reader.GetString(3) == null ? "" : reader.GetString(3);
                colorTxt.Text = reader.GetString(2) == null ? "" : reader.GetString(2);
                cnx.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                cnx.Close();
                reader.Close();
            }
            
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            int id = listIds[productsCombo.SelectedIndex];
            SqlCommand query = new SqlCommand("UPDATE SalesLT.Product SET Name=@Name, ProductNumber=@ProductNumber, Color=@Color, ProductCategoryID=@ProductCategoryID where ProductId = " + id, cnx);
            query.Parameters.AddWithValue("Name", nameTxt.Text);
            query.Parameters.AddWithValue("ProductNumber", pNumberTxt.Text);
            query.Parameters.AddWithValue("Color", colorTxt.Text);
            query.Parameters.AddWithValue("ProductCategoryID", listCategoryIds[pCategoryCombo.SelectedIndex]);
            try
            {
                if (cnx.State != ConnectionState.Open)
                    cnx.Open();
                query.ExecuteNonQuery();
                cnx.Close();
                MessageBox.Show("Product Changed Succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}