using CustomerModule.Model.Customer;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace CustomerModule
{
    public interface IConnect
    {
        SqlConnection ConnectToDatabase();
        
    }
    public interface IConnectionString
    {
        string ConnectionString();
    }
    public interface IUpdate
    {
        void Update(Customer cus);
    }
    public interface IDelete
    {
        void Delete(Customer cus);
    }
    public interface IInsert
    {
        void Insert(Customer cus);
    }
    public interface IFill
    {
        DataTable Fill();
    }
    public interface IFilter
    {
        DataTable Filter(ComboBox lbList,TextBox text);
    }
}
