using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendshipAPI.Controllers
{
    [ApiController]
    [Route("api/Test")]
    public class TestController : ControllerBase
    {
        public static List<string> Test = new List<string>()
        {
            "Test",
            
        };

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllFriends()
        {
            return Ok(Test);
        }
    }
}