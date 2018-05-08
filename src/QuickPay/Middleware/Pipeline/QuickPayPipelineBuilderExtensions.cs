using DotCommon.Dependency;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuickPay.Middleware.Pipeline
{
    public static class QuickPayPipelineBuilderExtensions
    {
        internal const string InvokeMethodName = "Invoke";
        internal const string InvokeAsyncMethodName = "InvokeAsync";

        public static IQuickPayPipelineBuilder UseMiddleware<TMiddleware>(this IQuickPayPipelineBuilder app, params object[] args)
        {
            return app.UseMiddleware(typeof(TMiddleware), args);
        }

        public static IQuickPayPipelineBuilder UseMiddleware(this IQuickPayPipelineBuilder app, Type middleware, params object[] args)
        {
            return app.Use(next =>
            {
                var methods = middleware.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var invokeMethods = methods.Where(m =>
                    string.Equals(m.Name, InvokeMethodName, StringComparison.Ordinal)
                    || string.Equals(m.Name, InvokeAsyncMethodName, StringComparison.Ordinal)
                ).ToArray();

                if (invokeMethods.Length > 1)
                {
                    throw new InvalidOperationException();
                }

                if (invokeMethods.Length == 0)
                {
                    throw new InvalidOperationException();
                }

                var methodinfo = invokeMethods[0];
                if (!typeof(Task).IsAssignableFrom(methodinfo.ReturnType))
                {
                    throw new InvalidOperationException();
                }

                var parameters = methodinfo.GetParameters();
                if (parameters.Length == 0 || parameters[0].ParameterType != typeof(ExecuteContext))
                {
                    throw new InvalidOperationException();
                }

                var ctorArgs = new object[args.Length + 1];
                ctorArgs[0] = next;
                Array.Copy(args, 0, ctorArgs, 1, args.Length);

                //??
                var instance = IocManager.GetContainer().Resolve(middleware);
                var quickPayExecuteDelegate = (QuickPayExecuteDelegate)methodinfo.CreateDelegate(typeof(QuickPayExecuteDelegate), instance);

                return context =>
                {
                    return quickPayExecuteDelegate(context);
                };
            });
        }


        public static IQuickPayPipelineBuilder Use(this IQuickPayPipelineBuilder app, Func<ExecuteContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return middleware(context, simpleNext);
                };
            });
        }

       
        private static object GetService(IServiceProvider sp, Type type)
        {
            var service = sp.GetService(type);
            if (service == null)
            {
                throw new InvalidOperationException();
            }

            return service;
        }
    }
}
