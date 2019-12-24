using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.db;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Project.DataAccessor
{
    public class CustomersDataAccessor
    {
        public void Read (AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand ("SELECT * FROM customers", c.get(), t.get());
            a.Fill(ds, "customers");
        }
        public void Update (AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM customers"; 
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO customers (fio, position_customers) VALUES (@fio, @position_customers)";
            adapter.InsertCommand = new MySqlCommand (sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "fio",
                ParameterName = "@fio"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "position_customers",
                ParameterName = "@position_customers"
            });

            //UPDATE
            String sqlUpdate = "Update customers SET fio=@fio, position_customers=@position_customers where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "fio",
                ParameterName = "@fio"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "position_customers",
                ParameterName = "@position_customers"
            });

            //DELETE
            String sqlDelete = "DELETE FROM customers WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder (adapter);

            adapter.Update(ds, "customers");
        }

    }

    public class StaffDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM staff", c.get(), t.get());
            a.Fill(ds, "staff");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM staff"; 
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO staff (position, fio, phone, address) VALUES (@position, @fio, @phone, @address)";
            adapter.InsertCommand = new MySqlCommand (sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "position",
                ParameterName = "@position"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "fio",
                ParameterName = "@fio"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "phone",
                ParameterName = "@phone"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "address",
                ParameterName = "@address"
            });

            //UPDATE
            String sqlUpdate = "Update staff SET position=@position, fio=@fio, phone=@phone, address=@address where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "position",
                ParameterName = "@position"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "fio",
                ParameterName = "@fio"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "phone",
                ParameterName = "@phone"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "address",
                ParameterName = "@address"
            });


            //DELETE
            String sqlDelete = "DELETE FROM staff WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder (adapter);

            adapter.Update(ds, "staff");
        }

    }
    public class MaterialsDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM materials", c.get(), t.get());
            a.Fill(ds, "materials");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM materials";
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO materials (manufacturer, name) VALUES (@manufacturer, @name)";
            adapter.InsertCommand = new MySqlCommand(sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "manufacturer",
                ParameterName = "@manufacturer"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "name",
                ParameterName = "@name"
            });


            //UPDATE
            String sqlUpdate = "Update materials SET manufacturer=@manufacturer, name=@name where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "manufacturer",
                ParameterName = "@manufacturer"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "name",
                ParameterName = "@name"
            });

            //DELETE
            String sqlDelete = "DELETE FROM materials WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(adapter);

            adapter.Update(ds, "materials");
        }

    }
    public class PricesDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM prices", c.get(), t.get());
            a.Fill(ds, "prices");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM prices";
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO prices (name, price, id_material) VALUES (@name, @price, @id_material)";
            adapter.InsertCommand = new MySqlCommand(sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "name",
                ParameterName = "@name"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "price",
                ParameterName = "@price"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_material",
                ParameterName = "@id_material"
            });

            //UPDATE
            String sqlUpdate = "UPDATE prices SET name=@name, price=@price, id_material=@id_material where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "name",
                ParameterName = "@name"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "price",
                ParameterName = "@price"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_material",
                ParameterName = "@id_material"
            });

            //DELETE
            String sqlDelete = "DELETE FROM prices WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(adapter);

            adapter.Update(ds, "prices");
        }

    }
    public class ServicesDataAccessor
    {
        public void Read(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter a = new MySqlDataAdapter();
            a.SelectCommand = new MySqlCommand("SELECT * FROM services", c.get(), t.get());
            a.Fill(ds, "services");
        }
        public void Update(AbstractConnection c, AbstractTransaction t, DataSet1 ds)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM services";
            adapter.SelectCommand = new MySqlCommand(sqlSelect, c.get(), t.get());

            //INSERT
            String sqlInsert = "INSERT INTO services (payment, id_price, id_customers, id_staff) VALUES (@payment, @id_price, @id_customers, @id_staff)";
            adapter.InsertCommand = new MySqlCommand(sqlInsert, c.get(), t.get());
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "payment",
                ParameterName = "@payment"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_price",
                ParameterName = "@id_price"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_customers",
                ParameterName = "@id_customers"
            });
            adapter.InsertCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_staff",
                ParameterName = "@id_staff"
            });

            //UPDATE
            String sqlUpdate = "UPDATE services SET payment=@payment, id_price=@id_price, id_customers=@id_customers, id_staff=@id_staff where id=@id";
            adapter.UpdateCommand = new MySqlCommand(sqlUpdate, c.get(), t.get());
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "payment",
                ParameterName = "@payment"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_price",
                ParameterName = "@id_price"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_customers",
                ParameterName = "@id_customers"
            });
            adapter.UpdateCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id_staff",
                ParameterName = "@id_staff"
            });


            //DELETE
            String sqlDelete = "DELETE FROM services WHERE id = @id";
            adapter.DeleteCommand = new MySqlCommand(sqlDelete, c.get(), t.get());
            adapter.DeleteCommand.Parameters.Add(new MySqlParameter()
            {
                SourceColumn = "id",
                ParameterName = "@id"
            });

            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(adapter);

            adapter.Update(ds, "services");
        }

    }
}
