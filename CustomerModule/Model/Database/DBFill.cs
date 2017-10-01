using CustomerModule.LogClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerModule.Model.DatabaseClass
{
    class DBFill : IFill
    {
        private IWrite _write;
        private IConnect _connect;
        public DBFill(IConnect connect, IWrite write)
        {
            this._write = write;
            this._connect = connect;
        }
        public DataTable Fill()
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
                        command.CommandText = @"dbo.sp_FillGrid_Cutomers";

                        DataTable DT = new DataTable();
                        SqlDataAdapter SDA = new SqlDataAdapter(command);
                        SDA.Fill(DT);
                        return DT;
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
