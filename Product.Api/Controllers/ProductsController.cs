using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Api.Data;
using Product.Api.Model;
using Product.Api.Model.ProductDto;

namespace Product.Services.Api.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDBContext? _db;
        private IMapper? _mapper;
        ResponseDTO? _response = new ResponseDTO();

        public ProductsController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseDTO> GetProducts()
        {
            try
            {
                var PdData = await _db.products.ToListAsync();

                _response.Data = PdData;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("{id}")]
        public async Task<ResponseDTO> GetProductsById(int id)
        {
            try
            {
                var PdData = await _db.products.FirstOrDefaultAsync(dt => dt.ProductId == id);

                _response.Data = PdData;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDTO> DeleteProductsById(int id)
        {
            try
            {
                Products products = _db.products.First(u => u.ProductId == id);


                if (!string.IsNullOrEmpty(products.ImageLocalPath))
                {
                    var olddirectory = Path.Combine(Directory.GetCurrentDirectory(), products.ImageLocalPath);

                    FileInfo fileInfo = new FileInfo(olddirectory);

                    if (fileInfo.Exists)
                    {

                        fileInfo.Delete();
                    }
                }

                var PdData = await _db.products.Where(ds => ds.ProductId == id).ExecuteDeleteAsync();

                _response.Data = PdData;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public async Task<ResponseDTO> CreateProduct(ProductsDto products)
        {
            try
            {
                Products products1 = _mapper.Map<Products>(products);

                await _db.products.AddAsync(products1);
                await _db.SaveChangesAsync();

                if (products.formFile != null)
                {
                    string filename = products1.ProductId + Path.GetExtension(products.formFile.FileName);
                    string filpath = @"wwwroot\ProductImages\" + filename;
                    var filepathdirectory = Path.Combine(Directory.GetCurrentDirectory(), filpath);
                    using (var filestream = new FileStream(filepathdirectory, FileMode.Create))
                    {
                        await products.formFile.CopyToAsync(filestream);
                    }

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    products1.ImageUrl = baseUrl + "/ProductImages/" + filename;
                    products1.ImageLocalPath = filpath;

                }
                else
                {
                    products1.ImageUrl = "https://placehold.co/600x400";
                }


                _db.products.Update(products1);
                await _db.SaveChangesAsync();

                _response.Data = _mapper.Map<ProductsDto>(products1);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        public async Task<ResponseDTO> UpdatProudct(ProductsDto products)
        {
            try
            {

                Products product = _db.products.AsNoTracking().First(u => u.ProductId == products.ProductId);

                if (!string.IsNullOrEmpty(product.ImageLocalPath))
                {
                    var olddirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                    FileInfo fileInfo = new FileInfo(olddirectory);

                    if (fileInfo.Exists)
                    {

                        fileInfo.Delete();
                    }
                }

                if (products.formFile != null)
                {
                    string filename = products.ProductId + Path.GetExtension(products.formFile.FileName);
                    string filpath = @"wwwroot\ProductImages\" + filename;
                    var filepathdirectory = Path.Combine(Directory.GetCurrentDirectory(), filpath);
                    using (var filestream = new FileStream(filepathdirectory, FileMode.Create))
                    {
                        await products.formFile.CopyToAsync(filestream);
                    }

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    products.ImageUrl = baseUrl + "/ProductImages/" + filename;
                    products.ImageLocalPath = filpath;

                }
                else
                {
                    products.ImageUrl = "https://placehold.co/600x400";
                }

                Products data = _mapper.Map<Products>(products);

                _db.products.Update(data);

                await _db.SaveChangesAsync();

                _response.Data = data;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;

        }
    }
}
