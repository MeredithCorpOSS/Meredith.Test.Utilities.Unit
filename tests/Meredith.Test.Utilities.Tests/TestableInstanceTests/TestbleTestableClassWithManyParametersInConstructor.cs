using System;
using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForInstanceTests
{
    public class TestableWrapperForTestableClassWithManyParametersInConstructor
    {
        TestableInstance<TestableClassWithManyParametersInConstructor> myClass;

        [SetUp]
        public void SetUp()
        {
            myClass = new TestableInstance<TestableClassWithManyParametersInConstructor>();
            myClass.DependsOn<ICredentials>()
                .Setup(credentials => credentials.GetCredential(It.IsAny<Uri>(), It.IsAny<string>()))
                .Returns(new NetworkCredential { UserName = "TheTestUserName" });
        }

        [Test]
        public void It_Should_AutoMock_NonSetup_Parameters()
        {
            // Arrange
            var ex = new TestException();

            // Act
            var result = myClass.Target.DoWorkWithDependencies(ex);

            // Assert
            // Times.Once is very important to test that it isn't hanging on to previously mocked objects during a test run
            Assert.IsNotNull(result);
            myClass.DependsOn<ICrossCuttingConcern>()
                .Verify(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("string")), ex), Times.Once);
            myClass.DependsOn<ICredentials>()
                .Verify(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"), Times.Once);
        }

        [Test]
        public void It_Should_Not_Reuse_Mocks()
        {
            // Arrange
            var ex = new TestException();

            // Act
            var result = myClass.Target.DoWorkWithDependencies(ex);

            // Assert
            // Times.Once is very important to test that it isn't hanging on to previously mocked objects during a test run
            Assert.IsNotNull(result);
            myClass.DependsOn<ICrossCuttingConcern>()
                .Verify(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("string")), ex), Times.Once);
            myClass.DependsOn<ICredentials>()
                .Verify(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"), Times.Once);
        }
    }
}