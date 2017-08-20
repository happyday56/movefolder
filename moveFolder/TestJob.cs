 
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace moveFolder
{
    public class TestJob : IJob
    {
        //private static readonly ILog logger = LogManager.GetLogger(typeof(TestJob));
        public void Execute(IJobExecutionContext context)
        {
            FolderOper.MoveFolder();
        }
    }
}