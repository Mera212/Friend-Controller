using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendshipAPI.Controllers
{
    [ApiController]
    [Route("api/friends")]
    public class FriendsController : ControllerBase
    {
        public static List<string> Friends = new List<string>()
        {
            "Josi,Mario",
            "Paavo,Juha"
        };

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllFriends()
        {
            return Ok(Friends);
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<string>> GetFriendsByName(string name)
        {
            var relatedFriends = Friends.Where(f => f.Contains(name)).ToList();
            if (relatedFriends.Any())
            {
                return Ok(relatedFriends);
            }
            else
            {
                return NotFound($"No friends found for name '{name}'.");
            }
        }

        [HttpPost("{name}")]
        public ActionResult<string> AddFriendByName(string name, [FromBody] string newFriend)
        {
            var friendPair = $"{name},{newFriend}";
            Friends.Add(friendPair);
            return CreatedAtAction(nameof(GetAllFriends), new { }, friendPair);
        }

        [HttpPost]
        public ActionResult<string> AddFriendToAll([FromBody] string newFriendPair)
        {
            Friends.Add(newFriendPair);
            return CreatedAtAction(nameof(GetAllFriends), new { }, newFriendPair);
        }

        [HttpDelete("{name}")]
        public ActionResult RemoveFriendsByName(string name)
        {
            Friends.RemoveAll(f => f.Contains(name));
            return NoContent();
        }

        [HttpDelete("{name1},{name2}")]
        public ActionResult RemoveFriendship(string name1, string name2)
        {
            var friendPair = $"{name1},{name2}";
            if (Friends.Contains(friendPair))
            {
                Friends.Remove(friendPair);
                return NoContent();
            }
            else
            {
                return NotFound($"Friendship between '{name1}' and '{name2}' not found.");
            }
        }
    }
}
