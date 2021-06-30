using PriceChecker.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PriceChecker.Services;
using System.Threading;
using System.Linq;

namespace PriceChecker.ViewModels
{
    public class ProductList : INotifyPropertyChanged
    {
        private CartService cartService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Product> _cartPproducts;
        public ObservableCollection<Product> CartProducts
        {
            get { return _cartPproducts; }
            set { _cartPproducts = value; OnPropertyChanged(); }
        }

        public double _cartTotal;
        public double CartTotal
        {
            get { return _cartTotal; }
            set { _cartTotal = value; OnPropertyChanged(); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ProductList()
        {
            cartService = new CartService();
            LoadProducts();
        }

        private void LoadProducts()
        {
            IsBusy = true;
            Task.Run(async () =>
            {               
                Thread.Sleep(1000); // Lets simulate a slow service
                CartProducts = await cartService.GetProductsAsync();
                CartTotal = await cartService.GetCartTotalAsync();
                IsBusy = false;
            });
        }

        public void AddProductAsync(string id)
        {
            var product = ProductService.GetProductsByIdAsync(id).Result;
            if (product == null) {
                return;
            }
            cartService.Products.Add(product);
            LoadProducts();
        }

        public void RemoveProduct(string id)
        {
            var product = CartProducts.Where(p => p.Id == id).FirstOrDefault();
            if (product == null) {
                return;
            }
            CartProducts.Remove(product);
            LoadProducts();
        }
    }
}
