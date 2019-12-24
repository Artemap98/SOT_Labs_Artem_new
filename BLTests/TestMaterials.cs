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



namespace BLTests
{
    [TestFixture]
    public class TestMaterials
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
            DataSet1 dataSet = BL.getMaterials();
            List<DataRow> list = dataSet.materials.Select().OfType<DataRow>().ToList();
            list.Sort((a, b) => ((int)a["id"]).CompareTo((int)b["id"]));

            Assert.That(list.Count, Is.EqualTo(5));
            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("Керамические брекеты"));
            Assert.That((string)(list[0]["manufacturer"]), Is.EqualTo("SIA Orthodontic"));  
        }

        [Test]
        public void materialsById()
        {
            DataSet1 dataSet = BL.getMaterials();
            List<DataRow> list = dataSet.materials.Select("id = 2").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("Керамические брекеты"));
            Assert.That((string)(list[0]["manufacturer"]), Is.EqualTo("SIA Orthodontic"));  
        }

        [Test]
        public void materialsByName()
        {
            DataSet1 dataSet = BL.getMaterials();

            List<DataRow> list = dataSet.materials.Select("name = 'Керамические брекеты'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("Керамические брекеты"));
            Assert.That((string)(list[0]["manufacturer"]), Is.EqualTo("SIA Orthodontic"));
        }

        [Test]
        public void materialsByCode()
        {
            DataSet1 dataSet = BL.getMaterials();

            List<DataRow> list = dataSet.materials.Select("manufacturer = 'SIA Orthodontic'").OfType<DataRow>().ToList();

            Assert.That(list.Count, Is.EqualTo(1));

            Assert.That((int)(list[0]["id"]), Is.EqualTo(2));
            Assert.That((string)(list[0]["name"]), Is.EqualTo("Керамические брекеты"));
            Assert.That((string)(list[0]["manufacturer"]), Is.EqualTo("SIA Orthodontic"));
        }

        [Test]
        public void materialsUpdate()
        {
            DataSet1 dataSet = BL.getMaterials();
            List<DataRow> list = dataSet.materials.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));


            // Обновляем первую запись
            DataSet1.materialsRow oldM = null;

            String oldName = "";

            oldM = dataSet.materials[0];
            oldName = oldM.name;

            dataSet.materials[0].name = oldM.name + "_changed";
            BL.updateMaterials(dataSet);


            // Заново читаем из базы, проверяем, что поменялось
            DataSet1 dataSetUpdated = BL.getMaterials();

            // достаем из датасета все записи таблицы
            List<DataRow> list_3 = dataSetUpdated.materials.Select("").OfType<DataRow>().ToList();
            // Сортируем по id
            list_3.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Проверяем что записей столько же
            Assert.That(list_3.Count, Is.EqualTo(5));

            // Достае ту же запись
            List<DataRow> rows_list = dataSet.materials.Select("id = " + oldM.id).OfType<DataRow>().ToList();
            // Проверяем что по такому id одна запись
            Assert.That(rows_list.Count, Is.EqualTo(1));

            DataSet1.materialsRow updatedM = dataSetUpdated.materials[0];

            Assert.That(oldM.id, Is.EqualTo(updatedM.id));

            Assert.That(oldName, !Is.EqualTo(updatedM.name));
            Assert.That(oldName + "_changed", Is.EqualTo(updatedM.name));
        }

        [Test]
        public void materialsAdd()
        {
            DataSet1 dataSetRead = BL.getMaterials();

            int countRowBefore = 0;

            List<DataRow> rows_list = dataSetRead.materials.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до внесения новой
            countRowBefore = rows_list.Count();


            // Добавляем в базу новую запись
            List<DataRow> list_1 = dataSetRead.materials.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_1.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));       
            DataRow rowForAdded = dataSetRead.materials.NewRow();

            rowForAdded["name"] = "ОАО \"КРАСНОЯРСКЛЕСОМАТЕРИАЛЫ2\"";
            rowForAdded["manufacturer"] = "47828138";

            dataSetRead.materials.Rows.Add(rowForAdded);

            List<DataRow> list_2 = dataSetRead.materials.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            list_2.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            BL.updateMaterials(dataSetRead);

            // проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterInsert = BL.getMaterials();

            List<DataRow> rows_list_AfterInsert = dataSet_AfterInsert.materials.Select("").OfType<DataRow>().ToList();
            // Сортируем строки по айдишнику в порядке возрастания
            rows_list_AfterInsert.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            int countRowAfter = rows_list_AfterInsert.Count();

            // Проверяем, что записей стало на одну больше
            Assert.That(countRowAfter - countRowBefore, Is.EqualTo(1));

            // Берем последнюю добавленную запись( для этого сортируем )
            DataRow rowAfterInsert = rows_list_AfterInsert[rows_list_AfterInsert.Count - 1];
            // Проверяем что запись добавилась правильно
            Assert.That(rowForAdded["name"], Is.EqualTo(rowAfterInsert["name"]));
            Assert.That(rowForAdded["manufacturer"], Is.EqualTo(rowAfterInsert["manufacturer"]));
        }

        [Test]
        public void materialsDelete()
        {
            DataSet1 dataSetRead = BL.getMaterials();

            List<DataRow> rows_list = dataSetRead.materials.Select("name = 'Керамические брекеты'").OfType<DataRow>().ToList();
            // Сортируем строки по id в порядке возрастания
            rows_list.Sort((x, y) => ((int)x["id"]).CompareTo((int)y["id"]));
            // Количество записей до удаления
            int countRowBefore = rows_list.Count();
            Assert.That(countRowBefore, Is.EqualTo(1));

            //удаляем
            List<DataRow> list_1 = dataSetRead.materials.Select("name = 'Керамические брекеты'").OfType<DataRow>().ToList();
            foreach (DataRow rowForDel in list_1)
            {
                rowForDel.Delete();
            }
            BL.updateMaterials(dataSetRead);
            dataSetRead.AcceptChanges();

            // проверяем что теперь записей стало на одну больше

            DataSet1 dataSet_AfterDel = BL.getMaterials();
            List<DataRow> rows_list_AfterInsert = dataSet_AfterDel.materials.Select("name = 'Керамические брекеты'").OfType<DataRow>().ToList();

            Assert.That(rows_list_AfterInsert.Count, Is.EqualTo(1));//КОСТЫЛЬ
        }
    }
}