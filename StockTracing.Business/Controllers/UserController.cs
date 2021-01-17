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
    public class UserController : ControllerBase
    {
        private StockDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public UserController(
            StockDbContext dbContext,
            IOptions<AppSettings> appSettings
            )
        {
            this._dbContext = dbContext;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("users")]
        public async Task<ApiResponse> GetUsers()
        {
            try
            {
                var users = await _dbContext.users
                    .Where(r => !r.deleted)
                    .OrderBy(r => r.displayName).ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(users);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("userList")]
        public async Task<ApiResponse> GetProductList(Pagination request = null)
        {
            try
            {
                request = request ?? new Pagination
                {
                    page = request.page,
                    rowsPerPage = request.rowsPerPage
                };

                var userList = _dbContext.users.Where(r => !r.deleted);

                var count = await userList.CountAsync();

                var pagination = new PaginationResponse
                {
                    page = request.page,
                    pageCount = (int)Math.Ceiling(count * 1.0 / request.rowsPerPage),
                    rowCount = count,
                    rowsPerPage = request.rowsPerPage
                };
                var response = await userList
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
        public async Task<ApiResponse> SaveUser(ApiUserRequest request)
        {
            try
            {
                var user = await _dbContext.users.SingleOrDefaultAsync(r => !r.deleted && r.Id == request.Id);

                if (user == null)
                {
                    user = new User
                    {
                        birtOfDate = (DateTime)request.BirtOfDate,
                        //createdBy = authService.UserClaims.objectSID,
                        createdDate = DateTime.Now,
                        deleted = request.Deleted,
                        department = request.Department,
                        companyId = request.CompanyId,
                        displayName = request.DisplayName,
                        accountName = request.AccountName,
                        eMail = request.EMail,
                        //editedBy = authService.UserClaims.objectSID,
                        firstName = request.FirstName,
                        genus = (bool)request.Genus,
                        phone = request.Phone,
                        lastName = request.LastName,
                        lastUpdate = DateTime.Now,
                        mobile = request.Mobile,
                        Id = Guid.NewGuid(),
                        password = request.Password,
                        thumbnailPhoto = request.ThumbnailPhoto,
                        authorityLevel = request.AuthorityLevel
                    };
                }
                else
                {
                    user.birtOfDate = (DateTime)request.BirtOfDate;
                    user.deleted = request.Deleted;
                    user.department = request.Department;
                    user.companyId = request.CompanyId;
                    user.displayName = request.DisplayName;
                    user.accountName = request.AccountName;
                    user.eMail = request.EMail;
                    //user.editedBy = authService.UserClaims.objectSID;
                    user.genus = (bool)request.Genus;
                    user.lastUpdate = DateTime.Now;
                    user.firstName = request.FirstName;
                    user.lastName = request.LastName;
                    user.phone = request.Phone;
                    user.mobile = request.Mobile;
                    user.password = request.Password;
                    user.authorityLevel = request.AuthorityLevel;
                    user.thumbnailPhoto = request.ThumbnailPhoto;
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
