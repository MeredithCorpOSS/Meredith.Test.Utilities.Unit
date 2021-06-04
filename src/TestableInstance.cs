using Moq;

namespace Meredith.Test.Utilities.Unit
{
    public class TestableInstance<T>
        where T : class
    {
        private readonly AutoMocker<T> _autoMocker;

        public TestableInstance()
        {
            _autoMocker = new AutoMocker<T>();
        }

        public virtual Mock<TMocked> DependsOn<TMocked>()
            where TMocked : class
        {
            return _autoMocker.The<TMocked>();
        }

        public virtual T Target => _autoMocker.Target;

        public virtual void VerifyAll()
        {
            _autoMocker.VerifyAll();
        }
    }
}
