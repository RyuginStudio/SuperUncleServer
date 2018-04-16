/*
 * 时间：2018年4月16日12:15:03
 * 作者：vszed
 * 功能：数据库操作
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace SuperUncleServer
{
    public class DB_Controler
    {
        public static MySqlConnection mySqlConnection;//连接类对象

        private static string myConnectionString = "server=47.75.2.153;database=SuperUncleDB;uid=root;pwd=vszed666.;";

        //连接数据库
        public static void connectDB()
        {
            if (mySqlConnection == null)
            {
                try
                {
                    mySqlConnection = new MySqlConnection(myConnectionString);
                    mySqlConnection.Open();
                }
                catch (Exception)
                {
                    throw new Exception("数据库连接失败.....");
                }
            }
        }

        //查看“表”是否已经存在
        public static bool CheckExistsTable(string tablename)
        {
            connectDB();

            String tableNameStr = "SELECT table_name FROM information_schema.TABLES WHERE table_name = '" + tablename + "';";  //sql语句，查询表

            MySqlCommand command = new MySqlCommand(tableNameStr, mySqlConnection);

            var exist = command.ExecuteReader().HasRows;  //true => 存在

            disConnectDB();

            return exist;
        }

        //创建“表”
        public static void createTable(string tableName)
        {
            connectDB();

            String tableCreateStr = "CREATE TABLE " + tableName + " (UserName VarChar(255), CostTime Integer(255), Score Integer(255));";  //sql语句，查询表

            using (MySqlCommand command = new MySqlCommand(tableCreateStr, mySqlConnection))
            {
                command.ExecuteNonQuery();
            }

            disConnectDB();
        }

        //“表”插入信息
        public static void insertTableData(string TabelName, string UserName, int CostTime, int Score)
        {
            connectDB();

            String modifyTableDataStr = "insert into " + TabelName + " (UserName,CostTime,Score) values('" + UserName + "','" + CostTime + "','" + Score + "')";

            using (MySqlCommand command = new MySqlCommand(modifyTableDataStr, mySqlConnection))
            {
                command.ExecuteNonQuery();
            }

            disConnectDB();
        }

        public static void disConnectDB()
        {
            if (mySqlConnection != null)
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
                mySqlConnection = null;
            }
        }
    }
}