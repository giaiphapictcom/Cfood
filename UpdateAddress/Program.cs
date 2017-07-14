using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using V308CMS.Common;
using V308CMS.Respository;

namespace UpdateAddress
{
    class RegionItem
    {
        public int id { get; set; }
        public string name { get; set; }
    }

   
    class Program
    {
        static void Main(string[] args)
        {            

            var regionService = new RegionRespository();
            var listRegion = regionService.GetListRegionByParentId();         
            if (listRegion.Any())
            {
                foreach (var region in listRegion)
                {
                    var requestHelper = new RequestHelper("https://tiki.vn/customer/address/ajaxDistrict");
                    requestHelper.Parameters.Add("region_id",region.Code);
                    requestHelper.RequestType = RequestType.Post;
                    var result = requestHelper.Send();
                    if (!string.IsNullOrEmpty(result))
                    {
                        var listCityTiki = JsonConvert.DeserializeObject<List<RegionItem>>(result);
                        if (listCityTiki.Count > 0)
                        {
                            foreach (var city in listCityTiki)
                            {                              
                                var cityIdResult = regionService.Insert(city.name, city.id.ToString(), region.Id);
                                if (cityIdResult != "exists")
                                {
                                    Console.WriteLine("Add city {0} successful.", city.name);
                                    var wardRequestHelper = new RequestHelper("https://tiki.vn/customer/address/ajaxWard");
                                    wardRequestHelper.Parameters.Add("city_id", city.id.ToString());
                                    wardRequestHelper.RequestType = RequestType.Post;
                                    var wardResult = wardRequestHelper.Send();
                                    if (!string.IsNullOrEmpty(wardResult))
                                    {
                                        var listWardTiki = JsonConvert.DeserializeObject<List<RegionItem>>(wardResult);
                                        if (listWardTiki.Count > 0)
                                        {
                                            foreach (var wardItem in listWardTiki)
                                            {
                                                regionService.Insert(wardItem.name, wardItem.id.ToString(), int.Parse(cityIdResult) );
                                                Console.WriteLine("Add ward {0} successful.", city.name);
                                            }
                                        }
                                    }
                                }
                              
                            }
                        }

                    }

                }
            }
            Console.WriteLine("Finish !");
            Console.ReadKey();
        }


    }
}