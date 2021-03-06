﻿using CustomerModule.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace CustomerModule
{
    public partial class MainWindow : Window
    {

        List<TextBox> tbList = new List<TextBox>();
        CustomerDataTable dt = new CustomerDataTable();
        DataRowView dataRow;
        int ColumnRowIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            tbList = dt.AddMany<TextBox>(tbName, tbSurname, tbTelephone, tbAddress,tbID);
            dt.FillDataGrid(dgCustomer);
            dt.FillFilterList(cbFilter, tbList);
        }       

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox tb in tbList)
                tb.Text = string.Empty;
            dgCustomer.SelectedIndex = -1;
            dgCustomer.Focus();
        }

        private void dgCustomer_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            dataRow = (DataRowView)dgCustomer.SelectedItem;
            if(dataRow!=null)
                foreach (TextBox tb in tbList)
                {
                    tb.Text = dataRow.Row.ItemArray[ColumnRowIndex].ToString();
                    ColumnRowIndex++;
                }
            ColumnRowIndex = 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            dt.DeleteCustomer(dgCustomer, tbID);
            tbFilter.Text = string.Empty;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            dt.InsertCustomer(dgCustomer, tbList);
            tbFilter.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            dt.UpdateCustomer(dgCustomer, tbList);
            tbFilter.Text = string.Empty;
        }

        private void dgCustomer_AutoGeneratedColumns(object sender, EventArgs e)
        {

            foreach (DataGridColumn col in dgCustomer.Columns) // Hide ID column 
            {
                if (col.Header.ToString() == "Id")
                {
                    col.Visibility = Visibility.Collapsed;
                    break;
                }
            }
        }

        private void tbTelephone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) ) // Check if input is Digit for Telephone
                e.Handled = true;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbFilter.SelectedItem != null)
                if (cbFilter.SelectedItem.ToString() != "No Filter")
                    dt.FilterCustomer(cbFilter, tbFilter, dgCustomer);
        }
    }
    
}
