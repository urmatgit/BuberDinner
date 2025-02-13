using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.UnitTests.Menus.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class Menu
        {
            public const string Name = "Menu Name";
            public const string Description = "Menu Descriptions";
            public const string SectionName = "Menu section name";
            public const string SectionDescription = "Menu section description";
            public const string ItemName = "Menu item name";
            public const string ItemDescription = "Menu item description";
            public static string SectionNameFromIndex(int index) 
                => $"Section name {index}";
            public static string SectionDescriptionFromIndex(int index)
             => $"Section Descriptions {index}";
            public static string ItemNameFromIndex(int index)
             => $"Item name {index}";
            public static string ItemDescriptionFromIndex(int index)
             => $"Item Descriptions {index}";
        }

    }
}
