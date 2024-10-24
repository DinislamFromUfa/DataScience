﻿namespace DataScience.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Author Author { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public Guid AuthorId { get; set; }
    }
}
