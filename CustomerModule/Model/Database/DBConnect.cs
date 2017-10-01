using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerModule.LogClass;

namespace CustomerModule.DatabaseClass
{
    class DBConnect : IConnect
    {
        private readonly IConnectionString _connectionString;
        private readonly IWrite _write;
        public DBConnect(IConnectionString connectionString,IWrite write)
        {
            this._write = write;
            this._connectionString = connectionString;
        }
        public SqlConnection ConnectToDatabase()
        {
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString.ConnectionString());
                return conn;
            }
            catch (Exception ex)
            {
                _write.Write(ex.ToString());
                throw ex;
            }
        }
    }
}
