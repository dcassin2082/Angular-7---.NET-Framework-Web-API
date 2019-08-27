using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Services
{
    public class CompanyService : ServicesBase, ICompanyService
    {
        private IRepository<Company> _companyRepo;

        public CompanyService() : base() =>
            _companyRepo = _companyRepo ?? (_companyRepo = new Repository<Company>(dbContext));

        public async Task<Company> DeleteCompany(Company company) =>
            await _companyRepo.Delete(company);

        public IQueryable<Company> GetCompanies() =>
             _companyRepo.GetAll();

        public IQueryable<Company> GetCompanies(Expression<Func<Company, bool>> predicate) =>
            _companyRepo.GetAll(predicate);

        public async Task<Company> GetCompany(int id) =>
            await _companyRepo.GetSingle(id);

        public async Task<Company> GetCompany(Expression<Func<Company, bool>> predicate) =>
            await _companyRepo.GetSingle(predicate);

        public async Task<Company> PostCompany(Company company) =>
            await _companyRepo.Post(company);

        public async Task<Company> PutCompany(Company company) =>
            await _companyRepo.Put(company);
    }
}