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
    public partial class Viewbill : System.Web.UI.Page
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
            int userid = Convert.ToInt32(Session["userid"]);
            string str = "select Order_Id,Pdt_Id,Quantity,Total_Price from Order_tab where User_Id=" + userid + "";
            DataSet ds = objcls.fn_dataset(str);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            decimal grandTotal = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                grandTotal += Convert.ToDecimal(row["Total_Price"]);
            }
            Label2.Text = grandTotal.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            ServiceReference7.ServiceClient obj = new ServiceReference7.ServiceClient();
            decimal amountToPay = Convert.ToDecimal(Label2.Text);
            string userId = Convert.ToString(Session["userid"]);
            string paymentStatus = obj.Processpayment(userId, amountToPay);

            if (paymentStatus.Contains("Payment successful"))
            {
                string updOrder = "UPDATE Order_tab SET Status = 'Paid' WHERE User_Id = " + userId + " AND Status = 'Pending'";
                int orderUpdateResult = objcls.Fn_nonquery(updOrder);

                if (orderUpdateResult > 0)
                {
                    string selectOrderDetails = "SELECT Pdt_Id, Quantity FROM Order_tab WHERE User_Id = " + userId + " AND Status = 'Paid'";
                    DataTable orderDetails = objcls.fn_exedatatable(selectOrderDetails);

                    foreach (DataRow row in orderDetails.Rows)
                    {
                        int proId = Convert.ToInt32(row["Pdt_Id"]);
                        int quantityPurchased = Convert.ToInt32(row["Quantity"]);
                        string updateStock = "UPDATE Pdt_tab SET Stock = Stock - " + quantityPurchased + " WHERE Pdt_Id = " + proId;
                        objcls.Fn_nonquery(updateStock);
                    }

                    lblPaymentStatus.Text = "Payment Done successfully!";

                }
                else
                {
                    lblPaymentStatus.Text = "Payment successful, but no 'Ordered' records found to update.";
                }
            }
            else
            {
                lblPaymentStatus.Text = paymentStatus;
            }
        }
    }
}