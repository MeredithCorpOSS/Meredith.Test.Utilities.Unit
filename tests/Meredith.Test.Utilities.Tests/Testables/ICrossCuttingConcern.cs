namespace Meredith.Test.Utilities.Unit.Tests.Testables
{
    public interface ICrossCuttingConcern<T>
    {
        void SomeMethod(string someItem, object someOtherItem);
    }

    public interface ICrossCuttingConcern
    {
        void SomeMethod(string someItem, object someOtherItem);
    }
}