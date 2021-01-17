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
    public class CompanyController : ControllerBase
    {

        private StockDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public CompanyController(
            StockDbContext dbContext,
            IOptions<AppSettings> appSettings
            )
        {
            this._dbContext = dbContext;
            this._appSettings = appSettings.Value;
        }

        [HttpGet("companies")]
        public async Task<ApiResponse> GetCompanies()
        {
            try
            {
                var companies = await _dbContext.companies
                    .Where(r => !r.deleted)
                    .OrderBy(r => r.name)
                    .ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(companies);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("companyList")]
        public async Task<ApiResponse> GetCompanyList(Pagination request = null)
        {
            try
            {
                request = request ?? new Pagination
                {
                    page = request.page,
                    rowsPerPage = request.rowsPerPage
                };

                var companyList = _dbContext.companies.Where(r => !r.deleted);
                    

                var count = await companyList.CountAsync();

                var pagination = new PaginationResponse
                {
                    page = request.page,
                    pageCount = (int)Math.Ceiling(count * 1.0 / request.rowsPerPage),
                    rowCount = count,
                    rowsPerPage = request.rowsPerPage
                };
                var response = await companyList
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

        [HttpPost("companyUserList/{companyId}")]
        public async Task<ApiResponse> GetCompanyUserList(Guid companyId)
        {
            try
            {
                var companyUserList = await _dbContext.companyUsers.Join(_dbContext.users, c => c.userId, u => u.Id, (c, u) => new { c, u, }).Where(r => !r.c.deleted && !r.u.deleted && r.c.companyId == companyId).Select(r => new
                {
                    r.u.Id,
                    r.u.displayName,
                    r.u.department,
                    r.u.thumbnailPhoto,
                    r.u.mobile,
                    r.u.phone,
                    r.u.eMail,
                }).ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(companyUserList);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("UserList/{userId}")]
        public async Task<ApiResponse> GetUserList(Guid userId)
        {
            try
            {
                var companyUserList = await _dbContext.companyUsers.Join(_dbContext.companies, c => c.companyId, u => u.Id, (c, u) => new { c, u, }).Where(r => !r.c.deleted && !r.u.deleted && r.c.companyId == userId).Select(r => new
                {
                    r.u.Id,
                    r.u.isShipping,
                    r.u.name,
                    r.u.phone,
                    r.u.taxNo,
                    r.u.webSite,
                    r.u.address,
                    r.u.eMail,
                }).ToListAsync();

                return ApiResponseErrorType.OK.CreateResponse(companyUserList);
            }
            catch (Exception e)
            {
                return (e.InnerException ?? e).Message.CreateResponse();
            }
        }

        [HttpPost("saveCompany")]
        public async Task<ApiResponse> SaveCompany(ApiCompanyRequest request)
        {
            try
            {
                var company = await _dbContext.companies.SingleOrDefaultAsync(r => !r.deleted && r.Id == request.Id);

                if (company==null)
                {
                    company = new Company
                    {
                        address = request.Address,
                        //createdBy = authService.UserClaims.objectSID,
                        createdDate = DateTime.Now,
                        deleted = request.Deleted,
                        //editedBy = authService.UserClaims.objectSID,
                        eMail = request.Eposta,
                        Id = Guid.NewGuid(),
                        lastUpdate = DateTime.Now,
                        name = request.Name,
                        phone = request.Phone,
                        taxNo = request.TaxNo,
                        webSite = request.WebSite,
                        isShipping = request.IsShipping
                    };
                }
                else
                {
                    company.address = request.Address;
                    company.deleted = request.Deleted;
                    //company.editedBy = authService.UserClaims.objectSID;
                    company.eMail = request.Eposta;
                    company.lastUpdate = DateTime.Now;
                    company.name = request.Name;
                    company.phone = request.Phone;
                    company.taxNo = request.TaxNo;
                    company.webSite = request.WebSite;
                    company.isShipping = request.IsShipping;
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
