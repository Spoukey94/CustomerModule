using CustomerModule.LogClass;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace CustomerModule.Model.Database
{
    public class DBFilter : IFilter
    {
        private IWrite _write;
        private IConnect _connect;
        public DBFilter(IConnect connect, IWrite write)
        {
            this._write = write;
            this._connect = connect;
        }
        public DataTable Filter(ComboBox cbFilter, TextBox text)
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
                        command.CommandText = @"dbo.sp_Filter_Customer";
                       
                        command.Parameters.AddWithValue(@"filter", cbFilter.SelectedItem.ToString());
                        command.Parameters.AddWithValue(@"text", text.Text);

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
