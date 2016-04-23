using System.Collections.Generic;

using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;
using BusinesContract.UnitOfWorkInterface;
using System;
using BusinesContract.ServicesInterfaces;
using Business.Services;


namespace Bl.Services
{
    public class GenreService : IGenreService
    {
        protected IUnitOfWork UnitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IServiceResult GetAllGenres()
        {
            try
            {
                var genres = Mapper.Map<IList<DalModels.Genre>, IList<BlModels.Genre>>(UnitOfWork.GenreRepository.GetAllGenres());
                return new Result { Success = true, Data = genres };
            }
            catch(Exception ex)
            {
                return new Result { Success = false, Errors = new[] { "There is problems in genres service" } };
            }
        }

        public int? GetIdByGenreName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
