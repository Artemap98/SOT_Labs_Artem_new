using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Project.db
{
    public class DBFactory
    {
        public static AbstractConnection createConnection ()
        {
            string host = "127.0.0.1";  // Имя локального компьютера
            string database = "testdb";  // Имя базы данных
            string user = "root";       // Имя пользователя
            string password = ""; // Пароль пользователя

            string confStr = "Server=" + host + "; Database=" + database + "; Uid=" + user + "; Pwd=" + password;

            MySqlConnection sc = new MySqlConnection(confStr);
            AbstractConnection lc = new LabConnection(sc);
            return lc;
        }
    }
    public abstract class AbstractConnection
    {
        protected MySqlConnection c;
        public abstract AbstractTransaction beginTransaction();
        public abstract void open();
        public abstract void close();
        public abstract MySqlConnection get();
    }

    public class LabConnection : AbstractConnection
    {
        public LabConnection (MySqlConnection cPar)
        {
            c = cPar;
        }
        public override AbstractTransaction beginTransaction()
        {
            MySqlTransaction t = c.BeginTransaction();
            AbstractTransaction lt = new LabTransaction(t);
            return lt;
        }
        public override void open() { c.Open(); }
        public override void  close() { c.Close(); }
        public override MySqlConnection get() { return c; }
    }

    public abstract class AbstractTransaction
    {
        protected MySqlTransaction t;
        public abstract void commit();
        public abstract void rollback ();
        public abstract MySqlTransaction get();
    }
    public class LabTransaction: AbstractTransaction
    {
        //public LabTransaction() { t = new MySqlTransaction(); }
        public LabTransaction(MySqlTransaction tPar) { t = tPar; }
        public override void commit() { t.Commit(); }
        public override void rollback() { t.Rollback(); }
        public override MySqlTransaction get() { return t; }
    }
}
