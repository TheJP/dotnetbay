using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.FileStorage;
using DotNetBay.Interfaces;
using DotNetBay.Core.Execution;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IMainRepository MainRepository { private set; get; }
        public IAuctionRunner AuctionRunner { private set; get; }

        public App()
        {
            this.MainRepository = new FileSystemMainRepository("store.json");
            this.AuctionRunner = new AuctionRunner(this.MainRepository);
            this.AuctionRunner.Start();
            //Test data
            var memberService = new MemberService(this.MainRepository);
            var service = new AuctionService(this.MainRepository);
            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember();
                service.Save(new Auction
                {
                    Title = "My First Auction",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Seller = me
                });
            }
        }
    }
}
