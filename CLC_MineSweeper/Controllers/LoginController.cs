﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLC_MineSweeper.Controllers {
    public class LoginController:Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
