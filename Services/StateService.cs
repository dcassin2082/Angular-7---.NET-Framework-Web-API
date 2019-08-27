using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Services
{
    public class StateService : ServicesBase, IStateService
    {
        public IQueryable<State> GetStates()
        {
            return dbContext.States;
        }
    }
}