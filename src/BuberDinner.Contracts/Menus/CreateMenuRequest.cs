﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Contracts.Menus
{
    public record CreateMenuRequest
    (
        string Name,
        string Description,
        double AverageRating,
        List<MenuSection> Sections
    );
    public record MenuSection(string Name,string Description, List<MenuItem> Items);
    public record MenuItem(string Name,string Description);
}
