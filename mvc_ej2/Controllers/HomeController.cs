using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;


namespace mvc_ej2.Controllers
{
    public class HomeController : Controller
    {

        public static List<OrdersDetails> orddata = new List<OrdersDetails>();
        public static List<Employee1Details> empdata = new List<Employee1Details>();
        public ActionResult Index()
        {
            if (orddata.Count() == 0)
                BindData();

            ViewBag.datasource = orddata.ToArray();
            return View();
        }
        public ActionResult Child()
        {
            if (orddata.Count() == 0)
                BindData();

            ViewBag.datasource = orddata.ToArray();
            return View();
        }

        public ActionResult Getdata()
        {
            IEnumerable DataSource = OrdersDetails.GetAllRecords();
            return Json(DataSource);
        }

        public void BindData()
        {
            int code = 10000;
            for (int i = 1; i < 3; i++)
            {
                orddata.Add(new OrdersDetails(code + 2, "ANATR", i + 0, 3.3 * i, true, new DateTime(1995, 7, 2, 2, 3, 5), "Madrid", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                orddata.Add(new OrdersDetails(code + 3, "ANTON", i + 1, 4.3 * i, true, new DateTime(2012, 12, 25, 2, 3, 5), "Cholchester", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                orddata.Add(new OrdersDetails(code + 4, "BLONP", i + 2, 5.3 * i, false, new DateTime(2002, 12, 25, 2, 3, 5), "Marseille", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                orddata.Add(new OrdersDetails(code + 5, "BOLID", i + 3, 6.3 * i, true, new DateTime(1953, 02, 18, 05, 2, 4), "Tsawassen", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                code += 5;
            }
        }


        public ActionResult UrlDatasource(TestDm dm)
        {

            IEnumerable DataSource = orddata.ToList();
            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<OrdersDetails>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        public ActionResult Update(OrdersDetails value)
        {
            var ord = value;
            OrdersDetails val = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.EmployeeID = ord.EmployeeID;
            val.CustomerID = ord.CustomerID;
            return Json(value);
        }

        public ActionResult Insert(OrdersDetails value)
        {
            OrdersDetails.GetAllRecords().Insert(0, value);
            return Json(value);
        }

        public ActionResult Delete(int key)
        {
            OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(or => or.OrderID == key).FirstOrDefault());
            var data = OrdersDetails.GetAllRecords();
            return Json(data);
        }


        public class TestDm : DataManagerRequest
        {
            public int flag { get; set; }
        }

        public class CRUDModel
        {
            //public List<OrdersDetails> Added { get; set; }
            //public List<OrdersDetails> Changed { get; set; }
            //public List<OrdersDetails> Deleted { get; set; }
            public OrdersDetails value { get; set; }
            public int key { get; set; }
            public string action { get; set; }
            public string keycolumn { get; set; }
            public int IDMASTER { get; set; }
        }

        public class OrdersDetails
        {
            public OrdersDetails()
            {

            }
            public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, DateTime OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress)
            {
                this.OrderID = OrderID;
                this.CustomerID = CustomerId;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                this.Verified = Verified;
                this.OrderDate = OrderDate;
                this.ShipName = ShipName;
                this.ShipCountry = ShipCountry;
                this.ShippedDate = ShippedDate;
                this.ShipAddress = ShipAddress;
            }

            public static List<OrdersDetails> GetAllRecords()
            {
                List<OrdersDetails> order = new List<OrdersDetails>();
                int code = 10000;
                for (int i = 1; i < 10; i++)
                {
                    order.Add(new OrdersDetails(code + 1, "ALFKI", i + 0, 2.3 * i, false, new DateTime(1991, 05, 15), "Berlin", "Simons bistro", "Denmark", new DateTime(1996, 7, 16), "Kirchgasse 6"));
                    order.Add(new OrdersDetails(code + 2, "ANATR", i + 2, 3.3 * i, true, new DateTime(1990, 04, 04), "Madrid", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                    order.Add(new OrdersDetails(code + 3, "ANTON", i + 1, 4.3 * i, true, new DateTime(1957, 11, 30), "Cholchester", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                    order.Add(new OrdersDetails(code + 4, "BLONP", i + 3, 5.3 * i, false, new DateTime(1930, 10, 22), "Marseille", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                    order.Add(new OrdersDetails(code + 5, "BOLID", i + 4, 6.3 * i, true, new DateTime(1953, 02, 18), "Tsawassen", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                    code += 5;
                }
                return order;
            }


            public int? OrderID { get; set; }
            public string CustomerID { get; set; }
            public int? EmployeeID { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public DateTime OrderDate { get; set; }

            public string ShipName { get; set; }

            public string ShipCountry { get; set; }

            public DateTime ShippedDate { get; set; }
            public string ShipAddress { get; set; }
        }
        public class Employee1Details
        {
            public static List<Employee1Details> order = new List<Employee1Details>();
            public Employee1Details()
            {

            }
            public Employee1Details(int EmployeeId, string FirstName, string LastName, int ReportsTO)
            {
                this.EmployeeID = EmployeeId;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.ReportsTo = ReportsTo;
            }
            public static List<Employee1Details> GetAllRecords()
            {
                if (order.Count() == 0)
                {
                    for (int i = 1; i < 2; i++)
                    {
                        order.Add(new Employee1Details(i + 0, "Nancy", "Davolio", i + 0));
                        order.Add(new Employee1Details(i + 1, "Andrew", "Fuller", i + 3));
                        order.Add(new Employee1Details(i + 2, "Janet", "Leverling", i + 2));
                        order.Add(new Employee1Details(i + 3, "Margaret", "Peacock", i + 1));

                    }
                }
                return order;
            }


            public int? EmployeeID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? ReportsTo { get; set; }
        }
    }
}