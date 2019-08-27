using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Services
{
    public class EmployeeService : ServicesBase, IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService() : base() =>
            _employeeRepo = _employeeRepo ?? (_employeeRepo = new Repository<Employee>(dbContext));

        public async Task<Employee> DeleteEmployee(Employee employee) =>
            await _employeeRepo.Delete(employee);

        public async Task<Employee> GetEmployee(int id) =>
            await _employeeRepo.GetSingle(id);

        public async Task<Employee> GetEmployee(Expression<Func<Employee, bool>> predicate) =>
            await _employeeRepo.GetSingle(predicate);

        public IQueryable<Employee> GetEmployees() =>
            _employeeRepo.GetAll();

        public IQueryable<Employee> GetEmployees(Expression<Func<Employee, bool>> predicate) =>
            _employeeRepo.GetAll(predicate);

        public async Task<Employee> PostEmployee(Employee employee) =>
            await _employeeRepo.Post(employee);

        public async Task<Employee> PutEmployee(Employee employee) =>
            await _employeeRepo.Put(employee);
    }
}