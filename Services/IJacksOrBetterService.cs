using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IJacksOrBetterService
    {
        GameModel GetPaytable(string username);
        GameModel Deal(int bet, string username);
        GameModel Draw(int[] arr, int bet, string username);
    }
}