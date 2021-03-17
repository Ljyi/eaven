using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.Host.Authorize
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        //Add any custom requirement properties if you have them
        public PermissionRequirement()
        {

        }
    }
}
