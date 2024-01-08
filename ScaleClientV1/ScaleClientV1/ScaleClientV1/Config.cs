using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleTerminal
{
    class Config
    {
        //数据库文件
        public static string DatabaseFile = "DB/myScaleDB.db";
        //数据源
        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DatabaseFile);
            }
        }
    }
}
