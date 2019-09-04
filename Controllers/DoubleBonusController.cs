using System.Web.Http;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api/doublebonus")]
    public class DoubleBonusController : ApiController
    {
        private IDoubleBonusService _doubleBonusService;

        public DoubleBonusController(IDoubleBonusService gameService)
        {
            _doubleBonusService = gameService;
        }

        [Route("paytable/{username}")]
        [HttpGet]
        public GameModel GetPaytable([FromUri] string username)
        {
            return _doubleBonusService.GetPaytable(username);
        }

        [Route("deal")]
        [HttpGet]
        public GameModel Deal(int bet, string username)
        {
            return _doubleBonusService.Deal(bet, username);
        }

        [Route("draw/{arr}")]
        [HttpGet]
        public GameModel Draw([FromUri]int[] arr, int bet, string username)
        {
            return _doubleBonusService.Draw(arr, bet, username);
        }
    }
}
