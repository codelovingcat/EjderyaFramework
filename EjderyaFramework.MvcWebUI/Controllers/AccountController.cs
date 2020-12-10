using EjderyaFramework.Business.Abstract;
using EjderyaFramework.Core.CrossCuttingConcerns.Security.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjderyaFramework.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Account
        /// <summary>
        ///  public string Login()
        ///{
        ///    AuthenticationHelper.CreateAuthCookie(
        ///    new Guid(), "uremsancaktutan",
        ///    "uremsancaktutan@gmail.com",
        ///   DateTime.Now.AddDays(15),
        ///  new[] { "Admin" },
        /// false,
        /// "Ürem",
        /// "Sancaktutan");
        ///  return "User is authenticated!";
        /// }
        /// </summary>
        /// <returns></returns>

        public string Login(string userName, string password)
        {
            var user = _userService.GetByUserNameAndPassword(userName, password);
            if (user != null)
            {
                AuthenticationHelper.CreateAuthCookie(
                new Guid(), user.UserName,
                user.Email,
                DateTime.Now.AddDays(15),
                _userService.GetUserRoles(user).Select(u => u.RoleName).ToArray(),
                false,
                user.FirstName,
                user.LastName);
                return "User is authenticated!";
            }
            return "User is NOT authenticated!";
        }
    }
}