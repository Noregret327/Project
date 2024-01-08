
using ScaleTerminal;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System.Data.SQLite
{
    class SQLiteQuery
    {
        public void Execute(string sql)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        sh.Execute(sql);

                        //toolTip1.Show("Executed successfully", this, 150, 50, 1000);

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public void DgvSelect(DataGridView dgv,string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    try
                    {
                        DataTable dt = sh.Select(sql);
                        dgv.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Error");
                        dt.Rows.Add(ex.ToString());
                        dgv.DataSource = dt;
                    }

                    conn.Close();
                }
            }
        }

        public DataTable DtSelect(string sql)
        {
            DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(Config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    try
                    {
                        dt = sh.Select(sql);
                        //dgv.DataSource = dt;
                        //return dt;
                    }
                    catch (Exception ex)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("Error");
                        dt.Rows.Add(ex.ToString());
                        //dgv.DataSource = dt;
                    }

                    conn.Close();
                }
                return dt;
            }
            
        }
        
    }
}
