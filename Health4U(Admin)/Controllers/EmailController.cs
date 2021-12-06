using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;

namespace CRM.Controllers
{
    using System.Net;
    using System.Net.Mail;
    using static DataLibrary.BusinessLogic.EmailProcessor;

    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult ViewEmail()
        {
            var filter = "";
            filter = TempData["filter"] as string;
            if (filter == "1")
            {
                List<Email> Email = new List<Email>();
                var data = LoadEmailTrue();

                foreach (var row in data)
                {
                    Email.Add(new Email
                    {
                        email_ID = row.email_ID,
                        email_AddressFrom = row.email_AddressFrom,
                        email_AddressTo = row.email_AddressTo,
                        email_Name = row.email_Name,
                        email_PreviewSubject = row.email_PreviewSubject,
                        email_subject = row.email_subject,
                        email_Text = row.email_Text,
                        isSend = row.isSend
                    });
                }
                return View(Email);
            }
            else
            {
                List<Email> Email = new List<Email>();
                var data = LoadEmailFalse();

                foreach (var row in data)
                {
                    Email.Add(new Email
                    {
                        email_ID = row.email_ID,
                        email_AddressFrom = row.email_AddressFrom,
                        email_AddressTo = row.email_AddressTo,
                        email_Name = row.email_Name,
                        email_PreviewSubject = row.email_PreviewSubject,
                        email_subject = row.email_subject,
                        email_Text = row.email_Text,
                        isSend = row.isSend
                    });
                }
                return View(Email);

            }

        }

        public ActionResult AddEmail()
        {


            var recordsUser = LoadUser().ToList();
            SelectList UserList = new SelectList(recordsUser, "user_email", "UserShort");
            ViewBag.UserList = UserList;

            return View();
        }

        [HttpPost]
        public ActionResult AddEmail(Email model)
        {
            List<Email> Email = new List<Email>();
            Email app = new Email();
            var data = LoadEmail();

            var lastEmail = data.AsQueryable().OrderByDescending(c => c.email_ID).FirstOrDefault();

            if (lastEmail == null)
            {
                app.email_ID = "E001";
            }
            else
            {
                app.email_ID = "E" + (Convert.ToInt32(lastEmail.email_ID.Substring
                    (1, lastEmail.email_ID.Length - 1)) + 1).ToString("D3");
            }
            int recordsCreated = CreateEmail(app.email_ID, model.email_Name,
         model.email_subject, model.email_AddressFrom, model.email_AddressTo, model.email_Text, false);

            return RedirectToAction("index", "Home");
        }

        public ActionResult Details(string id)
        {
            var recordsUser = LoadUser().ToList();
            SelectList UserList = new SelectList(recordsUser, "user_email", "UserShort");
            ViewBag.UserList = UserList;
            var recordsselected = SelectsEmail(id);
            var model = new Email()
            {
                email_ID = recordsselected.email_ID,
                email_Name = recordsselected.email_Name,
                email_subject = recordsselected.email_subject,
                email_AddressTo = recordsselected.email_AddressTo,
                email_Text = recordsselected.email_Text,
                isSend = recordsselected.isSend

            };

            return View(model);
        }

        //Send
        [HttpPost]
        public ActionResult Details(Email model, string value)
        {
            var infoSelectFrom = SelectEmail(model.email_AddressFrom);
            var infoSelectTo = SelectEmail(model.email_AddressTo);

            if (value == "Edit")
            {
                int recordsupdated =
                   UpdateEmail(model.email_ID, model.email_Name, model.email_subject,
                model.email_AddressFrom, model.email_AddressTo, model.email_Text, model.isSend);

                return RedirectToAction("ViewEmail");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var senderEmail = new MailAddress(model.email_AddressFrom, infoSelectFrom.user_nickname);
                        var receiverEmail = new MailAddress(model.email_AddressTo, infoSelectTo.user_nickname);
                        var password = infoSelectFrom.user_password;
                        var sub = model.email_subject;
                        var body = "Dear Sir/Madam," + Environment.NewLine + Environment.NewLine +
                       model.email_Text + Environment.NewLine
                     + Environment.NewLine + Environment.NewLine +
                        "Best Regards" + Environment.NewLine + infoSelectFrom.user_nickname;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                            var updatestatus = UpdateStatus(true, model.email_ID);
                        }
                        return RedirectToAction("ViewEmail");
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "Some Error";
                    return RedirectToAction("ViewEmail");

                }
            }

            return RedirectToAction("ViewEmail");
        }




        [HttpGet]
        public ActionResult EditDetails(string email_ID)
        {
            var recordsUser = LoadUser().ToList();
            SelectList UserList = new SelectList(recordsUser, "user_email", "UserShort");
            ViewBag.UserList = UserList;
            var recordsselected = SelectsEmail(email_ID);
            var model = new Email()
            {
                email_ID = recordsselected.email_ID,
                email_Name = recordsselected.email_Name,
                email_subject = recordsselected.email_subject,
                email_AddressTo = recordsselected.email_AddressTo,
                email_Text = recordsselected.email_Text,
                isSend = recordsselected.isSend

            };

            return View(model);

        }

        [HttpPost]
        public ActionResult EditDetails(Email model)
        {
            if (model != null)
            {
                int recordsupdated =
                    UpdateEmail(model.email_ID, model.email_Name, model.email_subject,
                 model.email_AddressFrom, model.email_AddressTo, model.email_Text, model.isSend);

                return RedirectToAction("ViewEmail");
            }
            return View(model);
        }

        public ActionResult Filter(string filter)
        {
            TempData["filter"] = filter;

            return RedirectToAction("ViewEmail");
        }
    }
}