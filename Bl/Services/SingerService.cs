using System.Collections.Generic;

using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;
using BusinesContract.UnitOfWorkInterface;
using Business.Services;
using System;
using BusinesContract.ServicesInterfaces;

namespace Bl.Services
{
    public class SingerService : ISingerService
    {
        protected IUnitOfWork UnitOfWork;

        public SingerService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IServiceResult GetAllSingers()
        {
            try
            {
                var singers = Mapper.Map<IList<DalModels.Singer>, IList<BlModels.Singer>>(UnitOfWork.SingerRepository.GetAllSingers());
                return new Result { Success = true, Data = singers };
            }
            catch (Exception ex)
            {
                return new Result { Success = false, Errors = new[] { "There is problems in genres service" } };
            }
        }
    }
}
