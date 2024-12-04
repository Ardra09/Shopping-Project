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
    public partial class ViewSinglepdt : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        //public string FormatPrice(decimal price)
        //{
        //    return string.Format("₹{0:N2}", price);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["product_selected"] != null)
                {
                    int pdtid = Convert.ToInt32(Session["product_selected"]);
                    Loadpdt(pdtid);
                }
            }
        }
        private void Loadpdt(int pdtid)
        {
            string sel = "select * from Pdt_tab where Pdt_Id=" + pdtid + "";
            SqlDataReader dr = objcls.fn_exeReader(sel);
            while (dr.Read())
            {
                Label1.Text = dr["Name"].ToString();
                decimal Price = Convert.ToDecimal(dr["Price"]);
                //Label3.Text = FormatPrice(Price);
                Label2.Text = Price.ToString("C");
                Label3.Text = dr["Description"].ToString();
                Image1.ImageUrl = dr["Image"].ToString();
            }
            dr.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Cart_Id) from Cart_tab";
            string maxcartid = objcls.Fn_scalar(sel);
            int cart_id = 0;
            if (maxcartid == "")
            {
                cart_id = 1;
            }
            else
            {
                int newcartid = Convert.ToInt32(maxcartid);
                cart_id = newcartid + 1;
            }
            int pdtid = Convert.ToInt32(Session["product_selected"]);
            int quantity = Convert.ToInt32(DropDownList1.SelectedItem.Text);
            string qryPrice = "select Price from Pdt_tab where Pdt_Id=" + pdtid + "";
            SqlDataReader pricedr = objcls.fn_exeReader(qryPrice);
            decimal price = 0;
            if (pricedr.Read())
            {
                price = Convert.ToDecimal(pricedr["Price"]);
            }
            pricedr.Close();
            decimal TotalPrice = quantity * price;
            int userid = Convert.ToInt32(Session["userid"]);
            string ins = "insert into Cart_tab values(" + cart_id + "," + pdtid + "," + userid + ","+quantity+"," + TotalPrice + ")";
            int result = objcls.Fn_nonquery(ins);
            if (result == 1)
            {
                Label4.Visible = true;
                Label4.Text = "Product successfully Added";
            }
        }
    }
}







