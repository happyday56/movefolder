using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
namespace moveFolder
{
    public class FolderOper
    {
        static string folder = "D:\\wwwroot\\liuxueba\\wwwroot\\uploads\\allimg";
        static string toFolder = "C:\\res\\uploads\\allimg";
        public static void setFileAccess(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            System.Security.AccessControl.FileSecurity fileSecurity = fi.GetAccessControl();
            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.Read, AccessControlType.Allow));
            fi.SetAccessControl(fileSecurity);
        }

        public static void MoveFolder()
        {
            MoveFolder(folder, toFolder);
        }

        /// <summary>  
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹  
        /// </summary>  
        /// <param name="sourcePath">源文件夹</param>  
        /// <param name="destPath">目标文件夹</param>  
        private static void MoveFolder(string sourcePath, string destPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    if (!Directory.Exists(destPath))
                    {
                        //目标目录不存在则创建  
                        try
                        {
                            Directory.CreateDirectory(destPath);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("创建目标目录失败：" + ex.Message);
                        }
                    }
                    //获得源文件下所有文件  
                    List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                    files.ForEach(c =>
                    {
                        string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                        //覆盖模式  
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile);
                        }
                        File.Move(c, destFile);
                        setFileAccess(destFile);
                    });
                    //获得源文件下所有目录文件  
                    List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                    folders.ForEach(c =>
                    {
                        string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                        //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。  
                        //Directory.Move(c, destDir);  

                        //采用递归的方法实现  
                        MoveFolder(c, destDir);
                    });
                }
                else
                {
                    throw new DirectoryNotFoundException("源目录不存在！");
                }
            }
            catch (Exception e)
            {
            }
        }


        public void folderAccess(string sourcePath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {

                    //获得源文件下所有文件  
                    List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                    files.ForEach(c =>
                    {
                        setFileAccess(c);
                    });
                    //获得源文件下所有目录文件  
                    List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                    folders.ForEach(c =>
                    {
                        //采用递归的方法实现  
                        folderAccess(c);
                    });
                }
                else
                {
                    throw new DirectoryNotFoundException("源目录不存在！");
                }
            }
            catch (Exception e)
            {
            }
        }

        public static void copy()
        {
            copy(folder, toFolder);
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        private static void copy(string sourcePath, string destPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    if (!Directory.Exists(destPath))
                    {
                        //目标目录不存在则创建  
                        try
                        {
                            Directory.CreateDirectory(destPath);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("创建目标目录失败：" + ex.Message);
                        }
                    }
                    //获得源文件下所有文件  
                    List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                    files.ForEach(c =>
                    {
                        string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                        //覆盖模式  
                        if (!File.Exists(destFile))
                        {
                            File.Copy(c, destFile, true);
                            setFileAccess(destFile);
                        }

                    });
                    //获得源文件下所有目录文件  
                    List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                    folders.ForEach(c =>
                    {
                        string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                        //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。  
                        //Directory.Move(c, destDir);  

                        //采用递归的方法实现  
                        copy(c, destDir);
                    });
                }
                else
                {
                    throw new DirectoryNotFoundException("源目录不存在！");
                }
            }
            catch (Exception e)
            {
            }
        }

    }

}
