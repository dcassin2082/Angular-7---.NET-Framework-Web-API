
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
    [RoutePrefix("api/deuceswild")]
    public class DeucesWildController : ApiController
    {
        private IDeucesWildService _deucesWildService;

        public DeucesWildController(IDeucesWildService gameService)
        {
            _deucesWildService = gameService;
        }

        [Route("paytable/{username}")]
        [HttpGet]
        public GameModel GetPaytable([FromUri] string username)
        {
            return _deucesWildService.GetPaytable(username);
        }

        [Route("deal")]
        [HttpGet]
        public GameModel Deal(int bet, string username)
        {
            return _deucesWildService.Deal(bet, username);
        }

        [Route("draw/{arr}")]
        [HttpGet]
        public GameModel Draw([FromUri]int[] arr, int bet, string username)
        {
            return _deucesWildService.Draw(arr, bet, username);
        }
    }
}
