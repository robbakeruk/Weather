namespace Weather.Service.Core
{
    public interface IContainer
    {
        TType Get<TType>();
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void RegisterInstance<TInstance>(TInstance instance);
        void Dispose();
    }
}
