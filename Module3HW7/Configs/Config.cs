using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3HW7.Configs
{
    public class Config
    {
        public int RecordsInOneTime { get; set; }
        public string LoggerPath { get; set; }
        public string LogFileName { get; set; }
        public string BackupPath { get; set; }
        public string FileNameFormat { get; set; }
        public string FileType { get; set; }
    }
}
