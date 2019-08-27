﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IStateService : IDisposable
    {
        IQueryable<State> GetStates();
    }
}