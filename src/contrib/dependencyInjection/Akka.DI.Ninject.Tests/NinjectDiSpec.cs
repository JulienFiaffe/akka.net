﻿using System;
using System.Diagnostics;
using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.TestKit;
using Ninject;

namespace Akka.DI.Ninject.Tests
{
    public class NinjectDiSpec : DiResolverSpec
    {
        protected override object NewDiContainer()
        {
            return new StandardKernel();
        }

        protected override IDependencyResolver NewDependencyResolver(object diContainer, ActorSystem system)
        {
            var kernel = ToKernel(diContainer);
            return new NinjectDependencyResolver(kernel, system);
        }

        protected override void Bind<T>(object diContainer, Func<T> generator)
        {
            var kernel = ToKernel(diContainer);
            kernel.Bind<T>().ToMethod(context => generator());
        }

        protected override void Bind<T>(object diContainer)
        {
            var kernel = ToKernel(diContainer);
            kernel.Bind<T>().ToSelf();
        }

        private static IKernel ToKernel(object diContainer)
        {
            var kernel = diContainer as IKernel;
            Debug.Assert(kernel != null, "kernel != null");
            return kernel;
        }
    }
}
