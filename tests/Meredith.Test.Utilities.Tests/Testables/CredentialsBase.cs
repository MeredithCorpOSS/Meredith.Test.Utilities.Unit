using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public abstract class CredentialsBase : ICredentials
    {
        public abstract NetworkCredential GetCredential(Uri uri, string authType);
    }
}