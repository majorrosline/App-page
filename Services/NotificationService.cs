using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using GroceryManagement1.Models;
using Twilio.Types;

namespace GroceryManagement1.Services
{
    public class NotificationService
    {
        public static void SendNotification(User user, string message)
        {
            const string accountSid = "AC465962ff48f5cb5836314f4091aca9de";
            const string authToken = "cb81f5f8bd5a86dc8b321990a005d491";
            TwilioClient.Init(accountSid, authToken);

            var msg = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber("+19782957121"),
                to: new Twilio.Types.PhoneNumber(user.PhoneNumber)
            );
        }
    }
}


//namespace OnlineGroceryDeliverySystem
//{
//    class Program
//    {
//        static string accountSid = "your_account_sid";  // Replace with your Twilio account SID
//        static string authToken = "your_auth_token";    // Replace with your Twilio Auth Token
//        static string twilioPhoneNumber = "+1234567890"; // Replace with your Twilio phone number

//        static void Main(string[] args)
//        {
//            // Replace with the customer's phone number
//            string customerPhoneNumber = "+1234567890";

//            // Simulate each stage of the order
//            SendOrderPlacedMessage(customerPhoneNumber);
//            SendPackingReceivedMessage(customerPhoneNumber);
//            SendPackingCompletedMessage(customerPhoneNumber);
//            SendDeliveryReceivedMessage(customerPhoneNumber);
//            SendDeliveryCompletedMessage(customerPhoneNumber);
//        }

//        // Sends a message when the order is placed
//        static void SendOrderPlacedMessage(string customerPhoneNumber)
//        {
//            string message = "Thank you for your order! Your groceries are being prepared.";
//            SendMessage(customerPhoneNumber, message);
//        }

//        // Sends a message when the packing person has received the order
//        static void SendPackingReceivedMessage(string customerPhoneNumber)
//        {
//            string message = "Your order has been received by our packing team and is being packed.";
//            SendMessage(customerPhoneNumber, message);
//        }

//        // Sends a message when the packing is completed
//        static void SendPackingCompletedMessage(string customerPhoneNumber)
//        {
//            string message = "Your groceries are packed and ready for delivery.";
//            SendMessage(customerPhoneNumber, message);
//        }

//        // Sends a message when the delivery person receives the order
//        static void SendDeliveryReceivedMessage(string customerPhoneNumber)
//        {
//            string message = "Our delivery team has received your order and is on the way to your location.";
//            SendMessage(customerPhoneNumber, message);
//        }

//        // Sends a message when the delivery is completed
//        static void SendDeliveryCompletedMessage(string customerPhoneNumber)
//        {
//            string message = "Your groceries have been delivered! Thank you for using our service.";
//            SendMessage(customerPhoneNumber, message);
//        }

//        // Helper method to send SMS
//        static void SendMessage(string toPhoneNumber, string message)
//        {
//            try
//            {
//                TwilioClient.Init(accountSid, authToken);

//                var messageOptions = new CreateMessageOptions(
//                    new PhoneNumber(toPhoneNumber))
//                {
//                    From = new PhoneNumber(twilioPhoneNumber),
//                    Body = message
//                };

//                var sentMessage = MessageResource.Create(messageOptions);
//                Console.WriteLine($"Message sent to {toPhoneNumber}: {sentMessage.Body}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error sending message: {ex.Message}");
//            }
//        }
//    }
//}