using System;
using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForInstanceTests
{
    [TestFixture]
    public class TestableWrapperForTestableClassWithPropertyInjection
    {
        [Test]
        public void It_Should_Resolve_Items_Set_On_Target()
        {
            // Arrange
            var testableClassWithPropertyInjection = new TestableInstance<TestableClassWithPropertyInjection>();

            testableClassWithPropertyInjection.Target.CrossCuttingConcern = testableClassWithPropertyInjection.DependsOn<ICrossCuttingConcern>().Object;
            testableClassWithPropertyInjection.Target.Credentials = testableClassWithPropertyInjection.DependsOn<ICredentials>().Object;

            var ex = new TestException();
            testableClassWithPropertyInjection.DependsOn<ICredentials>()
                .Setup(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"))
                .Returns(new NetworkCredential { UserName = "Bozo" });

            testableClassWithPropertyInjection.DependsOn<ICrossCuttingConcern>()
                .Setup(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("Bozo did stuff")), ex));

            // Act
            testableClassWithPropertyInjection.Target.LogMessageJustBecause(ex);

            // Assert
            testableClassWithPropertyInjection.VerifyAll();
        }
    }
}
