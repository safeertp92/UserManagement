using AutoMapper;
using Liwapoi.DB.Models;

namespace Liwapoi.DB.Repositories
{

    public abstract class BaseRepository
    {
        #region protected fields
        protected readonly Liwapoi_WarnAppContext _context;
        protected readonly IMapper _mapper;
        #endregion

        #region Ctor
        protected BaseRepository(Liwapoi_WarnAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion
    }
}
