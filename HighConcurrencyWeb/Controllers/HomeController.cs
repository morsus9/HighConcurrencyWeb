using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HighConcurrencyWeb.Models;
using HighConcurrencyWeb.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace HighConcurrencyWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 抢单接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GrabSingle(User user)
        {
            //使用后台任务
            //BackgroundJob.Enqueue(() => MqPublish.AddQueue(user));
            MQPublish.AddQueue(user);
            return Json(new { Status = "OK" });
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCount()
        {

            using (var dbcontext = Context.DBContext())
            {
                int Count = dbcontext.Persons.Count();
                return Json(new { Count = Count });
            }

        }


        [HttpPost]
        public ActionResult test()
        {

            MQPublish.Write();
            return Json(new { Status = "OK" });
        }

        //[HttpPost]
        //public ActionResult test()
        //{
        //    Person person = new Person { Id = 1,Id2 = "1",Name = "test" };

        //    using (var context = Context.DBContext())
        //    {
        //        var p =  context.Persons.Add(person);
        //        context.SaveChanges();
        //    }

        //    return Json(new { Status = "OK" });
        //}
    }
}