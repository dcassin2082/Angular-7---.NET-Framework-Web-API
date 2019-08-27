using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICompanyService : IDisposable
    {
        IQueryable<Company> GetCompanies();
        IQueryable<Company> GetCompanies(Expression<Func<Company, bool>> predicate);
        Task<Company> GetCompany(int id);
        Task<Company> GetCompany(Expression<Func<Company, bool>> predicate);
        Task<Company> PostCompany(Company company);
        Task<Company> PutCompany(Company company);
        Task<Company> DeleteCompany(Company company);
    }
}