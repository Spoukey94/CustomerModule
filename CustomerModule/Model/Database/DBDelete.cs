using CustomerModule.LogClass;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CustomerModule.Model.DatabaseClass
{
    class DBDelete : IDelete
    {
        private IConnect _connect;
        private IWrite _write;
        public DBDelete(IConnect connect,IWrite write)
        {
            this._connect = connect;
            this._write = write;
        }

        public void Delete(Customer.Customer cus)
        {
            try
            {
                using (SqlConnection con = _connect.ConnectToDatabase())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = con;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 1200;
                        command.CommandText = @"dbo.sp_Delete_Customer";

                        command.Parameters.AddWithValue(@"id", cus.ID);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _write.Write(ex.ToString());
                throw ex;
            }
        }
    }
}
