using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Utility
{
    public enum FolderKind
    {
        NonFolder = -1,
        Nomal = 0,
        Drive,
        NetShare,
        Server,
        
    }

    public class FolderInfo
    {
        private const string SIGN_W_BACKSLASH = @"\\";
        private const string SIGN_BACKSLASH = @"\";

        public static string GetName(string path)
        {
            if (path.EndsWith(SIGN_BACKSLASH))
                path = path.Substring(0, path.Length - 1);

            var folderKind = GetFolderKind(path);

            switch (folderKind)
            {
                case FolderKind.Nomal:
                    return Path.GetFileName(path);
                case FolderKind.Drive:
                    return GetDriveName(path);
                case FolderKind.Server:
                    return path.Substring(2, path.Length - 3);
                case FolderKind.NetShare:
                    return GetNetShareName(path);
                default:
                    return path;

            }
        }

        public static FolderKind GetFolderKind(string path)
        {
            if (IsNetworkRoot(path))
                return FolderKind.Server;

            if (!Directory.Exists(path))
                return FolderKind.NonFolder;

            if (IsShared(path))
                return FolderKind.NetShare;

            var root = Directory.GetDirectoryRoot(path);
            if (root == path)
                return FolderKind.Drive;

            return FolderKind.Nomal;
        }


        private static string GetDriveName(string path)
        {
            var sDrive = path.Replace(SIGN_BACKSLASH, "");
            var drive = new DriveInfo(sDrive);

            if (drive?.IsReady == true)
                return drive.VolumeLabel + " (" + sDrive + ")";

            return path;
        }

        private static string GetNetShareName(string path)
        {
            var folder = path.Replace(SIGN_W_BACKSLASH, "").Split(SIGN_BACKSLASH.ToCharArray());
            return folder[1];
        }

        private static bool IsNetworkRoot(string path)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(path, @"^(\\\\[^\\]+([^\\]|\\))$");
        }
        
        private static bool IsShared(string path)
        {
            if (path.StartsWith(SIGN_W_BACKSLASH))
            {
                return path.Replace(SIGN_W_BACKSLASH, "").Split(SIGN_BACKSLASH.ToCharArray()).Length == 2;
            }
            return false;
        }
    }
}
