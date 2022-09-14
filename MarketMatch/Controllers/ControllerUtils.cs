using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketMatch.Data;
using MarketMatch.Models;

namespace MarketMatch.Controllers
{
    public class ControllerUtils : Controller
    {
        public string? getUserListId(HttpContext context, HttpResponse response)
        {
            // Check for cookie and create if not found
            string userListId = context.Request.Cookies["userListId"];
            if (userListId == null)
            {
                //Set the Expiry date of the Cookie.
                CookieOptions option = new CookieOptions();
                option.MaxAge = TimeSpan.FromDays(30);

                response.Cookies.Append("userListId", Guid.NewGuid().ToString(), option);
            }
            return userListId;
        }
    }
}
