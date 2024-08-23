using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polian.App.Models;
using Polian.App.Services.IServices;

namespace Polian.App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService? _productsService;
        private readonly INotyfService? _notyfService;
        public ProductsController(IProductsService productsService, INotyfService notyfService)
        {
            _productsService = productsService;
            _notyfService = notyfService;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            List<ProductsDto?> lst = new List<ProductsDto>();
            try
            {
                ResponseDTO? response = await _productsService.GetProducts();

                if (response != null && response.IsSuccess)
                {
                    lst = JsonConvert.DeserializeObject<List<ProductsDto?>>(response.Data.ToString());
                }
                else
                {
                    _notyfService.Error(response.Message);
                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return View(lst);
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductsDto productsDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productsDto.ImageUrl = productsDto.formFile.FileName;

                    var data = await _productsService.CreateProduct(productsDto);

                    if (data.IsSuccess && _notyfService != null)
                    {

                        _notyfService.Success("Product Added Successfully");
                    }
                    else
                    {

                        _notyfService.Error(data.Message);
                        return View(productsDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _notyfService.Error(ex.Message);
                    return View();
                }
            }
            return View();
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            try
            {
                var data = await _productsService.GetProductById(id);
                if (data.IsSuccess)
                {

                    var obj = JsonConvert.DeserializeObject<ProductsDto>(data.Data.ToString());
                    return View(obj);
                }
                else
                {
                    _notyfService.Error(data.Message);
                    return View();

                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View();
            }
        }


        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductsDto productsDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _productsService.UpdateProduct(productsDto);

                    if (data.IsSuccess && _notyfService != null)
                    {

                        _notyfService.Success("Product Updated Successfully");
                    }
                    else
                    {

                        _notyfService.Error(data.Message);
                        return View(productsDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _notyfService.Error(ex.Message);
                    return View();
                }
            }
            return View();
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var data = await _productsService.DeleteProduct(id);

            return RedirectToAction("Index");
        }


    }
}
