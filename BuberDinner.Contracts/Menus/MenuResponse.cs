using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Contracts.Menus
{
    public record MenuResponse (
        string Id,
        string Name,
        string Description,
        double? AverageRating,
        List<MenuSectionResponse> Sections,
        string HostId,
        List<string> DinnerIds,
        List<string> MunuReviewIds,
        DateTime CreateDateTime,
        DateTime UpdateDateTime
        );
    public record MenuSectionResponse(
        string Id,
        string Name,
        string Description,
        List<MenuItemResponse> Items
         );
    public record MenuItemResponse(
        string Id,
        string Name,
        string Description);
}
