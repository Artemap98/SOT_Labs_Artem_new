using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BL;
using Project;
namespace WebApplication3
{
    /// <summary>
    /// Сводное описание для WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet1 getCustomers()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getCustomers();
        }

        [WebMethod]
        public DataSet1 getStaff()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getStaff();
        }

        [WebMethod]
        public DataSet1 getMaterialss()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getMaterialss();
        }

        [WebMethod]
        public DataSet1 getPricess()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getPricess();
        }

        [WebMethod]
        public DataSet1 getServicess()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getServicess();
        }
    }
}
