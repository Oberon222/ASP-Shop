﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop2.Data;
using Shop2.Models;
using Shop2.Models.ViewModels;
using Shop2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop2.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty] //
        public ProductUserVM ProductUserVM { get; set; } 
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>(); 
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(ENV.SessionCart);
            }

            //всі товари з корзини знаходяться в сесії
            List<int> productListInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productList = _db.Products.Where(i => productListInCart.Contains(i.Id));


            return View(productList);
        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(ENV.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));

            // обновлюємо сесію
            HttpContext.Session.Set(ENV.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Order()
        {
            // отримуємо юзера, який є зараз в системі
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(ENV.SessionCart);
            }

            //список товарів в корзині
            List<int> productListInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productList = _db.Products.Where(i => productListInCart.Contains(i.Id));

            ProductUserVM = new ProductUserVM()
            {
                AppUser = _db.AppUsers.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = productList.ToList()
            };

            return View(ProductUserVM);
        }
    }
}