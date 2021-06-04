using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public class TestableClassWithPropertyInjection
    {
        public ICrossCuttingConcern CrossCuttingConcern { get; set; }
        public ICredentials Credentials { get; set; }

        public void LogMessageJustBecause(Exception ex)
        {
            var credential = Credentials.GetCredential(new Uri("http://localhost"), "Basic");
            CrossCuttingConcern.SomeMethod(string.Format("{0} did stuff and bad happened", credential.UserName), ex);
        }
    }
}
