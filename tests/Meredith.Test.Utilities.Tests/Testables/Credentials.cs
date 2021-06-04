using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public class Credentials : CredentialsBase
    {
        public override NetworkCredential GetCredential(Uri uri, string authType)
        {
            return new NetworkCredential();
        }
    }
}