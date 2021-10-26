using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using BlackSugar.Entity;
using BlackSugar.Utility;

namespace BlackSugar.Repository
{
    public interface IWindowGetter
    {
        List<ExplorerWindow> GetExplorerWindows();

    }

    public class WindowGetter : IWindowGetter
    {

        public const string MODULE_NAME_EXPLORER = "EXPLORER.EXE";

        private string ExplorerFullName()
        {
            return System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToUpper(),
                MODULE_NAME_EXPLORER);
        }

        public List<ExplorerWindow> GetExplorerWindows()
        {
            var list = new List<ExplorerWindow>();
            var fullName = ExplorerFullName();

            Shell32.Shell shell = null;
            SHDocVw.ShellWindows win = null;
            IEnumerator enumerator = null;

            try
            {
                shell = new Shell32.Shell();
                win = shell.Windows();

                enumerator = win.GetEnumerator();

                SHDocVw.IWebBrowser2 web = null;

                while (enumerator.MoveNext())
                {
                    web = enumerator.Current as SHDocVw.IWebBrowser2;
                    try
                    {
                        if (web.FullName.ToUpper() == fullName && web.LocationURL != "")
                        {
                            list.Add(new ExplorerWindow(   
                                new Uri(web.LocationURL).LocalPath,
                                FolderInfo.GetName(new Uri(web.LocationURL).LocalPath)
                            ));
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        if (web != null) Marshal.ReleaseComObject(web);
                    }
                }

                return list;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ICustomAdapter adapter = enumerator as ICustomAdapter;
                if (adapter != null) Marshal.ReleaseComObject(adapter.GetUnderlyingObject());
                if (win != null) Marshal.ReleaseComObject(win);
                if (shell != null) Marshal.ReleaseComObject(shell);

                adapter = null;
                enumerator = null;
                win = null;
                shell = null;

                // Application オブジェクトのガベージ コレクトを強制します。
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }


    }
}
