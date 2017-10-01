using CustomerModule.LogClass;
using System;
using System.Configuration;
using System.IO;

namespace CustomerModule
{
    class DBConnectionString : IConnectionString
    {
        private readonly IWrite _write;
        public DBConnectionString(IWrite write)
        {
            this._write = write;
        }
        // Set the path to the currennt base directory
        // Get the Database connection path from App Config
        public string ConnectionString()
        {
            try
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
                string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                return connectionString;
            }
            catch(Exception ex)
            {
                _write.Write(ex.ToString());
                throw ex;
            }         
        }
    }
}
