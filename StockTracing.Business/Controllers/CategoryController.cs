using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockTracing.Business.ServicesClasses;
using StockTracing.DataAccess.DataBaseClasses;
using StockTracking.DataAccess.ApiClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracing.Business.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private StockDbContext _dbContext;
        private readonly AppSettings _appSettings;
        public CategoryController(StockDbContext dbContext,AppSettings appSettings)
        {
            _dbContext = dbContext;
            _appSettings = appSettings;            
        }

        public CategoryController(StockDbContext dbContext, IOptions<AppSettings> appSettings)
        {
            this._dbContext = dbContext;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("categories")]
        public async Task<ApiResponse> GetCategories()
        {
            try
            {
                var categories = await _dbContext.categories.Where(r => !r.deleted && r.type == 0).OrderBy(r => r.name).ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(categories);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
                
            }
        }
    }
}
