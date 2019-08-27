using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IContactService : IDisposable
    {
        IQueryable<Contact> GetContacts();
        IQueryable<Contact> GetContacts(Expression<Func<Contact, bool>> predicate);
        Task<Contact> GetContact(int id);
        Task<Contact> GetContact(Expression<Func<Contact, bool>> predicate);
        Task<Contact> PostContact(Contact contact);
        Task<Contact> PutContact(Contact contact);
        Task<Contact> DeleteContact(Contact contact);
    }
}