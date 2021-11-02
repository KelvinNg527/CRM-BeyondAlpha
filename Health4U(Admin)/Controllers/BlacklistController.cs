using DalSoft.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace Health4U_Admin_.Controllers
{
    public class BlacklistController : Controller
    {
        public ActionResult AddBlacklist()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddBlacklist(Blacklist model)
        {
            var user = Session["user"] as LoginModel;

            dynamic client = new RestClient("http://10.123.10.58:8080/Blacklist/BlacklistUserID?userID="
                + model.userID+"&&reason="
                +model.reason+"&&staffID="+user.ID);
            //var dt = new { uuid = uuid };
            var result = await client
                  .Headers(new Headers { { "Content-Type", "application/x-www-form-urlencoded" } })
                  //.Blacklist.WhitelistUUID
                  .Post();

            return RedirectToAction("ViewBlacklistUserID");
        }

        public ActionResult ViewBlacklistOpt( string Blacklist)
        {
            if (Blacklist == "UUID")
            {
                return RedirectToAction("ViewBlacklist");
            }
            else if (Blacklist == "UserID")
            {
                return RedirectToAction("ViewBlacklistUserID");
            }
            return RedirectToAction("ViewBlacklist");

        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ViewBlacklistUserID()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "UUID", Value = "UUID" });
            li.Add(new SelectListItem { Text = "UserID", Value = "UserID" });
            ViewData["Blacklist"] = li;
            List<Blacklist> Blacklist = new List<Blacklist>();
            var client = new RestClient("http://10.123.10.58:8080");
            var user = await client.Resource("Blacklist/GetBlacklistUserID").Get();
            foreach (var row in user)
            {   
                Blacklist.Add(new Blacklist
                {
                    blacklistTime = row.blacklistTime,
                    reason = row.reason,
                    userID = row.userID,
                    staffID = row.staffID
                });
            }
            return View(Blacklist);
        }

        // GET: Blacklist
        public async System.Threading.Tasks.Task<ActionResult> ViewBlacklist()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "UUID", Value = "UUID" });
            li.Add(new SelectListItem { Text = "UserID", Value = "UserID" });
            ViewData["Blacklist"] = li;
            List<Blacklist> Blacklist = new List<Blacklist>();
            var client = new RestClient("http://10.123.10.58:8080");
            var user = await client.Resource("Blacklist/GetBlacklistUUID").Get();
            foreach (var row in user)
            {
                Blacklist.Add(new Blacklist
                {
                    id = row.id,
                    reason = row.reason,
                    uuid = row.uuid,
                    staffID = row.staffID
                });
            }
            return View(Blacklist);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Whitelist(string uuid)
        {

            //var config = new Config().UseFormUrlEncodedHandler();

            dynamic client = new RestClient("http://10.123.10.58:8080/Blacklist/WhitelistUUID?uuid="+uuid);
            //var dt = new { uuid = uuid };
            var result = await client
                  .Headers(new Headers { { "Content-Type", "application/x-www-form-urlencoded" } })
                  //.Blacklist.WhitelistUUID
                  .Post();
            
            return RedirectToAction("ViewBlacklist");
        }


        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> WhitelistUserID(string id)
        {

            //var config = new Config().UseFormUrlEncodedHandler();

            dynamic client = new RestClient("http://10.123.10.58:8080/Blacklist/WhitelistUserID?id=" + id);
            //var dt = new { uuid = uuid };
            var result = await client
                  .Headers(new Headers { { "Content-Type", "application/x-www-form-urlencoded" } })
                  //.Blacklist.WhitelistUUID
                  .Post();

            return RedirectToAction("ViewBlacklistUserID");
        }

    }

}