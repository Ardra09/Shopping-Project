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
    public partial class ViewCart : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Bindgrid();
            }
        }
        public void Bindgrid()
        {
            int userid = Convert.ToInt32(Session["userid"]);
            string str="select * from Cart_tab where User_Id="+userid+"";
            DataSet ds = objcls.fn_dataset(str);
            if (ds.Tables[0].Rows.Count>0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                Label1.Visible = true;
                Label1.Text="Your Cart is Empty";
                GridView1.Visible = false;
            }
        }

      

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Bindgrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex =-1;
            Bindgrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowindex = e.RowIndex;
            int cart_id = Convert.ToInt32(GridView1.DataKeys[rowindex].Values["Cart_Id"]);
            int pdtid = Convert.ToInt32(GridView1.DataKeys[rowindex].Values["Pdt_Id"]);
            TextBox txtQuantity = (TextBox)GridView1.Rows[rowindex].Cells[4].Controls[0];
            int quantity = Convert.ToInt32(txtQuantity.Text); // Convert directly
            int userid = Convert.ToInt32(Session["userid"]);
            string pdtQuery = "select Price from Pdt_tab where Pdt_Id = " + pdtid + "";
            decimal productPrice = Convert.ToDecimal(objcls.Fn_scalar(pdtQuery));
            decimal totalPrice = productPrice * quantity;
            string updQuery = "update Cart_tab SET Quantity = " + quantity + ", Total_Price = " + totalPrice + " where Cart_Id = " + cart_id + " and User_Id = " + userid + "";
            int result = objcls.Fn_nonquery(updQuery);

            if (result > 0)
            {
                Label1.Text = "Cart updated successfully.";
            }
            else
            {
                Label1.Text = "Failed to update cart.";
            }
            GridView1.EditIndex = -1;
            Bindgrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int cart_id = Convert.ToInt32(GridView1.DataKeys[i].Values["Cart_Id"]);
            string del = "delete from Cart_tab where Cart_Id=" + cart_id + "";
            int result = objcls.Fn_nonquery(del);
            if (result > 0)
            {
                Label1.Text = "Cart Deleted successfully.";
            }
            else
            {
                Label1.Text = "Failed to Delete cart.";
            }
            Bindgrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            int userid = Convert.ToInt32(Session["userid"]);
            string sel = "select max(Cart_Id) from Cart_tab where User_Id="+userid+"";
            int maxcartid = Convert.ToInt32(objcls.Fn_scalar(sel));
            for (int i = 1; i <= maxcartid; i++)
            {
                string s = "select * from Cart_tab where Cart_Id="+i+ " and  User_Id=" +userid+ "";
                SqlDataReader dr = objcls.fn_exeReader(s);
                int pdtid = 0;
               // int userid = 0;
                int quantity = 0;
                decimal price = 0;
                while (dr.Read())
                {
                     pdtid = Convert.ToInt32(dr["Pdt_Id"]);
                    //userid = Convert.ToInt32(dr["User_Id"]);
                    quantity = Convert.ToInt32(dr["Quantity"]);
                     price = Convert.ToDecimal(dr["Total_Price"]);
                }
                dr.Close();
                
                string ins = "insert into Order_tab values(" + pdtid + "," + userid + "," + quantity + "," + price + ",'" + sdate + "','Pending')";
                int result = objcls.Fn_nonquery(ins);
                string del = "delete from Cart_tab where Cart_Id="+i + " and  User_Id=" + userid + "";
                int result1 = objcls.Fn_nonquery(del);
            }
           // string date = DateTime.Now.ToString("yyyy-MM-dd");
            string grandtotal = "select Sum(Total_Price) from Order_tab where User_Id=" + userid + " and Status='Pending'";
            string grand_total = objcls.Fn_scalar(grandtotal);
            string insert = "insert into Bill_tab values("+userid+ ",'" + sdate + "'," + grand_total + ")";
            int ress = objcls.Fn_nonquery(insert);
            Response.Redirect("Viewbill.aspx");
        }
    }
}
