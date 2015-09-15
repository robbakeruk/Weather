using Ninject;
using Weather.Service.Core;

namespace Weather.Test.Core
{
    public class Container : IContainer
    {
        private readonly IKernel _kernel;

        public Container()
        {
            _kernel = new StandardKernel();
        }

        public TType Get<TType>()
        {
            return _kernel.Get<TType>();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>();
        }

        public void RegisterInstance<TInstance>(TInstance instance)
        {
            _kernel.Unbind<TInstance>();
            _kernel.Bind<TInstance>().ToConstant(instance);
        }

        public void Dispose()
        {
            _kernel?.Dispose();
        }
    }
}
