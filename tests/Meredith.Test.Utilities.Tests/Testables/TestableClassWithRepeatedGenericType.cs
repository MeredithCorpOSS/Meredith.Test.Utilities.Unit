using System;
using System.Net;

namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public class TestableClassWithRepeatedGenericType
    {
        private readonly ICredentials _credentials;
        private readonly ICrossCuttingConcern<ICredentials> _crossCuttingConcern1;
        private readonly ICrossCuttingConcern<ICrossCuttingConcern> _crossCuttingConcern2;

        public TestableClassWithRepeatedGenericType(ICredentials credentials, ICrossCuttingConcern<ICredentials> crossCuttingConcern1, ICrossCuttingConcern<ICrossCuttingConcern> crossCuttingConcern2)
        {
            _credentials = credentials;
            _crossCuttingConcern1 = crossCuttingConcern1;
            _crossCuttingConcern2 = crossCuttingConcern2;
        }

        public void DoWorkWithDependencies(Exception ex)
        {
            _crossCuttingConcern1.SomeMethod("IGotCalled", ex);
            _crossCuttingConcern2.SomeMethod("IGotCalled", ex);
        }
    }
}