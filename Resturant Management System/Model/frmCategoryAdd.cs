﻿using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant_Management_System.Model
{
    public partial class frmCategoryAdd : Form
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }
        public int id = 0;
        public virtual void btnSave1_Click(object sender, EventArgs e) //should be override
        {
            string qry = "";

            if(id== 0)//insert
            {
                qry = "Insert into category Values(@Name)";
            }
            else //update
            {
                qry = "Update category Set catName = @Name where catID =@id";
            }
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if (MainClass.SQl(qry, ht) > 0)
            {
                MessageBox.Show("Saved successfully");
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
