using System;
using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForInstanceTests
{
    public class TestableWrapperForTestableClassWithManyConstructors
    {
        [Test]
        public void It_Should_Use_The_Constructor_With_The_Most_Parameters()
        {
            // Arrange
            var ex = new TestException();

            var myClass = new TestableInstance<TestableClassWithManyConstructors>();

            myClass.DependsOn<ICredentials>()
                .Setup(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"))
                .Returns(new NetworkCredential {UserName = "Bozo"});

            myClass.DependsOn<ICrossCuttingConcern>()
                .Setup(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("Bozo did stuff")), ex));

            // Act
            myClass.Target.LogMessageJustBecause(ex);

            // Assert
            myClass.VerifyAll();
        }
    }
}