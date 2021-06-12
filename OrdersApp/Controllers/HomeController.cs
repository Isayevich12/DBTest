using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DBTest;
using OrdersApp.ViewModels;
using DBTest.Entities;
using OrdersApp.Services;

namespace OrdersApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            OrderViewModel model;

            using (var db = new Logic())
            {
                model = new OrderViewModel()
                {
                    Clients = db.GetAllClients().ToList(),
                    Orders = db.GetAllOrders().ToList()
                };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddClient()
        {
   
            return View("AddClient");

        }


        [HttpPost]
        public IActionResult AddClient(ClientModel client)
        {
            ViewBag.Client = $"{client.Name} {client.Adress}";

            Client newClient = new Client { Name = client.Name, Adress = client.Adress };

            using (var db = new Logic())
            {
                db.AddClient(newClient);

                return View("ResultAdditionByClient");
            }             
        }

        
        public IActionResult DeleteClient(string name, string adress)
        {
            Client client = new Client {Name = name, Adress =adress };
            ViewBag.Client = $"{name} {adress}"; 
            using (var db = new Logic())
            {
                db.RemoveClient(client);

                return View("DeleteClient");
            }     
        }

        [HttpGet]
        public IActionResult EditClient( string name, string adress)
        {
            ForSaveOldStateByClient.Name = name;
            ForSaveOldStateByClient.Addres = adress;

            ViewData["Client"] = $"{name} {adress}";
            return View("EditClient");

        }

        [HttpPost]
        public IActionResult EditClient(ClientModel client, string name, string adress)
        {          
            Client oldClient = new Client { Name = ForSaveOldStateByClient.Name, Adress = ForSaveOldStateByClient.Addres };
            Client newClient = new Client { Name = client.Name, Adress = client.Adress };
            ViewBag.Info = $"Изначальные данные : {oldClient.Name} {oldClient.Adress}. Новые данные : {newClient.Name} {newClient.Adress}";
            using (var db = new Logic())
            {
                db.ChangeInfoByClient(oldClient, newClient);

                return View("ResultEditionByClient");
            }
        }

        [HttpGet]
        public IActionResult ClientOrders(string name, string adress)
        {

            using (var db = new Logic())
            {
                var queryCurrentClient = db.GetAllClients().Where(n => n.Name == name && n.Adress == adress).FirstOrDefault();
                List<Order> queryOrders = db.GetAllOrders().Where(n => n.ClientId == queryCurrentClient.Id).ToList();
                OrdersByCurrentClient ordersByCurrentClient = new OrdersByCurrentClient { Client = queryCurrentClient, Orders = queryOrders };
                return View("ClientOrders", ordersByCurrentClient);
            }
           

        }

        [HttpGet]
        public IActionResult AddOrder(int clientId)
        {
            using (var db = new Logic())
            {               

                var queryClient = db.GetAllClients().Where(n => n.Id == clientId).FirstOrDefault();
                ForSaveOldStateByClient.Name = queryClient.Name;
                ForSaveOldStateByClient.Addres = queryClient.Adress;

                ViewBag.Client = queryClient;

                return View("AddOrder");
            }     

        }

        [HttpPost]
        public IActionResult AddOrder(OrderModel orderModel)
        {

            using (var db = new Logic())
            {
                var currentClient = db.GetAllClients().Where(n => n.Name == ForSaveOldStateByClient.Name && n.Adress == ForSaveOldStateByClient.Addres).FirstOrDefault();
                Order currentOrder = new Order {Amount = orderModel.Amount, Cost = orderModel.Cost, Date = orderModel.Date, Description = orderModel.Description };
                db.AddOrderByClient(currentClient, currentOrder);
                ViewBag.Client = currentClient;
                return View("ResultAdditionByOrder");
            }
        }

        public IActionResult DeleteOrder(int orderId, int clientId)
        {
            
            using (var db = new Logic())
            {
                var queryClient = db.GetAllClients().Where(n => n.Id == clientId).FirstOrDefault();
                var queryOrder = db.GetAllOrders().Where(n => n.Id == orderId).FirstOrDefault();
                db.RemoveOrderByClient(queryClient, queryOrder);
                ViewBag.Client = queryClient;

                return View("ResultDeletionByOrder");
            }
        }


    }
}
