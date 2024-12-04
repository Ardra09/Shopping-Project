using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace Project
{
    public partial class Product : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Bind_Category();
            }
        }
        private void Bind_Category()
        {
            string qry = "select * from Cat_tab";
            DataTable Cat_tab = objcls.fn_exedatatable(qry);
            DropDownList1.DataSource = Cat_tab;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "Cat_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Pls Select a Category", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Visible = true;
            if (DropDownList1.SelectedValue != "0")
            {
                int catid = Convert.ToInt32(DropDownList1.SelectedValue);
                string ph = "~/PdtPhotos/" + FileUpload1.FileName;
                FileUpload1.SaveAs(MapPath(ph));
                string ins = "insert into Pdt_tab values(" + catid + ",'" + TextBox1.Text + "','" + ph + "','" + TextBox2.Text + "','" + TextBox3.Text + "','Available','" + TextBox4.Text + "')";
                int i = objcls.Fn_nonquery(ins);
                if (i > 0)
                {
                    Label1.Text = "Product added";
                }
                else
                {
                    Label1.Text = "Error Occured";
                }
            }
            else
            {
                Label1.Text = "Pls select a valid category";
            }
        }
    }
}