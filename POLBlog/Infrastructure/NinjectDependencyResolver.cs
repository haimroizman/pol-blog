using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;
using POLBlog.Domain.Abstract;
using POLBlog.Domain.Concrete;
using POLBlog.Infrastructure.Abstract;
using POLBlog.Infrastructure.Concrete;


namespace POLBlog.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel=new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }

        public IKernel Kernel
        {
            get { return kernel; }
        }

        private void AddBindings()
        {
            Bind<IPostsRepository>().To<EFPostRepository>();
            Bind<IArtsRepository>().To<EFArtRepository>();
            Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}