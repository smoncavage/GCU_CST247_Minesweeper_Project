using Recipe_Shop.Models;
using Recipe_Shop.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe_Shop.Services.Business {
    public class SecurityService {
        public bool Authenticate(User user) {
            SecurityDAO service = new SecurityDAO();

            return service.FindByUser(user);
        }
    }
}