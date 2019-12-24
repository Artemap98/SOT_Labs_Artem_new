using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

using Project;
using Project.DataAccessor;
using Project.db;
using BL;
using BLTests.Extend;
using NUnit.Framework;



namespace BLTests
{
    [TestFixture]
    public class TestStaff
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
            DataSet1 dataSet = BL.getStaff();

            List<DataRow> list = dataSet.staff.Select().OfType<DataRow>().ToList();
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));  //сортируем по ид

            Assert.That(list.Count, Is.EqualTo(15));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffById()
        {
            DataSet1 dataSet = BL.getStaff();

            List<DataRow> list = dataSet.staff.Select("id = 4").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffByName()
        {
            DataSet1 dataSet = BL.getStaff();

            List<DataRow> list = dataSet.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffByCode()
        {
            DataSet1 dataSet = BL.getStaff();
            List<DataRow> list = dataSet.staff.Select("position = 'Главный врач Стоматолог-хирург'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(4));
            Assert.That((string)(list[0]["fio"]), Is.EqualTo("Кулешов Юрий Викторович"));
            Assert.That((string)(list[0]["position"]), Is.EqualTo("Главный врач Стоматолог-хирург"));
            Assert.That((string)(list[0]["phone"]), Is.EqualTo("+79134568712"));
        }

        [Test]
        public void staffUpdate()
        {
            DataSet1 dataSet = BL.getStaff();
            List<DataRow> list = dataSet.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));


            // Обновляем первую запись
            DataSet1.staffRow oldM = null;
            AbstractConnection connectionN = null;
            AbstractTransaction transactionN = null;
            String oldName = "";

            oldM = dataSet.staff[0];
            oldName = oldM.fio;
            dataSet.staff[0].fio = oldM.fio + "_changed";
            BL.updateStaff(dataSet);


            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getStaff();
            List<DataRow> list_3 = dataSetUpdated.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(15));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet.staff.Select("id = " + oldM.id).OfType<DataRow>().ToList();
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
            DataSet1 dataSetRead = BL.getStaff();

            int countRowBefore = 0;

            List<DataRow> rows_list = dataSetRead.staff.Select("").OfType<DataRow>().ToList();
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            countRowBefore = rows_list.Count();

            //Добавляем в базу новую запись
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
            BL.updateStaff(dataSetRead);

            // Новый коннекшн, проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterInsert = BL.getStaff();

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.staff.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            int countRowAfter = rows_list_AfterInsert.Count();

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
            DataSet1 dataSetRead = BL.getStaff();

            List<DataRow> rows_list = dataSetRead.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                rowForDel.Delete();
            }
            BL.updateStaff(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше
            DataSet1 dataSet_AfterDel = BL.getStaff();
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.staff.Select("fio = 'Кулешов Юрий Викторович'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
