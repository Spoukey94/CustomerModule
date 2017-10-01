using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerModule.LogClass
{
    public interface IOpen
    {
        StreamWriter Open();
    }
    public interface IWrite
    {
        void Write(string text);
    }
    
}
