using CustomerModule.LogClass;
using CustomerModule.Model.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CustomerModule
{
    class Utility : ICheckEmptyInsert
    {
        private IWrite _write;
        public Utility(IWrite write)
        {
            this._write = write;
        }

        public bool CheckEmptyInsert(List<TextBox> tbList)
        {
            try
            {
                bool noElement = true;
                foreach (TextBox tbc in tbList)
                {
                    if (tbc.Text != "" && tbc.Name != "tbID")
                    {
                        noElement = false;
                        break;
                    }
                }
                if (noElement == true)
                {
                    MessageBoxResult result = MessageBox.Show("All the entries are empty.Are you sure do you want to continue ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _write.Write(ex.ToString());
                MessageBox.Show("There was a problem with the input data");
                return false;
            }
        }

        
    }
}
