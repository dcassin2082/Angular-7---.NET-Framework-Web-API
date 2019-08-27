using System.Web.Http;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("paytable")]
        [HttpGet]
        public GameModel GetPaytable()
        {
            return _gameService.GetPaytable();
        }

        [Route("deal/{bet}")]
        [HttpGet]
        public GameModel Deal([FromUri]int bet)
        {
            return _gameService.Deal(bet);
        }

        [Route("draw")]
        [HttpGet]
        public GameModel Draw([FromUri] int[] arr, [FromUri] int bet)
        {
            return _gameService.Draw(arr, bet);
        }
    }
}
