using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Repository;


namespace BlackSugar.Service
{
    public class OpenedFolderViewer
    {

        private IFileWriter _fileWriter;
        private IJsonParser _jsonParser;
        private IWindowGetter _windowGetter;

        public OpenedFolderViewer(
            IFileWriter fileWriter,
            IJsonParser jsonParser,
            IWindowGetter windowGetter
            )
        {
            _fileWriter = fileWriter;
            _jsonParser = jsonParser;
            _windowGetter = windowGetter;
        }

        public void MakeJsonFile(string path, bool option, ref string output)
        {
            try
            {
                var wins = _windowGetter.GetExplorerWindows();

                if (wins.Count == 0) return;

                var file = _fileWriter.GetFile();
 
                _fileWriter.Write(
                    System.IO.Path.Combine(path, file), 
                    _jsonParser.Parse(wins));

                if (option)
                    output = file;
            }
            catch (Exception ex)
            {
                _fileWriter.Write(
                    System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, 
                        _fileWriter.GetLogFile()),
                    ex);

            }


        }
    }
}
