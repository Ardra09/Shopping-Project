using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace Project
{
    public class ConClass
    {
        SqlConnection con;
        SqlCommand cmd;
        public ConClass()
        {
            con = new SqlConnection(@"server=DESKTOP-DID7AQD\SQLEXPRESS;database=ProjectDB;Integrated Security=true");
        }
        public int Fn_nonquery(string sql)//select,delete,update
        {
          
            cmd = new SqlCommand(sql, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //con.Open(); 
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public string Fn_scalar(string sql)//scalar function
        {
            cmd = new SqlCommand(sql, con);
            con.Open();
            string s = cmd.ExecuteScalar().ToString();
            con.Close();
            return s;
        }
        public SqlDataReader fn_exeReader(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataSet fn_dataset(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataTable fn_exedatatable(string sqlquery)//select
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}