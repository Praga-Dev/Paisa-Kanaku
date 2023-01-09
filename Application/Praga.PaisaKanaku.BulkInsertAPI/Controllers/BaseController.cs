using Microsoft.AspNetCore.Mvc;
using System;

namespace Praga.PaisaKanaku.BulkInsertAPI.Controllers
{
    public class BaseController : Controller
    {
        private Guid _LoggedInUserInfoId { get; set; }

        public BaseController()
        {

        }

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
