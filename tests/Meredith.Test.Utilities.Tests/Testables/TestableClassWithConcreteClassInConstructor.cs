using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public class TestableClassWithConcreteClassInConstructor
    {
        private readonly ICredentials _credentials;
        private readonly ICrossCuttingConcern _crossCuttingConcern;

        public TestableClassWithConcreteClassInConstructor(Credentials credentials, ICrossCuttingConcern crossCuttingConcern)
        {
            _credentials = credentials;
            _crossCuttingConcern = crossCuttingConcern;
        }

        public void LogMessageJustBecause(Exception ex)
        {
            var credential = _credentials.GetCredential(new Uri("http://localhost"), "Basic");
            _crossCuttingConcern.SomeMethod(string.Format("{0} did stuff and bad things happened", credential.UserName), ex);
        }
    }
}