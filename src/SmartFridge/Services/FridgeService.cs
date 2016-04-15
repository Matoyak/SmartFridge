using SmartFridge.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Services
{
    public class FridgeService
    {
        private FridgeRepository _fridgeRepo;

        public FridgeService(FridgeRepository fridgeRepo)
        {
            _fridgeRepo = fridgeRepo;
        }
    }
}
