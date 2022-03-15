﻿using System;
using System.IO;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProducts();
        return View(products);
    }

    [HttpGet]
    public async Task<ActionResult> Create()
    {
        ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            await _productService.Add(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetById(id);
        ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            await _productService.Update(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetById(id);
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetById(id);

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\", product.Image);
        var exits = System.IO.File.Exists(image);
        ViewBag.ImageExist = exits;

        return View(product);
    }

}
}
