using System.Collections.Generic;

using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;


namespace Bl.Services
{
    public class GenreService : IGenreService
    {
        protected IGenreRepository GenreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            GenreRepository = genreRepository;
        }

        public IList<BlContracts.Models.Genre> GetAllGenres()
        {
            return Mapper.Map<IList<DalModels.Genre>, IList<BlModels.Genre>>(GenreRepository.GetAllGenres());
        }
    }
}
