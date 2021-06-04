using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public class TestableClassWithManyParametersInConstructor
    {
        private readonly ICrossCuttingConcern _crossCuttingConcern;
        private readonly ICredentials _credentials;
        
        public TestableClassWithManyParametersInConstructor(ICrossCuttingConcern crossCuttingConcern, ICredentials credentials)
        {
            _crossCuttingConcern = crossCuttingConcern;
            _credentials = credentials;
        }

        public bool DoWorkWithDependencies(Exception ex)
        {
            var credential = _credentials.GetCredential(new Uri("http://localhost"), "Basic");
            _crossCuttingConcern.SomeMethod("string", ex);
            return credential == null;
        }
    }
}