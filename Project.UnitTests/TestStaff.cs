using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

using Project;
using Project.DataAccessor;
using Project.db;
using DATests.Extend;
using NUnit.Framework;



namespace DATests
{
    [TestFixture]
    public class TestStaff
    {
        [SetUp]
        public void Setup()
        {
            TableInit.init();
        }

        [Test]
        public void GetAll()
        {
            //Setup();
            StaffDataAccessor daStaff = new StaffDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                daStaff.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.staff.Select().OfType<DataRow>().ToList();
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));  //сортируем по ид

            Assert.That(1, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(15));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffById()
        {
            StaffDataAccessor daStaff = new StaffDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                daStaff.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.staff.Select("id = 4").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffByName()
        {
            StaffDataAccessor daStaff = new StaffDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                daStaff.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffByCode()
        {
            StaffDataAccessor daStaff = new StaffDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                daStaff.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.staff.Select("position = 'Главный врач Стоматолог-хирург'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffUpdate()
        {
            StaffDataAccessor daStaff = new StaffDataAccessor();
            DataSet1 dataSet1 = new DataSet1();
            AbstractConnection connection = null;
            AbstractTransaction transaction = null;
            try
            {
                connection = DBFactory.createConnection();
                connection.open();
                transaction = connection.beginTransaction();
                daStaff.Read(connection, transaction, dataSet1);
                transaction.commit();
            }
            catch (Exception e)
            {
                transaction.rollback();
            }
            finally
            {
                connection.close();
            }

            List<DataRow> list = dataSet1.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));


            // Обновляем первую запись
            DataSet1.staffRow oldM = null;
            AbstractConnection connectionN = null;
            AbstractTransaction transactionN = null;
            String oldName = "";
            try
            {
                connectionN = DBFactory.createConnection();
                connectionN.open();
                transactionN = connectionN.beginTransaction();
                oldM = dataSet1.staff[0];
                oldName = oldM.fio;

                dataSet1.staff[0].fio = oldM.fio + "_changed";
                daStaff.Update(connectionN, transactionN, dataSet1);
                transactionN.commit();
            }
            catch (Exception e)
            {
                transactionN.rollback();
            }
            finally
            {
                connectionN.close();
            }


            // Заново читаем из базы, проверяем, что поменялось
            StaffDataAccessor daUpdated = new StaffDataAccessor();
            DataSet1 dataSetUpdated = new DataSet1();
            AbstractConnection connectionUpdated = null;
            AbstractTransaction transactionUpdated = null;
            try
            {
                connectionUpdated = DBFactory.createConnection();
                connectionUpdated.open();
                transactionUpdated = connectionUpdated.beginTransaction();
                daUpdated.Read(connectionUpdated, transactionUpdated, dataSetUpdated);
                transactionUpdated.commit();
            }
            catch (Exception e)
            {
                transactionUpdated.rollback();
            }
            finally
            {
                connectionUpdated.close();
            }

            // достаем из датасета все записи таблицы
            List<DataRow> list_3 = dataSetUpdated.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(15));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet1.staff.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.staffRow updatedM = dataSetUpdated.staff[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));

            Assert.That(oldName, !Is.EqualTo(updatedM.fio));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.fio));

        }

        [Test]
        public void staffAdd()
        {
            DataSet1 dataSetRead = new DataSet1();
            StaffDataAccessor daStaff = new StaffDataAccessor();
            AbstractConnection absCon_Read = null;
            AbstractTransaction absTran_Read = null;
            int countRowBefore = 0;
            try
            {
                absCon_Read = DBFactory.createConnection();
                absCon_Read.open();
                absTran_Read = absCon_Read.beginTransaction();
                daStaff.Read(absCon_Read, absTran_Read, dataSetRead);

                List<DataRow> rows_list = dataSetRead.staff.Select("").OfType<DataRow>().ToList();
                // Сортируем строки по id в порядке возрастания
                rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
                // Количество записей до внесения новой
                countRowBefore = rows_list.Count();
                absTran_Read.commit();
            }
            catch (Exception e)
            {
                absTran_Read.rollback();
            }
            finally
            {
                absCon_Read.close();
            }


            // НОВОЕ СОЕДИНЕНИЕ, Добавляем в базу новую запись
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            ///            
            DataRow rowForAdded = dataSetRead.staff.NewRow();


            rowForAdded["fio"] = "Кулешов Юрий Викторович";
            rowForAdded["position"] = "Главный врач Стоматолог-хирург";
            rowForAdded["phone"] = "+79134568712";
            rowForAdded["address"] = "Полиграфическая Ул., дом 4, кв. 97";
            dataSetRead.staff.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));

            try
            {
                absCon_Update = DBFactory.createConnection();
                absCon_Update.open();
                absTran_Update = absCon_Update.beginTransaction();
                daStaff.Update(absCon_Update, absTran_Update, dataSetRead);

                absTran_Update.commit();
            }
            catch (Exception e)
            {
                absTran_Update.rollback();
            }
            finally
            {
                absCon_Update.close();
            }


            // Новый коннекшн, проверяем что теперь записей стало на одну больше
            AbstractConnection absCon_AfterInsert = null;
            AbstractTransaction absTran_AfterInsert = null;
            DataSet1 dataSet_AfterInsert = new DataSet1();
            StaffDataAccessor staffDA_AfterInsert = new StaffDataAccessor();
            int countRowAfter = 0;
            try
            {
                absCon_AfterInsert = DBFactory.createConnection();
                absCon_AfterInsert.open();
                absTran_AfterInsert = absCon_AfterInsert.beginTransaction();
                staffDA_AfterInsert.Read(absCon_AfterInsert, absTran_AfterInsert, dataSet_AfterInsert);
                absTran_AfterInsert.commit();
            }
            catch (Exception e)
            {
                absTran_AfterInsert.commit();
            }
            finally
            {
                absCon_AfterInsert.close();
            }

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1));

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["fio"], Is.EqualTo(rowAfterInsert["fio"]));
            Assert.That(rowForAdded["position"], Is.EqualTo(rowAfterInsert["position"]));
            Assert.That(rowForAdded["phone"], Is.EqualTo(rowAfterInsert["phone"]));
        }

        [Test]
        public void staffDelete()
        {
            DataSet1 dataSetRead = new DataSet1();
            StaffDataAccessor daStaff = new StaffDataAccessor();
            AbstractConnection absCon_Read = null;
            AbstractTransaction absTran_Read = null;
            int countRowBefore = 0;
            try
            {
                absCon_Read = DBFactory.createConnection();
                absCon_Read.open();
                absTran_Read = absCon_Read.beginTransaction();
                daStaff.Read(absCon_Read, absTran_Read, dataSetRead);

                List<DataRow> rows_list = dataSetRead.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();
                // Сортируем строки по id в порядке возрастания
                rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
                // Количество записей до удаления
                countRowBefore = rows_list.Count();
                absTran_Read.commit();
            }
            catch (Exception e)
            {
                absTran_Read.rollback();
            }
            finally
            {
                absCon_Read.close();
            }

            Assert.That(countRowBefore, Is.EqualTo(1));


            // НОВОЕ СОЕДИНЕНИЕ, удаляем
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();

            foreach (DataRow rowForDel in list_1)
            {
                dataSetRead.staff.Rows.Remove(rowForDel);
            }


            try
            {
                absCon_Update = DBFactory.createConnection();
                absCon_Update.open();
                absTran_Update = absCon_Update.beginTransaction();
                daStaff.Update(absCon_Update, absTran_Update, dataSetRead);

                absTran_Update.commit();
            }
            catch (Exception e)
            {
                absTran_Update.rollback();
            }
            finally
            {
                absCon_Update.close();
            }


            // Новый коннекшн, проверяем что теперь записей стало на одну больше
            AbstractConnection absCon_AfterInsert = null;
            AbstractTransaction absTran_AfterInsert = null;
            DataSet1 dataSet_AfterInsert = new DataSet1();
            StaffDataAccessor staffDA_AfterInsert = new StaffDataAccessor();
            try
            {
                absCon_AfterInsert = DBFactory.createConnection();
                absCon_AfterInsert.open();
                absTran_AfterInsert = absCon_AfterInsert.beginTransaction();
                staffDA_AfterInsert.Read(absCon_AfterInsert, absTran_AfterInsert, dataSet_AfterInsert);
                absTran_AfterInsert.commit();
            }
            catch (Exception e)
            {
                absTran_AfterInsert.commit();
            }
            finally
            {
                absCon_AfterInsert.close();
            }

            List<DataRow> rows_list_AfterInsert = dataSetRead.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
