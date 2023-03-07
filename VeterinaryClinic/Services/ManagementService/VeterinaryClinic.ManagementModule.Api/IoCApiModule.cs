using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using VeterinaryClinic.AppointmentModule.Domain;
using Module = Autofac.Module;

namespace VeterinaryClinic.ManagementModule.Api
{
    public class IoCApiModule : Module
    {
        public IoCApiModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            //-------------------  REGISTER MEDIATR FOR Autofac ---------------------------
            /*
             * https://github.com/jbogard/MediatR/tree/master/samples/MediatR.Examples.Autofac             
             */

            //REGISTER MEDIAT-R INSTANCE
            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            //REGISTER MEDIAT-R TYPES
            List<Assembly> _assemblies = new List<Assembly>();
            //var apiAssembly = Assembly.GetAssembly(typeof(IoCApiModule));
            var domainAssembly = Assembly.GetAssembly(typeof(IoCDomainModule));
            //var infrastructureAssembly = Assembly.GetAssembly(typeof(IoCInfrastructureModule));
            //_assemblies.Add(apiAssembly);
            _assemblies.Add(domainAssembly);
            //_assemblies.Add(infrastructureAssembly);

            //var applicationAssembly = typeof(IoCDomainModule).GetTypeInfo().Assembly;
            //var applicationAssembly = Assembly.GetAssembly(typeof(IoCDomainModule));
            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>)
            };
            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }
        }
    }
}
