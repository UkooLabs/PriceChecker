using Newtonsoft.Json;
using PriceChecker.Models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PriceChecker.Services
{
    public class CartService
    {
        public ObservableCollection<Product> Products { get; set; }

        public CartService()
        {
            Products = new ObservableCollection<Product>();

            var sampleDataJson = ResourceLoader.GetEmbeddedResourceString("sample-data.json");
            Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(sampleDataJson);
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {         
            return await Task.FromResult(Products);
        }

        public async Task<double> GetCartTotalAsync()
        {
            double total = 0;
            foreach (var product in Products)
            {
                var priceToConvert = product.Price?.TrimStart('$');           
                if (!double.TryParse(priceToConvert, out var price))
                {
                    // Ignore ivalid prices, should log or validate up stream
                    continue;
                }
                total += price;
            }
            return await Task.FromResult(total);
        }
    }
}
