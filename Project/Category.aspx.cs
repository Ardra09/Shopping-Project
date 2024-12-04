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
    public partial class Category : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Visible = true;
            string p = "~/CatPhotos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));
            string ins = "insert into Cat_tab values('" + TextBox1.Text + "','" + p + "','" + TextBox2.Text + "','Available')";
            int i = objcls.Fn_nonquery(ins);
            if (i == 1)
            {
                Label1.Text = "Inserted";
            }
        }

    }
}