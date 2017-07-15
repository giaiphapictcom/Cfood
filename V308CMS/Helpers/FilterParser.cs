using System;
using System.Collections.Generic;
using System.Linq;

namespace V308CMS.Helpers
{
    public class FilterParser
    {
        public static string[] ParseTokenizer(string filter, string splitChar ="|", string splitFilter="_")
        {
           
            if (string.IsNullOrWhiteSpace(filter))
            {
                return null;
            }           
            var listFilterCategory = filter.Split(new[] {splitChar}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (listFilterCategory.Length == 0)
            {
                return null;
            }
            List<string> result = new List<string>();
            foreach (var categoryItem in listFilterCategory)
            {
                var filterIem = categoryItem.Split(new[] {splitFilter}, StringSplitOptions.RemoveEmptyEntries).ToArray();
                if (filterIem.Length > 0)
                {
                    result.AddRange(filterIem);
                }
            }
            return result.Distinct().ToArray();
        }
    }
}