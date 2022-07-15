using Domain.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reserve_Space.Controllers
{
    public class OrderController : Controller
    {

        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44395/api/Admin/GetAllActiveOrders";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;

            return View(data);
        }

        public FileContentResult CreateInvoice(Guid? orderId)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44395/api/Admin/GetDetailsForOrder";

            var model = new
            {
                Id = orderId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            document.Content.Replace("{{Email}}", data.OrderedBy.Email);

            StringBuilder sb = new StringBuilder();
            double total = 0;
            foreach (var spaceInOrder in data.Spaces)
            {
                total += spaceInOrder.Quantity * spaceInOrder.SelectedSpace.price;
                sb.AppendLine(spaceInOrder.SelectedSpace.name + " with quantity of: " + spaceInOrder.Quantity + " and price of: " + spaceInOrder.SelectedSpace.price + "MKD");
            }

            document.Content.Replace("{{SpaceList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "MKD");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");
        }
    }
}
