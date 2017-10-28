using System;

namespace Blog.Libraries.Core.Domain.Members
{

    public interface IAuthenticationUser
    {

        Guid Guid { get; set; }

        AuthenticationType AuthenticationType { get; }

    }

}
