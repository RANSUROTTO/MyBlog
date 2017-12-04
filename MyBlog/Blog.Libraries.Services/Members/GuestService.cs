using System;
using System.Linq;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Members
{

    public class GuestService : IGuestService
    {

        #region Fields

        private readonly IRepository<Guest> _guestRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public GuestService(IRepository<Guest> guestRepository)
        {
            _guestRepository = guestRepository;
        }

        #endregion

        public Guest GetGuestById(long guestId)
        {
            throw new NotImplementedException();
        }

        public Guest GetGuestByGuid(Guid guid)
        {
            if (guid == Guid.Empty)
                return null;

            var query = from g in _guestRepository.Table
                        where g.Guid == guid
                        orderby g.Id
                        select g;

            return query.FirstOrDefault();
        }

    }

}
