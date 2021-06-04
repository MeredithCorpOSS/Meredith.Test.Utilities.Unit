using System;
using System.Net;
using Meredith.Test.Utilities.Unit.Tests.Testables;
using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit.Tests.TestableWrapperForTests
{
    [TestFixture]
    public class TestableWrapperForTestableClassWithPropertyInjection : TestableWrapperFor<TestableClassWithPropertyInjection>
    {
        [Test]
        public void It_Should_Resolve_Items_Set_On_Target()
        {
            // Arrange
            Target.CrossCuttingConcern = The<ICrossCuttingConcern>().Object;
            Target.Credentials = The<ICredentials>().Object;

            var ex = new TestException();
            The<ICredentials>()
                .Setup(credentials => credentials.GetCredential(It.IsAny<Uri>(), "Basic"))
                .Returns(new NetworkCredential() {UserName = "Bozo"});

            The<ICrossCuttingConcern>()
                .Setup(log => log.SomeMethod(It.Is<string>(msg => msg.StartsWith("Bozo did stuff")), ex));

            // Act
            Target.LogMessageJustBecause(ex);

            // Assert
            VerifyAll();
        }
    }
}
