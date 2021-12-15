using Cars.Models;
using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cars.Consts;

namespace Cars.Service
{
    public class OrderServices
    {
        public CarsContext db { get; set; }
        public OrderServices(CarsContext carsContext)
        {
            db = carsContext;     
        }

        public PagingViewModel<Order> getOrdersWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.IndexOrdersMaxRows = length;
            return getOrders(currentPageIndex);
        }
        public PagingViewModel<OrderDetails> getOrderLinesWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.IndexOrderLinesMaxRows = length;
            return getOrderLines(currentPageIndex);
        }
        public PagingViewModel<Order> getOrdersDraftWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.IndexOrdersDraftMaxRows = length;
            return getOrdersDraft(currentPageIndex);
        }
        public PagingViewModel<Order> getOrders(int currentPage)
        {
           var orders= db.Orders.Where(c => c.Enabled.HasValue && c.Enabled.Value).Include("Vehicle").Include("Customer").Include("Customer.CustomerContacts").Skip((currentPage - 1) * TablesMaxRows.IndexOrdersMaxRows).Take(TablesMaxRows.IndexOrdersMaxRows).ToList();

            PagingViewModel<Order> viewModel = new PagingViewModel<Order> ();
            //var Brands = unitOfWork.Brands.
            //   FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryBrandIndex, TablesMaxRows.InventoryBrandIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = orders.ToList();
            var itemsCount = db.Orders.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.IndexOrdersMaxRows));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.IndexOrdersMaxRows;
            return viewModel;
        }
        public PagingViewModel<OrderDetails> getOrderLines(int currentPage)
        {
            var orders = db.OrderDetails.Where(c=>c.Enabled.HasValue && c.Enabled.Value).Skip((currentPage - 1) * TablesMaxRows.IndexOrderLinesMaxRows).Take(TablesMaxRows.IndexOrderLinesMaxRows).ToList();

            PagingViewModel<OrderDetails> viewModel = new PagingViewModel<OrderDetails>();
            //var Brands = unitOfWork.Brands.
            //   FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryBrandIndex, TablesMaxRows.InventoryBrandIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = orders.ToList();
            var itemsCount = orders.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.IndexOrderLinesMaxRows));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.IndexOrderLinesMaxRows;
            return viewModel;
        }
        public PagingViewModel<Order> getOrdersDraft(int currentPage)
        {
            var orders = db.Orders.Include("Vehicle").Include("Customer").Include("Customer.CustomerContacts").Where(c=> !c.Enabled.HasValue || !c.Enabled.Value).Skip((currentPage - 1) * TablesMaxRows.IndexOrdersDraftMaxRows).Take(TablesMaxRows.IndexOrdersDraftMaxRows).ToList();

            PagingViewModel<Order> viewModel = new PagingViewModel<Order>();
            //var Brands = unitOfWork.Brands.
            //   FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryBrandIndex, TablesMaxRows.InventoryBrandIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = orders.ToList();
            var itemsCount = orders.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.IndexOrdersDraftMaxRows));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.IndexOrdersDraftMaxRows;
            return viewModel;
        }
        public Order GetOrderByID(long orderId)
        {
            try
            {
                Order order = db.Orders.Where(c => c.OrderID == orderId).Include("Vehicle").Include("Customer")
                                .Include("Customer.CustomerContacts").Include("OrderDetails").Include("OrderDetails.OrderDetailsType").FirstOrDefault();
                if (order is not null)
                {
                    return order;
                }
                return null;
            }
            catch (Exception )
            {
               return null;
            }
          
        }

        //internal int SaveOrderAsDraft(OrderViewModel orderModel)
        //{
        //    try
        //    {
        //        DraftOrder draftOrder = new DraftOrder()
        //        {
        //            DTsCreate = DateTime.Now,
        //            SystemUserCreate = "-1",
        //            Brand = string.IsNullOrEmpty(orderModel.Brand) ? null : orderModel.Brand,
        //            Chases = string.IsNullOrEmpty(orderModel.Chases) ? null : orderModel.Chases,
        //            EmployeeBranchID = 10,
        //            Model = string.IsNullOrEmpty(orderModel.Model) ? null : orderModel.Model,
        //            Name = string.IsNullOrEmpty(orderModel.VehicleName) ? null : orderModel.VehicleName,
        //            Year = string.IsNullOrEmpty(orderModel.Year) ? null : orderModel.Year,
        //            Phone = string.IsNullOrEmpty(orderModel.CustomerPhone) ? null : orderModel.CustomerPhone,
        //            WithMaintenance = orderModel.WithMaintenance,
        //            Enable = true
        //        };
        //        db.DraftOrders.Add(draftOrder);
        //        db.SaveChanges();
        //        return 1;
        //    }
        //    catch (Exception)
        //    {
        //        return -1;
        //    }           
        //}

        internal int SaveAsDraft(long orderId)
        {
           
            Order order = db.Orders.Include("OrderDetails").FirstOrDefault(c => c.OrderID == orderId);
            Layer  _layer= db.Layers.FirstOrDefault(c => c.LayerID == 1);
            if (order is not null)
            {
                order.Enabled = false;
                if (order.OrderDetails.Count > 0)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        item.Enabled = false;
                        item.LayerID = _layer.LayerID;
                        item.Layer = _layer;
                    }
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        internal int SaveOrder(long orderId)
        {
            Order order = db.Orders.Include("OrderDetails").FirstOrDefault(c => c.OrderID == orderId);
            Layer _layer = db.Layers.FirstOrDefault(c => c.LayerID == 2);
            if (order is not null)
            {
                order.Enabled = true;
                if (order.OrderDetails.Count > 0)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        item.Enabled = true;
                        item.LayerID = _layer.LayerID;
                        item.Layer = _layer;
                    }
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        internal DraftOrder getOrderDraftById(long orderDraftId)
        {
            return db.DraftOrders.FirstOrDefault(C => C.DraftOrderID == orderDraftId && C.Enable);
        }

        public long AddOrder(OrderViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    DateTime now = DateTime.Now;
                    Vehicle vehicle = new Vehicle()
                    {
                        DTsCreate = now,
                        SystemUserCreate = "-1",
                        Chases = model.Chases.Trim(),
                        Name = model.VehicleName.Trim(),
                        Year = model.Year?.Trim(),
                        Model = model.Model?.Trim(),
                        Brand = model.Brand?.Trim(),
                    };
                    Customer customer = new Customer()
                    {
                        DTsCreate = now,
                        SystemUserCreate = "-1"
                    };
                    CustomerContact contact = new CustomerContact()
                    {
                        DTsCreate = now,
                        SystemUserCreate = "-1",
                        CustomerID = customer.CustomerID,
                        Customer =customer,
                        Phone = model.CustomerPhone,
                        HasTelegram = false,
                        HasWhatsapp = false
                    };
                    Order order = new Order()
                    {
                        DTsCreate = now,
                        SystemUserCreate = "-1",
                        Customer = customer,
                        EmployeeBranchID = 10,
                        Vehicle = vehicle,
                        WithMaintenance = model.WithMaintenance
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();
                    db.CustomerContacts.Add(contact);
                    db.SaveChanges();

                    //if (model.DraftId.HasValue && model.DraftId.Value>0)
                    //{
                    //  DraftOrder draft=  getOrderDraftById(model.DraftId.Value);
                    //    draft.Enable = false;
                    //    db.SaveChanges();
                    //}
                    return order.OrderID;
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
           
        }

        internal List<OrderDetailsViewModel> GetOrderDetailsByOrderId(long orderid)
        {           
           var List = db.OrderDetails.Where(c => c.OrderID == orderid).Include("OrderDetailsType").ToList();
            if (List.Count > 0)
            {
                List<OrderDetailsViewModel> model = new List<OrderDetailsViewModel>();
                foreach (var item in List)
                {
                    model.Add(new OrderDetailsViewModel() { IsApproved=item.IsApproved,Items=item.Items,OrderDetailsID=item.OrderDetailsID,Quantity=item.Quantity,type=item.OrderDetailsType.NameEn});
                }
                return model;
            }
            return null;
        }

        internal int AddOrderDetails(string items, int quantity, int type,long orderID)
        {
          
            try
            {
                var order = db.Orders.FirstOrDefault(c => c.OrderID == orderID);
                OrderDetails orderDetails = new OrderDetails()
                {
                    DTsCreate = DateTime.Now,
                    SystemUserCreate = "-1",
                    Items = items,
                    Quantity = quantity,
                    OrderDetailsTypeID = type,                  
                    OrderID = orderID,
                    BranchID = -1,
                    //Enabled =order.Enabled
                };
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        internal SelectList GetSelectListOrderDetailsType()
        {
            var OrderDetailsTypes = db.OrderDetailsType.ToList();
            if (OrderDetailsTypes.Count() > 0)
            {
                return new SelectList(OrderDetailsTypes, "OrderDetailsTypeID", "NameEn");
            }
            return null;
        }
    }
}
