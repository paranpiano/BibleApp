using System;
using System.Collections.Generic;
using System.Text;

namespace BibleApp.Models
{
    public enum MenuItemType
    {
        Bible,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
