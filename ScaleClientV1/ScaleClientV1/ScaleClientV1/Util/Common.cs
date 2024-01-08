using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleClient1.Util
{
    class Common
    {

        #region 数据库与控件操作
        /// <summary>
        /// ListView控件的查询-显示结果 函数
        /// </summary>
        /// <param name="listView1"></param>
        /// <param name="sql"></param>
        public void listViewDT(ListView lv, string sql)
        {
            DataTable dt = new DataTable();
            //string sql = "SELECT * FROM param_tab WHERE 1=1";
            SQLiteQuery sq = new SQLiteQuery();
            dt = sq.DtSelect(sql);
            lv.Items.Clear();//先清理子项
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr[0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    listitem.SubItems.Add(dr[j].ToString());
                }
                lv.Items.Add(listitem);
            }
        }

        /// <summary>
        /// ComboBox的sql数据加载
        /// </summary>
        /// <param name="comboBox1"></param>
        /// <param name="sql"></param>
        public void comboBoxDT(ComboBox comboBox1, string sql)
        {
            DataTable dt = new DataTable();
            //string sql1 = "sql=";
            SQLiteQuery sq = new SQLiteQuery();
            dt = sq.DtSelect(sql);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];
            //    comboBox1.Items.Add(dr[0].ToString());
            //}
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "machineName";
        }

        /// <summary>
        /// 判断有多少条数据的数据加载
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int RowCountDT(string sql)
        {
            DataTable dt = new DataTable();
            SQLiteQuery sq = new SQLiteQuery();
            dt = sq.DtSelect(sql);
            return dt.Rows.Count;
        }
        #endregion
    }
}
