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
    public class TestPrices
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
            DataSet1 dataSet = BL.getPricess();

            List<DataRow> list = dataSet.prices.Select().OfType<DataRow>().ToList();
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));  //сортируем по ид

            Assert.That(1, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["id_material"]), Is.EqualTo(2));
        }

        [Test]
        public void pricesById()
        {
            DataSet1 dataSet = BL.getPricess();
            List<DataRow> list = dataSet.prices.Select("id = 1").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["id_material"]), Is.EqualTo(2));
        }

        [Test]
        public void pricesByName()
        {
            DataSet1 dataSet = BL.getPricess();
            List<DataRow> list = dataSet.prices.Select("name = '000001'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["id_material"]), Is.EqualTo(2));
        }

        [Test]
        public void pricesByCode()
        {
            DataSet1 dataSet = BL.getPricess();
            List<DataRow> list = dataSet.prices.Select("id_material = '2'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(1));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("000001"));
            Assert.That((int)(list[0]["id_material"]), Is.EqualTo(2));
        }

        [Test]
        public void pricesUpdate()
        {
            DataSet1 dataSet = BL.getPricess();
            List<DataRow> list = dataSet.prices.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));

            // Обновляем первую запись
            DataSet1.pricesRow oldM = dataSet.prices[0];
            String oldName = oldM.name;

            dataSet.prices[0].name = oldM.name + "_changed";
            BL.updatePricess(dataSet);


            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getPricess();
           
            List<DataRow> list_3 = dataSetUpdated.prices.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(2));

            // Достаем ту же запись
            List<DataRow> rows_list = dataSet.prices.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.pricesRow updatedM = dataSetUpdated.prices[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));
            Assert.That(oldName, !Is.EqualTo(updatedM.name));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.name));

        }

        [Test]
        public void pricesAdd()
        {
            DataSet1 dataSetRead = BL.getPricess();
           
            List<DataRow> rows_list = dataSetRead.prices.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            int countRowBefore = rows_list.Count();
     
            //Добавляем в базу новую запись
            AbstractConnection absCon_Update = null;
            AbstractTransaction absTran_Update = null;

            List<DataRow> list_1 = dataSetRead.prices.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
     
            DataRow rowForAdded = dataSetRead.prices.NewRow();

            rowForAdded["name"] = "000000";
            rowForAdded["price"] = "2011-02-20";
            rowForAdded["id_material"] = "2";

            dataSetRead.prices.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.prices.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            BL.updatePricess(dataSetRead);

            // Новый коннекшн, проверяем что теперь записей стало на одну больше
            DataSet1 dataSet_AfterInsert = BL.getPricess();

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.prices.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            int countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1)); ///!!!!!!!!

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["name"], Is.EqualTo(rowAfterInsert["name"]));
            Assert.That(rowForAdded["id_material"], Is.EqualTo(rowAfterInsert["id_material"]));
        }

        [Test]
        public void pricesDelete()
        {
            DataSet1 dataSetRead = BL.getPricess();

            List<DataRow> rows_list = dataSetRead.prices.Select("name = '000001'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.prices.Select("name = '000001'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                rowForDel.Delete();
            }
            BL.updatePricess(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше
            DataSet1 dataSet_AfterDel = BL.getPricess();
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.prices.Select("name = '000001'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(0));
        }
    }
}
