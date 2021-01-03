using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockTracking.Business.ApiRequestClasses;
using StockTracking.Business.ServicesClasses;
using StockTracking.DataAccess.ApiClasses;
using StockTracking.DataAccess.DatabaseClasses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracking.Business.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private StockDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public CategoryController(
            StockDbContext dbContext,
            IOptions<AppSettings> appSettings
            )
        {
            this._dbContext = dbContext;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("categories")]
        public async Task<ApiResponse> GetCategories()
        {
            try
            {
                var categories = await _dbContext.categories
                    .Where(r => !r.deleted && r.type == 0)
                    .OrderBy(r => r.name)
                    .ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(categories);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("categoryList")]
        public async Task<ApiResponse> GetCategoryList(Pagination request =null)
        {
            try
            {
                request = request ?? new Pagination
                {
                    page = request.page,
                    rowsPerPage = request.rowsPerPage
                };

                var categoryList = _dbContext.categories
                    .Join(_dbContext.categories, c => c.Id, sc => sc.parentId, (c, sc) => new { c, sc })
                    .Join(_dbContext.users, nc => nc.sc.editedBy, u => u.Id, (nc, u) => new { nc, u})
                    .Where(r=> !r.u.deleted && !r.nc.sc.deleted && !r.nc.c.deleted)
                    .OrderBy(r=>r.nc.c.name)
                    .Select(r=>new { 
                        categoryId=r.nc.c.Id,
                        subCategoryId = r.nc.sc.Id,
                        categoryName=r.nc.c.name,
                        subCategoryName = r.nc.sc.name,
                        lastUpdate=r.nc.sc.lastUpdate,
                        editedBy=r.u.displayName
                    });

                var count = await categoryList.CountAsync();

                var pagination = new PaginationResponse
                {
                    page = request.page,
                    pageCount = (int)Math.Ceiling(count * 1.0 / request.rowsPerPage),
                    rowCount = count,
                    rowsPerPage = request.rowsPerPage
                };
                var response = await categoryList
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

        [HttpGet("subCategories")]
        public async Task<ApiResponse> GetSubCategories()
        {
            try
            {
                var subCategories = await _dbContext.categories
                    .Where(r => !r.deleted && r.type == 1)
                    .OrderBy(r => r.name)
                    .ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(subCategories);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpGet("subCategoryList")]
        public async Task<ApiResponse> GetSubCategoryList(Pagination request = null)
        {
            try
            {
                request = request ?? new Pagination
                {
                    page = request.page,
                    rowsPerPage = request.rowsPerPage
                };

                var subCategoryList = await _dbContext.categories
                    .Join(_dbContext.categories, c => c.Id, sc => sc.parentId, (c, sc) => new { c, sc })
                    .Join(_dbContext.users, nc => nc.sc.editedBy, u => u.Id, (nc, u) => new { nc, u })
                    .Where(r => !r.u.deleted && !r.nc.sc.deleted && !r.nc.c.deleted)
                    .OrderBy(r => r.nc.c.name)
                    .Select(r => new {
                        categoryId = r.nc.c.Id,
                        subCategoryId = r.nc.sc.Id,
                        categoryName = r.nc.c.name,
                        subCategoryName = r.nc.sc.name,
                        lastUpdate = r.nc.sc.lastUpdate,
                        editedBy = r.u.displayName
                    }).ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(subCategoryList);

            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("saveCategory")]
        public async Task<ApiResponse> SaveCategory(ApiCategoryRequest request)
        {
            try
            {
                var category = await _dbContext.categories.SingleOrDefaultAsync(r => !r.deleted && r.Id == request.Id);

                if (category==null)
                {
                    category = new Category
                    {
                        //createdBy=authService.UserClaims.id,
                        deleted = request.Deleted,
                        //editedBy=authService.UserClaims.id,
                        Id = Guid.NewGuid(),
                        lastUpdate = DateTime.Now,
                        name=request.Name,
                        parentId=request.Type==0 ? Guid.Empty : request.ParentId,
                        type=request.Type
                    };
                    _dbContext.Entry(category).State = EntityState.Added;
                }
                else
                {
                    category.deleted = request.Deleted;
                    category.name = request.Name;
                    //category.editedBy = authService.UserClaims.id;
                    category.parentId = request.ParentId;
                    category.type = request.Type;
                    category.lastUpdate = DateTime.Now;

                    _dbContext.Entry(category).State = EntityState.Modified;

                    if (request.Type==0 && request.Deleted == true)
                    {
                        var subCatergory = await _dbContext.categories.Where(r => !r.deleted && r.type == 1 && r.parentId == request.Id).ToArrayAsync();

                        foreach (var item in subCatergory)
                        {
                            item.deleted = true;
                            //item.editedBy=authService.UserClaims.id;
                            item.lastUpdate = DateTime.Now;

                            _dbContext.Entry(item).State = EntityState.Modified;
                        }
                    }
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
