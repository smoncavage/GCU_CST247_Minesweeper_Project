using CLC_MinesweeperMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC_MinesweeperMVC.Controllers{
    public class ButtonController : Controller{
        static List<ButtonModel> buttons = new List<ButtonModel>();

        // GET: Button
        public ActionResult Index() {
            buttons.Add(new ButtonModel(true));
            buttons.Add(new ButtonModel(true));
            return View("MineSweep", buttons);
        }

        [HttpPost]
        public ActionResult OnButtonClick(string mine) {
            if(mine=="1") {
                if(buttons[0].State) {
                    buttons[0].State=false;
                }
                else {
                    buttons[0].State=true;
                }
            }
            if(mine=="2") {
                if(buttons[1].State) {
                    buttons[1].State=false;
                }
                else {
                    buttons[1].State=true;
                }
            }
            return View("MineSweep", buttons);
        }
    }
}