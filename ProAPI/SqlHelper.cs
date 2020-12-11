using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;
using System.Configuration;


namespace ProAPI
{
    public class SqlHelper
    {
 
        static public int ExecInsert_Update_Delete(string strSql)
        {
            int i = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                using (conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))//conn = new SqlConnection(ConnectionString)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (cmd = new SqlCommand(strSql, conn))
                    {
                        i = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return i;
        }
      
        
        static public DataTable ExecDataTable(string strSql)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            DataTable dt = null;
            try
            {
                using (conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))//conn = new SqlConnection(ConnectionString)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (cmd = new SqlCommand(strSql, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dt;
        }
        
    }
}