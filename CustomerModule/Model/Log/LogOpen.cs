using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomerModule.LogClass
{
    class LogOpen : IOpen
    {
        public StreamWriter Open()
        {
            try
            {
                StreamWriter Log = new StreamWriter("Log.txt");
                return Log;
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Log file could not be created");
                return null;
            }
            
        }      
    }

}
