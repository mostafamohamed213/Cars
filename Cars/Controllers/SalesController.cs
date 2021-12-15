using Cars.Models;
using Cars.Service;
using Cars.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cars.Controllers
{
    public class SalesController : Controller
    {
        public OrderServices orderServices { get; set; }      
        private readonly IStringLocalizer<SalesController> localizer;

        public SalesController(OrderServices _orderServices , IStringLocalizer<SalesController> _localizer)
        {
            orderServices = _orderServices;
            localizer = _localizer;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetOrders(int currentPage = 1)
        {

            try
            {              
                return View("Index", orderServices.getOrders(currentPage));
            }
            catch (Exception)
            {
                return View("_CustomError");
            }
        }
        [HttpGet]
        public IActionResult ChangeOrderTablelength(int length)
        {
            try
            {
                return View("Index", orderServices.getOrdersWithChangelength(1, length));
            }
            catch (Exception )
            {
                return View("_CustomError");
            }
        }
        [HttpGet]
        public IActionResult GetOrderLines(int currentPage = 1)
        {

            try
            {
                return View("OrderLines", orderServices.getOrderLines(currentPage));
            }
            catch (Exception)
            {
                return View("_CustomError");
            }
        }
        [HttpGet]
        public IActionResult ChangeOrderLinesTablelength(int length)
        {
            try
            {
                return View("OrderLines", orderServices.getOrderLinesWithChangelength(1, length));
            }
            catch (Exception)
            {
                return View("_CustomError");
            }
        }
        [HttpGet]
        public IActionResult ChangeOrderDraftTablelength(int length)
        {
            try
            {
                return View("Draft", orderServices.getOrdersDraftWithChangelength(1, length));
            }
            catch (Exception)
            {
                return View("_CustomError");
            }
        }
        [HttpGet]
        public IActionResult CreateOrder()
        {

            return View(new OrderViewModel { OrderID =-1});
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel orderModel)
        {
            //if (!orderModel.saveDraft)
            //{
                if (ModelState.IsValid)
                {
                    long orderId = orderServices.AddOrder(orderModel);
                    if (orderId > 0)
                    {
                        //return View("ViewOrder",orderServices.GetOrderByID(orderId));
                        return RedirectToAction("ViewOrder", new { OrderId = orderId });
                    }
                    else if (orderId == 0)
                    {
                        return View("_CustomError");
                    }
                    else if (orderId == -1)
                    {
                        return View("_CustomError");
                    }
                }
                return View(orderModel);
            //}
            //else
            //{
            //   int status =  orderServices.SaveOrderAsDraft(orderModel);
            //    if(status == 1 )
            //    {
            //        return RedirectToAction("Draft",new { currentPage = 1 });
            //    }
            //    else
            //    {
            //        return View("_CustomError");
            //    }
               
            //}
            
        }
        [HttpGet]
        public IActionResult Draft(int currentPage = 1)
        {
            try
            {
                return View("Draft", orderServices.getOrdersDraft(currentPage));
            }
            catch (Exception)
            {
                return View("_CustomError");
            }
        }
        //[HttpGet]
        //public IActionResult OpenOrderDraft(long OrderDraftId)
        //{
        //    try
        //    {
        //        DraftOrder draft = orderServices.getOrderDraftById(OrderDraftId);
        //        OrderViewModel model = new OrderViewModel()
        //        {
        //            Brand = string.IsNullOrEmpty(draft.Brand) ? null : draft.Brand,
        //            Chases = string.IsNullOrEmpty(draft.Chases) ? null : draft.Chases,
        //            CustomerPhone = string.IsNullOrEmpty(draft.Phone) ? null : draft.Phone,
        //            Model = string.IsNullOrEmpty(draft.Model) ? null : draft.Model,
        //            VehicleName = string.IsNullOrEmpty(draft.Name) ? null : draft.Name,
        //            WithMaintenance = draft.WithMaintenance.HasValue ? draft.WithMaintenance.Value : false,
        //            Year = string.IsNullOrEmpty(draft.Year) ? null : draft.Year,
        //            DraftId = draft.DraftOrderID,
        //            OrderID = -1
        //        };

        //        return View("CreateOrder", model);
        //    }
        //    catch (Exception)
        //    {
        //        return View("_CustomError");
        //    }
        //}
        [HttpGet]
        public IActionResult ViewOrder(long OrderId)
        {
            var order = orderServices.GetOrderByID(OrderId);
            ViewBag.types = orderServices.GetSelectListOrderDetailsType();
            //ViewBag.orderDetails = orderServices.GetOrderDetailsByOrderId(OrderId);
            return View(order);
        }
        [HttpGet]
        public IActionResult SaveAsDraft(long OrderId)
        {
            int status = orderServices.SaveAsDraft(OrderId);
            if (status == 1)
            {
                return RedirectToAction("Draft");
            }


            return View("_CustomError");
        }
        [HttpGet]
        public IActionResult SaveOrder(long OrderId)
        {
            int status = orderServices.SaveOrder(OrderId);
            if (status == 1)
            {
                
                return RedirectToAction("GetOrders");
            }


            return View("_CustomError");
        }

        [HttpPost]
        public IActionResult AddOrderDetails(string Items,int Quantity,int Type ,long orderId)
        {
            if (orderId <= 0)
            {
                return Json(new { status = -1, @object = $"{localizer["ErrorOccurred"]}"}); 
            }
            if (string.IsNullOrEmpty(Items) || Quantity <= 0 || Type <= 0)
            {
                
                   var test = localizer["CheckItemsQuantityAndType"];
                return Json(new { status = -1, @object = $"{localizer["CheckItemsQuantityAndType"]}" }); 
            }

            int status = orderServices.AddOrderDetails(Items,Quantity,Type, orderId);
            if (status == 1)
            {
                try
                {
                    var orderDetails = orderServices.GetOrderDetailsByOrderId(orderId);
                    return Json(new { status = 1, @object = orderDetails });
                }
                catch (Exception)
                {
                    return Json(new { status = -1, @object = $"{localizer["ErrorOccurred"]}" }); 
                }              
               
            }
            else 
            {
                return Json(new { status = -1, @object = $"{localizer["ErrorOccurred"]}" }); 
            }          
            
        }


    }
}
