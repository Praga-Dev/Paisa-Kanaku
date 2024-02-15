using Microsoft.AspNetCore.Mvc;

// TODO
//feature for travel, groceries UI LOGIC
//date change on product entry
//DINNER
//SALEM HOME NEEDS
// outing
// Money Transfer / Transactions

namespace Praga.Paisakanaku.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        private Guid _LoggedInUserInfoId { get; set; }

        public BaseController() { }

        public Guid LoggedInUserId
        {
            get
            {

                return Guid.Parse("F6510A9A-2E3D-4341-9E94-090ACC25D2A5");
            }
            set
            {
                _LoggedInUserInfoId = value;
            }
        }
    }
}
