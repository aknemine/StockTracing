using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockTracking.Business.ApiRequestClasses;
using StockTracking.Business.ServicesClasses;
using StockTracking.DataAccess.ApiClasses;
using StockTracking.DataAccess.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracking.Business.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private StockDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public ProductController(
            StockDbContext dbContext,
            IOptions<AppSettings> appSettings
            )
        {
            this._dbContext = dbContext;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("products")]
        public async Task<ApiResponse> GetProducts()
        {
            try
            {
                var products= await _dbContext.products
                    .Where(r => !r.deleted)
                    .OrderBy(r => r.name)
                    .ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(products);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("productList")]
        public async Task<ApiResponse> GetProductList(Pagination request = null)
        {
            try
            {
                request = request ?? new Pagination
                {
                    page = request.page,
                    rowsPerPage = request.rowsPerPage
                };

                var productList = _dbContext.products.Where(r=> !r.deleted);

                var count = await productList.CountAsync();

                var pagination = new PaginationResponse
                {
                    page = request.page,
                    pageCount = (int)Math.Ceiling(count * 1.0 / request.rowsPerPage),
                    rowCount = count,
                    rowsPerPage = request.rowsPerPage
                };
                var response = await productList
                    .Skip(request.rowsPerPage * (request.page - 1))
                    .Take(request.rowsPerPage)
                    .ToListAsync();

                return pagination.CreateResponse(response);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("saveProduct")]
        public async Task<ApiResponse> SaveProduct(ApiProductRequest request)
        {
            try
            {
                var product = await _dbContext.products.SingleOrDefaultAsync(r => !r.deleted && r.Id == request.Id);

                if (product == null)
                {
                    product = new Product
                    {
                        createdDate = DateTime.Now,
                        deleted = request.Deleted,
                        //editedBy = authService.UserClaims.objectSID,
                        Id = Guid.NewGuid(),
                        lastUpdate = DateTime.Now,
                        name = request.Name,
                        barcodeNo = request.BarcodeNo,
                        categoryId = request.CategoryId,
                        criticalLevel = request.CriticalLevel,
                        genus = request.Genus,
                        inStock = request.InStock
                    };
                }
                else 
                {
                    product.lastUpdate = DateTime.Now;
                    product.name = request.Name;
                    product.criticalLevel = request.CriticalLevel;
                    product.categoryId = request.CategoryId;
                    product.genus = request.Genus;
                    //roduct.editedBy = authService.UserClaims.objectSID;
                    product.deleted = request.Deleted;
                    product.inStock = request.InStock;
                }
                await _dbContext.SaveChangesAsync();
                return ApiResponseErrorType.OK.CreateResponse();
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }
    }
}
