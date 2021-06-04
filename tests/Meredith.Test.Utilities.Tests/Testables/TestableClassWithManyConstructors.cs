using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public class TestableClassWithManyConstructors
    {
        private readonly ICrossCuttingConcern _crossCuttingConcern;
        private readonly ICredentials _credentials;

        public TestableClassWithManyConstructors()
        {
        }

        public TestableClassWithManyConstructors(ICrossCuttingConcern crossCuttingConcern)
        {
            _crossCuttingConcern = crossCuttingConcern;
        }

        public TestableClassWithManyConstructors(ICrossCuttingConcern crossCuttingConcern, ICredentials credentials)
        {
            _crossCuttingConcern = crossCuttingConcern;
            _credentials = credentials;
        }

        public void LogMessageJustBecause(Exception ex)
        {
            var credential = _credentials.GetCredential(new Uri("http://localhost"), "Basic");
            _crossCuttingConcern.SomeMethod(string.Format("{0} did stuff and bad happened", credential.UserName), ex);
        }
    }
}