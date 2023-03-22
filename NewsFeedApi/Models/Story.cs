﻿namespace NewsFeedApi.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Time { get; set; }

        public string? Url { get; set; }
    }
}

