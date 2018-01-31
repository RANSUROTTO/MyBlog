using System;
using Blog.Libraries.Core.Domain.Members.Enum;

namespace Blog.Libraries.Core.Domain.Members
{

    public interface IAuthenticationUser
    {

        Guid Guid { get; set; }

        AuthenticationType AuthenticationType { get; }

    }

}
