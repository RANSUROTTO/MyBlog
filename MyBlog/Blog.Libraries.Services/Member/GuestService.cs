using System;
using System.Linq;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Member
{

    public partial class GuestService : IGuestService
    {

        #region Fields

        private readonly IRepository<Guest> _guestRepository;

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
