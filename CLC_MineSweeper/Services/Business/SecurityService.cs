using CLC_MineSweeper.Models;
using CLC_MineSweeper.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLC_MineSweeper.Services.Business
{
    public class SecurityService
    {
        public bool Authenticate(UserModel user)
        {
            SecurityDAO service = new SecurityDAO();

            return service.FindByUser(user);
        }
    }
}
