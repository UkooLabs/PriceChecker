using Newtonsoft.Json;
using PriceChecker.Models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PriceChecker.Services
{
    public static class ProductService
    {
        private static Product[] _products;
   
        public static async Task<Product> GetProductsByIdAsync(string id)
        {
            if (_products == null)
            {
                var sampleDataJson = ResourceLoader.GetEmbeddedResourceString("sample-data.json");
                _products = JsonConvert.DeserializeObject<Product[]>(sampleDataJson);
            }

            Product result = null;
            foreach (var product in _products)
            {
                if (product.Id != id)
                {
                    continue;
                }
                result = product;
                break;
            }
            return await Task.FromResult(result);
        }
    }
}
