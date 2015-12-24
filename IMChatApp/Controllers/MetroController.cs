using Base.Entities.Models;
using Base.Entities.UIModels;
using IMChatApp.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.UI.Controllers
{
    public class MetroController : Controller
    {
        // GET: Metro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Forms()
        {
            return View();
        }
        public ActionResult Widgets()
        {
            return View();
        }
        public ActionResult Messages()
        {
            return View();
        }
        public ActionResult Tables()
        {
            return View();
        }
        public ActionResult Icons()
        {
            return View();
        }
        public ActionResult Tasks()
        {
            return View();
        }
        public ActionResult Charts()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult Ui()
        {
            return View();
        }

        public ActionResult GroupChat()
        {
            return View();
        }

        public ActionResult Chat()
        {
            ChatUser user = new ChatUser();
            if (TempData["user"] != null)
                user = (ChatUser)TempData["user"];
            return View(user);
        }
        public ActionResult ChatCtrl()
        {
            return View();
        }
        public ActionResult Login()
        {
            IEnumerable<Gender> GenderType = Enum.GetValues(typeof(Gender))
                                                       .Cast<Gender>();
            ViewBag.error = "";
            return View( new ChatUser());
        }
        [HttpPost]
        public ActionResult Login(ChatUser user)
        {
            if (SRChat.chatUsers.Where(x => x.Nick == user.Nick).Count() > 0)
            {
                ViewBag.error = "User name alredy exists";
                return View("login",user);
            }
            else
            {
                //SRChat.chatUsers.Add(new ChatUser { Nick = user.Nick });
                TempData["user"] = user;
                return RedirectToAction("chat");
            }

           
        }
    }
}