using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WeekSevenCapstoneUseAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult GetRandomFact(DateTime date)
        {
            ViewBag.Date = date; 
            string day = date.Day.ToString();
            string month = date.Month.ToString();

            HttpWebRequest request =

                       WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/{day}/{month}/date?fragment=true&json=true");

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            // Adding keys to the header 
            request.Headers.Add("X-Mashape-Key", "https://numbersapi.p.mashape.com/{month}/{day}/date");

            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string DateData = reader.ReadToEnd();

            try
            {
                JObject jsonData = JObject.Parse(DateData);
                ViewBag.RandomFact = jsonData["text"];
            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            return View();
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}