using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class Event
        {
            public static string GetAllEventItems(
                string baseUri,int page,int take,
                int? category,int? location)
            {
                var filterQs = string.Empty;
                if (category.HasValue || location.HasValue)
                {
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : "null";
                    var locationQs = (location.HasValue) ? location.Value.ToString() : "null";
                    filterQs = $"/type/{categoryQs}/brand/{locationQs}";
                }
                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetAllCategories(string baseUri)
            {
                return $"{baseUri}eventcategories";
            }

            public static string GetAllLocations(string baseUri)
            {
                return $"{baseUri}eventlocations";
            }

        }
    }
}
