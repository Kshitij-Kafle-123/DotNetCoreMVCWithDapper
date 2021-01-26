using Microsoft.AspNetCore.Mvc;
using MVCCore.Models;
using MVCCore.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore.Controllers
{
    public class ManageProductsController : Controller
    {
        private readonly IProducts _products;
        public ManageProductsController(IProducts products)
        {
            _products = products;
        }
        public IActionResult Index()
        {
            var model = _products.GetProducts();
            return View(model);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Bind attribute – To protect from overposting attacks, we have use bind attribute.
        public IActionResult Create([Bind("Name,Quantity,Color,Price,ProductCode")] ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                _products.InsertProduct(productVm);
                return RedirectToAction("Index");

            }
            return View(productVm);
        }

        //--------------------------------
        /*  // GET
          public IActionResult Details()
          {
              return View(_products.GetProducts());
          }*/

        // GET with id
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _products.GetProductByProductId(Convert.ToInt32(id));
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
        //Delete--------------------------------
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _products.GetProductByProductId(Convert.ToInt32(id));

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //DeleteConfirmation---------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _products.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _products.GetProductByProductId(Convert.ToInt32(id));
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,Name,Quantity,Color,Price,ProductCode")] Product product)
        {
            if (id != product.ProductId)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_products.ProductExist(id))
                    {
                        _products.UpdateProduct(product);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Product Does Not Exists");
                        return View(product);
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
