﻿namespace DataScience.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<Post> Posts { get; set; }
    }
}
