using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Devoir2Azure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fillDataGrid(int id)
        {
            tabPage2.Controls.Clear();
            if (id == 1)
            {
                dataGrid1.Rows.Clear();
                dataGrid1.Columns.Clear();
                dataGrid1.Columns.Add("ProductId", "Product Id");
                dataGrid1.Columns.Add("Name", "Name");
                dataGrid1.Columns.Add("ProductNumber", "Product Number");
                dataGrid1.Columns.Add("Color", "Color");
                dataGrid1.Columns.Add("ProductCategory", "Product Category");
                tabPage2.Controls.Add(new EditProduct1());
                tabPage2.Controls[0].Dock = DockStyle.Fill;
            }
            else if(id == 2)
            {
                dataGrid1.Rows.Clear();
                dataGrid1.Columns.Clear();
                dataGrid1.Columns.Add("CustomerId", "Customer Id");
                dataGrid1.Columns.Add("NameStyle", "Name Style");
                dataGrid1.Columns.Add("Title", "Title");
                dataGrid1.Columns.Add("FisrtName", "Fisrt Name");
                dataGrid1.Columns.Add("LastName", "Last Name");
                dataGrid1.Columns.Add("CompanyName", "Company Name");
                dataGrid1.Columns.Add("SalesPerson", "Sales Person");
                dataGrid1.Columns.Add("EmailAddress", "Email Address");
                dataGrid1.Columns.Add("Phone", "Phone");
            }
            else if (id == 3)
            {
                dataGrid1.Rows.Clear();
                dataGrid1.Columns.Clear();
                dataGrid1.Columns.Add("ProductCategotyId", "Product Categoty Id");
                dataGrid1.Columns.Add("Name", "Name");
            }
        }

        SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["azure"].ConnectionString);

        private void ProductBtn_Click(object sender, EventArgs e)
        {
            fillDataGrid(1);
            SqlCommand query = new SqlCommand("Select p.ProductID, p.Name, p.ProductNumber, p.Color, pc.Name from SalesLT.Product p join SalesLT.ProductCategory pc on p.ProductCategoryID = pc.ProductCategoryID", cnx);
            SqlDataReader reader = null;
            try
            {
                if(cnx.State != ConnectionState.Open)
                    cnx.Open();
                reader = query.ExecuteReader();
                while(reader.Read())
                {
                    int ID = reader.GetInt32(0) == null ? 0 : reader.GetInt32(0);
                    string name = reader.GetString(1) == null ? "" : reader.GetString(1);
                    string ProductNumber = reader.GetString(2) == null ? "" : reader.GetString(2);
                    string Color = reader.GetString(3) == null ? "" : reader.GetString(3);
                    string Category = reader.GetString(4) == null ? "" : reader.GetString(4);

                    dataGrid1.Rows.Add(ID, name, ProductNumber, Color, Category);
                }
                cnx.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                    reader.Close();
                }
            }
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            fillDataGrid(2);
            SqlCommand query = new SqlCommand("Select CustomerID, NameStyle, Title, FirstName, LastName, CompanyName,SalesPerson, EmailAddress, Phone from SalesLT.Customer", cnx);
            SqlDataReader reader = null;
            try
            {
                if (cnx.State != ConnectionState.Open)
                    cnx.Open();
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    int CustomerID = reader.GetInt32(0) == null ? 0 : reader.GetInt32(0);
                    Boolean NameStyle = reader.GetBoolean(1);
                    string Title = reader.GetString(2) == null ? "" : reader.GetString(2);
                    string FirstName = reader.GetString(3) == null ? "" : reader.GetString(3);
                    string LastName = reader.GetString(4) == null ? "" : reader.GetString(4);
                    string CompanyName = reader.GetString(5) == null ? "" : reader.GetString(5);
                    string SalesPerson = reader.GetString(6) == null ? "" : reader.GetString(6);
                    string EmailAddress = reader.GetString(7) == null ? "" : reader.GetString(7);
                    string Phone = reader.GetString(8) == null ? "" : reader.GetString(8);

                    dataGrid1.Rows.Add(CustomerID, NameStyle, Title, FirstName, LastName, CompanyName, SalesPerson, EmailAddress, Phone);
                }
                cnx.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                    reader.Close();
                }
            }
        }

        private void PCategoryBtn_Click(object sender, EventArgs e)
        {
            fillDataGrid(3);
            SqlCommand query = new SqlCommand("Select ProductCategoryID, Name from SalesLT.ProductCategory", cnx);
            SqlDataReader reader = null;
            try
            {
                if (cnx.State != ConnectionState.Open)
                    cnx.Open();
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    int ProductCategoryID = reader.GetInt32(0) == null ? 0 : reader.GetInt32(0);
                    string Name = reader.GetString(1) == null ? "" : reader.GetString(1);

                    dataGrid1.Rows.Add(ProductCategoryID, Name);
                }
                cnx.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                    reader.Close();
                }
            }
        }
    }
}
