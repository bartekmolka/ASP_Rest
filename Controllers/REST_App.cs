using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace REST_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "John" },
            new User { Id = 2, Name = "Alice" },
            new User { Id = 3, Name = "Bob" }
        };

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _users.Remove(user);
            return NoContent();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}