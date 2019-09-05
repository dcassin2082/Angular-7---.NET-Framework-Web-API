using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Services
{
    public abstract class ServicesBase : IDisposable
    {
        protected readonly SmartEntities dbContext;

        public ServicesBase()
        {
            dbContext = new SmartEntities();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
