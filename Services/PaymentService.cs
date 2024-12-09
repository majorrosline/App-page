using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GroceryManagement1.Models;
using PayPal;
using PayPal.Api;

namespace GroceryManagement1.Services
{
    public class PaymentService
    {
        private readonly PaypalService _paypalService;

        public PaymentService()
        {
            var paypalService = new PaypalService();
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
        }

        
        public async Task<string> ProcessPayment(GroceryManagement1.Models.Order order)
        {
            try
            {
                string access_token = await _paypalService.GetAccessTokenAsync();
                // Configure PayPal API Context
                var apiContext = new APIContext(access_token);

                // Set up PayPal payment details
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = "USD",
                            total = order.TotalAmount.ToString()
                        },
                        description = "Order #" + order.OrderId
                    }
                },
                    redirect_urls = new RedirectUrls
                    {
                        return_url = "https://yourapp.com/Order/OrderConfirmation",
                        cancel_url = "https://yourapp.com/Order/Cancel"
                    }
                };

                // Execute payment
                try
                {
                    var createdPayment = payment.Create(apiContext);
                    return "1";
                }
                catch (PayPalException ex)
                {
                    // Log the exception and return failure
                    Console.WriteLine(ex.Message);
                    return "0";
                }
            }
            catch (Exception ex)
            {
                // Handle any errors and provide feedback
                Console.WriteLine(ex.Message);
                return "0";
            }
        }
    }
}