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
    public partial class ViewProduct : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                datalistbind();
            }
        }
        private void datalistbind()
        {
            if(Session["category_selected"]!=null)
            {
                int catid = Convert.ToInt32(Session["category_selected"]);
                string str = "select * from Pdt_tab where Cat_Id=" + catid + "";
                DataSet ds = objcls.fn_dataset(str);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
        }
        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            int pdtid = Convert.ToInt32(e.CommandArgument);
            Session["product_selected"] = pdtid;
            Response.Redirect("ViewSinglepdt.aspx");
        }
    }
}