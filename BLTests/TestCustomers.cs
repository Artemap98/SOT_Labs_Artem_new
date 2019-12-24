using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

using Project;
using Project.DataAccessor;
using Project.db;
using BLTests.Extend;
using BL;
using NUnit.Framework;


//Тесты на бизнес-логику
namespace BLTests
{
    [TestFixture]
    public class TestCustomers
    {
        private BusinessLogic BL = new BusinessLogic();

        [SetUp]
        public void Setup()
        {
            TableInit.init();
        }

        [Test]
        public void GetAll()
        {
            DataSet1 dataSet = BL.getCustomers();
            List<DataRow> list = dataSet.customers.Select().OfType<DataRow>().ToList();
            list.Sort((a, b) => ((int)a["id"]).CompareTo((int)b["id"]));

            Assert.That(list.Count, Is.EqualTo(8));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Ямских Иларион Миронович"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("89136574356"));  
        }

        [Test]
        public void CustomersById()
        {
            DataSet1 dataSet1 = BL.getCustomers();
            List<DataRow> list = dataSet1.customers.Select("id = 1").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Ямских Иларион Миронович"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("89136574356"));  
        }

        [Test]
        public void CustomersByName()
        {
            DataSet1 dataSet1 = BL.getCustomers();
            List<DataRow> list = dataSet1.customers.Select("fio = 'Ямских Иларион Миронович'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Ямских Иларион Миронович"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("89136574356"));
        }

        [Test]
        public void CustomersByCode()
        {
            DataSet1 dataSet1 = BL.getCustomers();

            List<DataRow> list = dataSet1.customers.Select("phone = '89136574356'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Ямских Иларион Миронович"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("89136574356"));
        }

        [Test]
        public void CustomersUpdate()
        {
            DataSet1 dataSet1 = BL.getCustomers();

            List<DataRow> list = dataSet1.customers.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            list.Sort((a, b) => ((int)a["id"]).CompareTo((int)b["id"]));

            // Обновляем первую запись
            DataSet1.customersRow oldM = null;
            String oldName = "";

            oldM = dataSet1.customers[0];
            oldName = oldM.fio;

            dataSet1.customers[0].fio = oldM.fio + "_changed";
            BL.updateCustomers(dataSet1);



            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getCustomers();
            
            // достаем из датасета все записи таблицы
            List<DataRow> list_3 = dataSetUpdated.customers.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(8));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet1.customers.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.customersRow updatedM = dataSetUpdated.customers[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));

            Assert.That(oldName, !Is.EqualTo(updatedM.fio));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.fio));

        }

        [Test]
        public void CustomersAdd()
        {
            DataSet1 dataSetRead = BL.getCustomers();

            int countRowBefore = 0;
            List<DataRow> rows_list = dataSetRead.customers.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            countRowBefore = rows_list.Count();


            // НОВОЕ СОЕДИНЕНИЕ, Добавляем в базу новую запись
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.customers.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
///            
            DataRow rowForAdded = dataSetRead.customers.NewRow();

            rowForAdded["fio"] = "Ямских Иларион Миронович";
            rowForAdded["phone"] = "89136574356";

            dataSetRead.customers.Rows.Add(rowForAdded);
            List<DataRow> list_2 = dataSetRead.customers.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            
            BL.updateCustomers(dataSetRead);


            //проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterInsert = BL.getCustomers();

            int countRowAfter = 0;

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.customers.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1));

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["fio"], Is.EqualTo(rowAfterInsert["fio"]));
            Assert.That(rowForAdded["phone"], Is.EqualTo(rowAfterInsert["phone"]));
        }

        [Test]
        public void CustomersDelete()
        {
            DataSet1 dataSetRead = BL.getCustomers();

            List<DataRow> rows_list = dataSetRead.customers.Select("fio = 'Ямских Иларион Миронович'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.customers.Select("fio = 'Ямских Иларион Миронович'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                //dataSetRead.customers.Rows.Remove(rowForDel);
                rowForDel.Delete();
            }
            BL.updateCustomers(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterDel = BL.getCustomers();
            //BL.updateCustomers(dataSet_AfterInsert);
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.customers.Select("fio = 'Ямских Иларион Миронович'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
