using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Repository;
using BlackSugar.Service;

namespace OFV
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {              
                string file = null;
                string sOp = null;
                string outputPath = AppDomain.CurrentDomain.BaseDirectory;

                if (args?.Length > 0)
                {
                    sOp = args.FirstOrDefault(a => a.ToUpper() == "/R");
                    if (args?.Length > 1)
                        outputPath = args[0];
                }

                new OpenedFolderViewer(new FileWriter(),
                                       new JsonParser(),
                                       new WindowGetter())
                    .MakeJsonFile(outputPath, sOp != null, ref file);

                if(sOp != null)
                    Console.Write(file);

            }
            catch (Exception ex)
            {
                
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
