using WebApi.Models;

namespace WebApi.Services
{
    public interface IDoubleBonusService 
    {
        GameModel GetPaytable(string username);
        GameModel Deal(int bet, string username);
        GameModel Draw(int[] arr, int bet, string username);
    }
}