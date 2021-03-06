﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.DataBaseModels;

namespace Domain.Exmo
{
    public interface IExmoRepertory
    {
        bool ReloadTicker();
        List<TickerTable> GetTicker();

        List<BookingTable> GetBooking(string coin);

        bool ReloadBooking();

        List<BalanceTable> GetBalance();

        bool ReloadBlance();
    }
}
