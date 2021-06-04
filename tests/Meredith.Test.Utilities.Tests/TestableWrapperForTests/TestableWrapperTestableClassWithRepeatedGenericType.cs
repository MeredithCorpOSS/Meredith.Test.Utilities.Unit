using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForTests
{
    public class TestableWrapperForTestableClassWithRepeatedGenericType : TestableWrapperFor<TestableClassWithRepeatedGenericType>
    {
        [Test]
        public void It_Should_AutoMock_NonSetup_Parameters()
        {
            // Arrange
            var ex = new TestException();

            // Act
            Target.DoWorkWithDependencies(ex);

            // Assert
            The<ICrossCuttingConcern<ICredentials>>()
                .Verify(concern => concern.SomeMethod(It.IsAny<string>(), It.IsAny<object>()));
            The<ICrossCuttingConcern<ICrossCuttingConcern>>()
                .Verify(concern => concern.SomeMethod(It.IsAny<string>(), It.IsAny<object>()));
        }

        [Test]
        public void It_Should_Not_Reuse_Mocks()
        {
            // Arrange
            var ex = new TestException();

            // Act
            Target.DoWorkWithDependencies(ex);
            ClearMocks();
            Target.DoWorkWithDependencies(ex);

            // Assert
            // Times.Once is very important to test that it isn't hanging on to previously mocked objects during a test run
            The<ICrossCuttingConcern<ICredentials>>()
                .Verify(concern => concern.SomeMethod(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            The<ICrossCuttingConcern<ICrossCuttingConcern>>()
                .Verify(concern => concern.SomeMethod(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}