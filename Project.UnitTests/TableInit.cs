using System;
using System.IO;
using Project.db;
using MySql.Data.MySqlClient;

namespace DATests.Extend
{
    class TableInit
    {
        private static string TruncateSql = "Truncate.sql";
        private static string InsertSql = "Insert.sql";
        
        public static void init()
        {
            // Дропаем все таблицы
            AbstractConnection connection_Truncate = null;
            AbstractTransaction transaction_Truncate = null;
            try
            {
                connection_Truncate = DBFactory.createConnection();
                connection_Truncate.open();
                transaction_Truncate = connection_Truncate.beginTransaction();
                string path = Path.Combine(@"C:\SQL", TruncateSql);
                String str = File.ReadAllText(path);
                MySqlScript script = new MySqlScript(connection_Truncate.get(), str);
                script.Execute();
                transaction_Truncate.commit();
            }
            catch (Exception e)
            {
                transaction_Truncate.rollback();
            }
            finally
            {
                connection_Truncate.close();
            }
            
            // Создаем базу заново
            AbstractConnection connection_Insert = null;
            AbstractTransaction transaction_Insert = null;
            try
            {
                connection_Insert = DBFactory.createConnection();
                connection_Insert.open();
                transaction_Insert = connection_Insert.beginTransaction();
                string path = Path.Combine( @"C:\SQL", InsertSql);
                String str = File.ReadAllText(path);
                MySqlScript script = new MySqlScript(connection_Insert.get(), str);
                script.Execute();
                transaction_Insert.commit();
            }
            catch (Exception e)
            {
                transaction_Insert.rollback();
            }
            finally
            {
                connection_Insert.close();
            }
        }  

    }
}

