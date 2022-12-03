using Recipe_Shop.Services.Data;

namespace Recipe_Shop.Services.Business
{
    public class SecurityService
    {
        public bool Authenticate(User user)
        {
            SecurityDAO service = new SecurityDAO();

            return service.FindByUser(user);
        }
    }
}