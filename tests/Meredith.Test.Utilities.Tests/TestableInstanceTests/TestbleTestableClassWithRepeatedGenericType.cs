using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForInstanceTests
{
    public class TestableWrapperForTestableClassWithRepeatedGenericType
    {
        [Test]
        public void It_Should_AutoMock_NonSetup_Parameters()
        {
            // Arrange
            var ex = new TestException();

            // Act
            var myClass = new TestableInstance<TestableClassWithRepeatedGenericType>();
            myClass.Target.DoWorkWithDependencies(ex);

            // Assert
            myClass.DependsOn<ICrossCuttingConcern<ICredentials>>()
                .Verify(concern => concern.SomeMethod(It.IsAny<string>(), It.IsAny<object>()));
            myClass.DependsOn< ICrossCuttingConcern <ICrossCuttingConcern>>()
                .Verify(concern => concern.SomeMethod(It.IsAny<string>(), It.IsAny<object>()));
        }
    }
}