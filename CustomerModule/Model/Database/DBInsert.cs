using CustomerModule.LogClass;
using CustomerModule.Model.Customer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CustomerModule.DatabaseClass
{
    class DBInsert : IInsert
    {
        private IConnect _connect;
        private IWrite _write;
        public DBInsert(IConnect connect,IWrite write)
        {
            this._connect = connect;
            this._write = write;
        }
        public void Insert(Customer cus)
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
                        command.CommandText = @"dbo.sp_Insert_Customer";

                        command.Parameters.AddWithValue(@"name",cus.Name );
                        command.Parameters.AddWithValue(@"surname", cus.Surname);
                        command.Parameters.AddWithValue(@"telephone", cus.Telephone);
                        command.Parameters.AddWithValue(@"address", cus.Address);

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
