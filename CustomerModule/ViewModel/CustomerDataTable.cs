using CustomerModule.DatabaseClass;
using CustomerModule.LogClass;
using CustomerModule.Model.Customer;
using CustomerModule.Model.Database;
using CustomerModule.Model.DatabaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace CustomerModule.ViewModel
{
    class CustomerDataTable : DataGrid
    {
        int RowId;

        LogWrite log;
        DBConnectionString dbString;
        DBConnect dbConnect;
        DBFill dbFill;
        DBDelete dbDelete;
        DBUpdate dbUpdate;
        DBInsert dbInsert;
        DBFilter dbFilter;
        Customer cus;
        Utility util;

        public CustomerDataTable()
        {
            cus = new Customer();
            util = new Utility(log);
            log = new LogWrite(new LogOpen());
            dbString = new DBConnectionString(log);
            dbConnect = new DBConnect(dbString, log);
            dbFill = new DBFill(dbConnect, log);
            dbDelete = new DBDelete(dbConnect, log);
            dbInsert = new DBInsert(dbConnect, log);
            dbUpdate = new DBUpdate(dbConnect, log);
            dbFilter = new DBFilter(dbConnect, log);
        }

        public void FillDataGrid(DataGrid dgCustomer)
        {
            dgCustomer.ItemsSource = dbFill.Fill().AsDataView();
        }

        public void FilterCustomer(ComboBox cbFilter,TextBox text,DataGrid dgCustomer )
        {
            dgCustomer.ItemsSource = dbFilter.Filter(cbFilter, text).AsDataView();
        }

        public void DeleteCustomer(DataGrid dgCustomer, TextBox tbID)
        {
            RowId = dgCustomer.SelectedIndex;
            if (dgCustomer.SelectedIndex != -1)
            {
                dbDelete.Delete(new Customer { ID = tbID.Text });
                FillDataGrid(dgCustomer);
            }
            else MessageBox.Show("Please select a column to remove");
            if (RowId - 1 >= 0)
            {
                dgCustomer.SelectedIndex = RowId - 1;
                dgCustomer.Focus();
            }
        }

        public void InsertCustomer(DataGrid dgCustomer,List<TextBox> tbList)
        {
            if (util.CheckEmptyInsert(tbList) == true)
            {              
                foreach(TextBox tb in tbList)
                {
                    if (tb.Name == "tbName")
                        cus.Name = tb.Text;
                    if (tb.Name == "tbSurname")
                        cus.Surname = tb.Text;
                    if (tb.Name == "tbAddress")
                        cus.Address = tb.Text;
                    if (tb.Name == "tbTelephone")
                        cus.Telephone = tb.Text;
                }
                dbInsert.Insert(cus);
                FillDataGrid(dgCustomer);
            }
        }

        public void UpdateCustomer(DataGrid dgCustomer,List<TextBox> tbList)
        {
                string tbID = string.Empty;
                foreach (TextBox tb in tbList)
                {
                    if (tb.Name == "tbName")
                        cus.Name = tb.Text;
                    if (tb.Name == "tbSurname")
                        cus.Surname = tb.Text;
                    if (tb.Name == "tbAddress")
                        cus.Address = tb.Text;
                    if (tb.Name == "tbTelephone")
                        cus.Telephone = tb.Text;
                    if (tb.Name == "tbID")
                    {
                        cus.ID = tb.Text;
                        tbID = tb.Text;
                    }
                }
                RowId = dgCustomer.SelectedIndex;
                if (dgCustomer.SelectedIndex != -1 && tbID != string.Empty)
                {
                    if (util.CheckEmptyInsert(tbList) == true)
                    {
                        dbUpdate.Update(cus);
                        FillDataGrid(dgCustomer);
                    }
                }
                else MessageBox.Show("Please select a column to modify");
                dgCustomer.SelectedIndex = RowId;
                dgCustomer.Focus();
        }

        public List<T> AddMany<T>(params T[] elements)
        {
            List<T> list = new List<T>();
            list.AddRange(elements);
            return list;
        }

        public void FillFilterList(ComboBox cbFilter,List<TextBox> tbList)
        {
            try
            {
                int index = 0;
                cbFilter.Items.Insert(index, "No Filter");
                foreach (TextBox tb in tbList)
                {
                    if (tb.Name != "tbID")
                    {
                        index++;
                        cbFilter.Items.Insert(index, tb.Name.Substring(2));
                    }
                }
                cbFilter.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                log.Write(ex.ToString());
            }
        }

        
    }
}
