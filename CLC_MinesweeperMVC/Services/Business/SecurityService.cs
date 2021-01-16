using CLC_MinesweeperMVC.Models;
using CLC_MinesweeperMVC.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC_MinesweeperMVC.Services.Business {
    public class SecurityService {
        public bool Authenticate(User user) {
            SecurityDAO service = new SecurityDAO();

            return service.FindByUser(user);
        }
    }
}