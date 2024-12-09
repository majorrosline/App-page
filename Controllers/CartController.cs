using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryManagement1.Commands;
using GroceryManagement1.Services;
using GroceryManagement1.Models;
using System.Threading.Tasks;
using PayPal.Api;

namespace GroceryManagement1.Controllers
{

    public class CartController : Controller
    {

        private GroceryDbContext db = new GroceryDbContext();

        private readonly PaymentService _paymentService;
        private readonly CommandInvoker _commandInvoker;

        public CartController()
        {
            _paymentService = new PaymentService();
            _commandInvoker = new CommandInvoker();
        }
        // GET: Cart
        public ActionResult Cart()
        {
            return View("Cart");
        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PlaceOrder(Models.Order order, List<OrderItem> cartItems, decimal totalAmount)
        {
            if (cartItems == null || !cartItems.Any())
            {
                ModelState.AddModelError("", "No items in the cart.");
                return View(order);
            }

            // Validate the order
            //if (!ModelState.IsValid)
            //{
            //    return View(order);
            //}

            order.UserId = Convert.ToInt32(Session["UserId"]);
            order.OrderBy = Convert.ToInt32(Session["UserId"]);
            order.Status = "Pending";
            order.CreatedAt = DateTime.Now;
            order.TotalAmount = totalAmount;

            // Save order in the database
            db.Orders.Add(order);
            await db.SaveChangesAsync();


            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId, // Use the generated OrderId
                    ProductId = item.ProductId,    // Reference to the product
                    Quantity = item.Quantity,
                    Price = item.Price       // Per-item price
                };

                db.OrderItems.Add(orderItem);
            }

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Inventory", new { orderId = order.OrderId });

        }
    }
}