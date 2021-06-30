using PriceChecker.Models;
using PriceChecker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;

namespace PriceChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ProductList();
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            var productList = (ProductList)BindingContext;

            // not able to test this with real device at moment

            //var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            //var result = scanner.Scan().Result;
            //productList.AddProductAsync(result?.Text);

            // instead lets add a random product

            var random = new Random();
            var value = random.Next(1, 3);
            productList.AddProductAsync($"000{value}");
        }

        private void RemoveProduct_Tapped(object sender, EventArgs e)
        {
            TappedEventArgs tappedEventArgs = (TappedEventArgs)e;
            var prioductList = (ProductList)BindingContext;
            prioductList.RemoveProduct((string)tappedEventArgs.Parameter);
        }
    }
}
