﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Kraken;
using Domain.Common.Ioc;
using FluentScheduler;
using Unity;


namespace Application.Server.Jobs
{
    public class BookingJob : IJob
    {
        private IUnityContainer resolver;
        private IKrakenRepertory repertory;

        public BookingJob()
        {
            resolver = IocFactory.Default;
            repertory = resolver.Resolve<IKrakenRepertory>();
        }
        public void Execute()
        {
            repertory.ReloadBooking();
        }
    }
}