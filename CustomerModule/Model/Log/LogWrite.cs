using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerModule.LogClass
{
    class LogWrite : IWrite
    {
        private  readonly IOpen _open;
        public LogWrite(IOpen Open)
        {
            this._open = Open;
        }
        public void Write(string text)
        {
            try
            {
                StreamWriter Log = _open.Open();
                using (Log)
                {
                    Log.WriteLine("{0}: {1}", DateTime.Now, text);
                }
            }
            catch
            {

            }
        }
    }
}
