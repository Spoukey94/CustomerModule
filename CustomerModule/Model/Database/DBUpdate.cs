using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerModule.Model.Customer;
using CustomerModule.LogClass;
using System.Data;
using System.Data.SqlClient;

namespace CustomerModule.Model.DatabaseClass
{
    class DBUpdate : IUpdate
    {
        private IConnect _connect;
        private IWrite _write;
        public DBUpdate(IConnect connect,IWrite write)
        {
            this._connect = connect;
            this._write = write;
        }
        public void Update(Customer.Customer cus)
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
                        command.CommandText = @"dbo.sp_Update_Customer";

                        command.Parameters.AddWithValue(@"name", cus.Name);
                        command.Parameters.AddWithValue(@"surname", cus.Surname);
                        command.Parameters.AddWithValue(@"telephone", cus.Telephone);
                        command.Parameters.AddWithValue(@"address", cus.Address);
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
