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
    public partial class EditProduct : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {
            string str = "select * from Pdt_tab";
            DataSet ds = objcls.fn_dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtname = (TextBox)GridView1.Rows[i].Cells[3].Controls[0];
            TextBox txtdescription = (TextBox)GridView1.Rows[i].Cells[5].Controls[0];
            TextBox txtprice = (TextBox)GridView1.Rows[i].Cells[6].Controls[0];
            TextBox txtstock = (TextBox)GridView1.Rows[i].Cells[8].Controls[0];
            FileUpload fileUpload = (FileUpload)GridView1.Rows[i].FindControl("FileUpload2");
            string upd = "update Pdt_tab set Name='" + txtname.Text + "',Description='" + txtdescription.Text + "',Price=" + txtprice.Text + ",Stock=" + txtstock.Text + "";
            if (fileUpload != null && fileUpload.HasFile)
            {
                string path1 = "~/PdtPhotos/" + fileUpload.FileName;
                fileUpload.SaveAs(MapPath(path1));

                upd += ", Image = '" + path1 + "'";
            }

            upd += " where Pdt_Id = " + getid;
            int str = objcls.Fn_nonquery(upd);
            GridView1.EditIndex = -1;
            BindGrid();
        }
    }
}