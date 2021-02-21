

using CLC_MinesweeperMVC.Models;
using CLC_MinesweeperMVC.Services.Business;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CLC_MinesweeperMVC.Controllers{
    public class UserController : Controller{
        private static NLog.Logger logger = LogManager.GetLogger("myAppLoggerRules");

        // GET: Register
        [HttpGet]
        public ActionResult Registration(){
            return View("Registration");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user) {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if(ModelState.IsValid) {

                #region //Email is already Exist 
                var isExist = IsEmailExist(user.EMAIL);
                if(isExist) {
                    logger.Error("User attempted to use an email which already exists.");
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion

                #region Generate Activation Code 
                user.ActivationCode=Guid.NewGuid();
                #endregion
                
                //Hashing Password for storing in DB, But this is not yet fully functional with log in. 

                //try{
                //#region  Password Hashing 
                //user.PASSWORD=Crypto.Hash(user.PASSWORD);
                //user.ConfirmPassword=Crypto.Hash(user.ConfirmPassword); //
                //}
                //catch (Exception e) {
                //   Console.WriteLine("Failed to Hash Password!");
                //    Console.WriteLine(e.Message);
                //}
                //#endregion
                user.IsEmailVerified=true;
                try {
                #region Save to Database
                using(MyDBEntities dc = new MyDBEntities()) {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    //Send Email to User
                    //SendVerificationLinkEmail(user.EMAIL, user.ActivationCode.ToString());
                    message="Registration successfully done. Account activation link "+
                        " has been sent to your email: "+user.EMAIL;
                    Status=true;
                }
                    #endregion
                }
                catch(SqlException e) {
                    Console.WriteLine(e.Message);
                }
                logger.Info("New User "+user+"has been added to the database Successfully.");
            }
            else {
                message="Invalid Request";
            }

            ViewBag.Message=message;
            ViewBag.Status=Status;
            // return View(user);
            return View("Registration");
        }
        public bool IsEmailExist(string email) {
            using(MyDBEntities dc = new MyDBEntities()) {
                var v = dc.Users.Where(a => a.EMAIL==email).FirstOrDefault();
                return v!=null;
            }
        }
        public bool IsUserExist(string username) {
            using(MyDBEntities dc = new MyDBEntities()) {
                var v = dc.Users.Where(a => a.USERNAME==username).FirstOrDefault();
                return v!=null;
            }
        }
        /*public void SendVerificationLinkEmail(string email, string activationCode) {
            var verifyUrl = "/User/VerifyAccount/"+activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "********"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is"+
                " successfully created. Please click on the below link to verify your account"+
                " <br/><br/><a href='"+link+"'>"+link+"</a> ";

            var smtp = new SmtpClient {
                Host="smtp.gmail.com",
                Port=587,
                EnableSsl=true,
                DeliveryMethod=SmtpDeliveryMethod.Network,
                UseDefaultCredentials=false,
                Credentials=new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using(var message = new MailMessage(fromEmail, toEmail) {
                Subject=subject,
                Body=body,
                IsBodyHtml=true
            })
                try{
                smtp.Send(message);
                }
                catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
        }*/
    }
}