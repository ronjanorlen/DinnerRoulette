using System;

namespace CookBook
{
    public class Recipe
    {
        public string? Name { get; set; }
        public string[] Ingredients { get; set; } = [];
        public string[] Instructions { get; set; } = [];
        public string[] Category { get; set; } = [];

    }

}