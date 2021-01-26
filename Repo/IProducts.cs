using MVCCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore.Repo
{
    public interface IProducts
    {
        void InsertProduct(ProductViewModel product);
        IEnumerable<Product> GetProducts();
        Product GetProductByProductId(int productId);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        bool ProductExist(int id);
    }
}
