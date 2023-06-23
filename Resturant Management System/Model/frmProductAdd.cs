using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant_Management_System.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }




        public int id = 0;
        public int cID = 0;

        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            string qry = "select catID 'id' , catName 'name' from category";

            MainClass.CBfill(qry, cbCat);
            if (cID > 0)
            {
                cbCat.SelectedValue = cID;
            }

            if (id > 0)
            {
                ForUpdateLoadData();
            }
        }

        string fillPath;
        Byte[] imageByteArray;
        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|* .png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fillPath = ofd.FileName;
                txtImage.Image = new Bitmap(fillPath);
            }
        }

        public virtual void btnSave_Click_1(object sender, EventArgs e) //should be override
        {
            string qry = "";

            if (id == 0)//insert
            {
                qry = "Insert into products Values(@Name, @price, @cat, @img)";
            }
            else //update
            {
                qry = "Update products Set pName = @Name, sPrice=@price, CategoryID=@cat, pImage=@img where pID =@id";
            }

            //image 
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@price", txtPrice.Text);
            ht.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));
            ht.Add("@img", imageByteArray);


            if (MainClass.SQl(qry, ht) > 0)
            {
                MessageBox.Show("Saved successfully");
                id = 0;
                cID = 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = 0;
                cbCat.SelectedIndex = -1;
                txtImage.Image = Resturant_Management_System.Properties.Resources.productPic;  //issue in the productpic
                txtName.Focus();
            }

        }


        private void btnClose_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ForUpdateLoadData()
        {
            string qry = @"Select * from products where pid=" + id + " ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtName.Text = dt.Rows[0]["pPrice"].ToString();

                Byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);
                byte[] imageByteArray = imageArray;
                txtImage.Image=Image.FromStream(new MemoryStream(imageArray));
            }

        }
    }
}
