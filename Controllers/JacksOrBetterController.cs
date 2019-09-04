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
    [RoutePrefix("api/jacksorbetter")]
    public class JacksOrBetterController : ApiController
    {
        private IJacksOrBetterService _jacksOrBetterService;

        public JacksOrBetterController(IJacksOrBetterService gameService)
        {
            _jacksOrBetterService = gameService;
        }

        [Route("paytable/{username}")]
        [HttpGet]
        public GameModel GetPaytable([FromUri] string username)
        {
            return _jacksOrBetterService.GetPaytable(username);
        }

        [Route("deal")]
        [HttpGet]
        public GameModel Deal(int bet, string username)
        {
            return _jacksOrBetterService.Deal(bet, username);
        }

        [Route("draw/{arr}")]
        [HttpGet]
        public GameModel Draw([FromUri]int[] arr, int bet, string username)
        {
            return _jacksOrBetterService.Draw(arr, bet, username);
        }
    }
}
