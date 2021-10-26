using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Entity
{
    public class ExplorerWindow
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ExplorerWindow(string path, string name)
        {
            Name = name;
            Path = path;
        }
    }
}
