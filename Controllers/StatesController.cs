using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api/states")]
    public class StatesController : ApiController
    {
        private IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        public IQueryable<State> GetStates()
        {
            return _stateService.GetStates();
        }
    }
}
