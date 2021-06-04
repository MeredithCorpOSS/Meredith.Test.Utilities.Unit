using Moq;
using NUnit.Framework;

namespace Meredith.Test.Utilities.Unit
{
    // TestableWrapperFor has a nUnit Setup method, but is not iself a test fixture due to the build 
    //    getting confused when looking for tests to run and producing test warnings
    public class TestableWrapperFor<T>
        where T : class
    {
        private readonly AutoMocker<T> _autoMocker;

        public TestableWrapperFor()
        {
            _autoMocker = new AutoMocker<T>();
        }

        [SetUp]
        protected virtual void ClearMocks()
        {
            _autoMocker.Clear();
        }

        protected virtual T Target => _autoMocker.Target;

        protected virtual Mock<TMocked> The<TMocked>()
            where TMocked : class
        {
            return _autoMocker.The<TMocked>();
        }

        protected virtual void VerifyAll()
        {
            _autoMocker.VerifyAll();
        }
    }
}
