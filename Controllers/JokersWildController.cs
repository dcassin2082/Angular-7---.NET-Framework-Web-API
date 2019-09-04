using System.Web.Http;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api/jokerswild")]
    public class JokersWildController : ApiController
    {
        private IJokersWildService _gameService;

        public JokersWildController(IJokersWildService gameService)
        {
            _gameService = gameService;
        }

        [Route("paytable/{username}")]
        [HttpGet]
        public GameModel GetPaytable([FromUri] string username)
        {
            return _gameService.GetPaytable(username);
        }

        [Route("deal")]
        [HttpGet]
        public GameModel Deal(int bet, string username)
        {
            return _gameService.Deal(bet, username);
        }

        [Route("draw/{arr}")]
        [HttpGet]
        public GameModel Draw([FromUri]int[] arr, int bet, string username)
        {
            return _gameService.Draw(arr, bet, username);
        }
    }
}
