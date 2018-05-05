using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace moveFolder
{
    public partial class Form1 : Form
    {
        static string folder = "D:\\wwwroot\\liuxueba\\wwwroot\\uploads\\allimg";
        static string toFolder = "C:\\res\\uploads\\allimg";

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //folderAccess(toFolder);

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(folder, "*.*");

            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            fileSystemWatcher.Created += fileSystemWatcher_Changed;
            fileSystemWatcher.Changed += fileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += fileSystemWatcher_Changed;
            fileSystemWatcher.Renamed += fileSystemWatcher_Changed;
            fileSystemWatcher.EnableRaisingEvents = true;


        }

        void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            FolderOper.copy();
        }

    }
}
