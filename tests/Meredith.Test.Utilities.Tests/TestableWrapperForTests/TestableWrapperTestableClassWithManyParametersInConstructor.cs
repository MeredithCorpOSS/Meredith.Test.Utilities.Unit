using System;
using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForTests
{
    public class TestableWrapperForTestableClassWithManyParametersInConstructor : TestableWrapperFor<TestableClassWithManyParametersInConstructor>
    {
        [SetUp]
        public void SetUp()
        {
            The<ICredentials>()
                .Setup(credentials => credentials.GetCredential(It.IsAny<Uri>(), It.IsAny<string>()))
                .Returns(new NetworkCredential { UserName = "TheTestUserName" });
        }

        [Test]
        public void It_Should_AutoMock_NonSetup_Parameters()
        {
            // Arrange
            var ex = new TestException();

            // Act
            var result = Target.DoWorkWithDependencies(ex);

            // Assert
            // Times.Once is very important to test that it isn't hanging on to previously mocked objects during a test run
            Assert.IsNotNull(result);
            The<ICrossCuttingConcern>()
                .Verify(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("string")), ex), Times.Once);
            The<ICredentials>()
                .Verify(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"), Times.Once);
        }

        [Test]
        public void It_Should_Not_Reuse_Mocks()
        {
            // Arrange
            var ex = new TestException();

            // Act
            Target.DoWorkWithDependencies(ex);
            ClearMocks();
            var result = Target.DoWorkWithDependencies(ex);

            // Assert
            // Times.Once is very important to test that it isn't hanging on to previously mocked objects during a test run
            Assert.IsNotNull(result);
            The<ICrossCuttingConcern>()
                .Verify(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("string")), ex), Times.Once);
            The<ICredentials>()
                .Verify(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"), Times.Once);
        }
    }
}