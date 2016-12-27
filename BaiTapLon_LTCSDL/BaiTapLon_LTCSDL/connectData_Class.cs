using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace BaiTapLon_LTCSDL
{
    class connectData_Class
    {
        SqlConnection cnn;
        string cnstr;
        
        public void Connect()
        {
            if (cnn != null && cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }

        }
        public void DisConnect()
        {
            if (cnn != null && cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }

        }
        public DataTable Getdata(string cmd)
        {
            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cnn = new SqlConnection(cnstr);
            try
            {
                
                Connect();
                SqlCommand cmds = new SqlCommand(cmd, cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmds);
                DisConnect();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return null;

            }
        }
        public Boolean execData(string cmd)
        {
            try
            {
                Connect();
                SqlCommand cmds = new SqlCommand(cmd, cnn);
                cmds.ExecuteNonQuery();
                DisConnect();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
