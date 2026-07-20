using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication1.Data
{
    public class DbAccess
    {
        private SqlConnection scon;
        private SqlCommand cmd;
        public DbAccess(string ConStr)
        {
            scon = new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = scon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
        }

        public string CmdText
        {
            get { return cmd.CommandText; }
            set { cmd.CommandText = value; }
        }

        public CommandType CmdType
        {
            get { return cmd.CommandType; }
            set { cmd.CommandType = value; }
        }

        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (scon.State == ConnectionState.Open)
                    scon.Close();
            }
            return dt;
        }
        public DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            try
            {
                if (scon.State == ConnectionState.Closed) ;
                {
                    scon.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (scon.State == ConnectionState.Open)
                    scon.Close();
            }
            return ds;
        }

        public int ExcuteNonQuery()
        {
            int result = 0;
            try
            {
                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (scon.State == ConnectionState.Open)
                    scon.Close();
            }
            return result;
        }

        public object ExecuteScalar()
        {
            try
            {
                if (scon.State == ConnectionState.Closed)
                    scon.Open();

                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (scon.State == ConnectionState.Open)
                    scon.Close();
            }
        }

        public void Dispose()
        {
            if (cmd != null)
                cmd.Dispose();

            if (scon != null)
                scon.Dispose();
        }
    }
}
