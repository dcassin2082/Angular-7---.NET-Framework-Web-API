using System;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IGameService : IDisposable
    {
        GameModel GetPaytable();
        GameModel Deal(int bet);
        GameModel Draw(int[] arr, int bet);
    }
}