using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLC_MinesweeperMVC.Models;

namespace CLC_MinesweeperMVC.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View("Game");
        }

        [HttpPost]
        public ActionResult NewGame(GameModel gm) {

            return View("Game", gm);
        }
    }
}