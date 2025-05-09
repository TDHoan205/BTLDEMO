using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace prjBTLDemo.NET.ViewModels
{
    public class SanPhamVM : INotifyPropertyChanged
    {
        private List<Product> _allProducts; // Original list of products
        private List<Product> _filteredProducts;
        private string _searchTerm;

        public SanPhamVM()
        {
            _allProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Sản phẩm 1", Price = 1000 },
                new Product { Id = 2, Name = "Sản phẩm 2", Price = 2000 },
                new Product { Id = 3, Name = "Sản phẩm 3", Price = 3000 }
            };
            _filteredProducts = new List<Product>(_allProducts);
        }

        public List<Product> Products
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                FilterProducts();
                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        public void AddProduct()
        {
            var newProduct = new Product
            {
                Id = _allProducts.Count > 0 ? _allProducts.Max(p => p.Id) + 1 : 1,
                Name = "Sản phẩm mới",
                Price = 0
            };
            _allProducts.Add(newProduct);
            FilterProducts();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _allProducts.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
            }
            FilterProducts();
        }

        public void DeleteProduct(int productId)
        {
            var product = _allProducts.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _allProducts.Remove(product);
            }
            FilterProducts();
        }

        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm))
            {
                // Reset to the original list if the search term is empty
                Products = new List<Product>(_allProducts);
            }
            else
            {
                // Filter products by Id or Name
                Products = _allProducts
                    .Where(p =>
                        p.Name.ToLower().Contains(_searchTerm.ToLower()) ||
                        p.Id.ToString().Contains(_searchTerm))
                    .ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}