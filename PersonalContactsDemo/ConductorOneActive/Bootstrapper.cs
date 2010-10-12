using Caliburn.Micro;
using ConductorOneActive.ViewModels;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Collections;
using ConductorOneActive.Framework;
using ConductorOneActive.Models;

namespace ConductorOneActive
{
    public class Bootstrapper : Caliburn.Micro.Bootstrapper<ShellViewModel>
    {
        public Bootstrapper()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<ShellViewModel>().Singleton().Use<ShellViewModel>();
                x.For<SearchScreenViewModel>().Singleton().Use<SearchScreenViewModel>();

                x.For<NoResultsViewModel>().Use<NoResultsViewModel>();
                x.For<ResultsViewModel>().Use<ResultsViewModel>();
                
                //x.For<AddPersonContactViewModel>().Use<AddPersonContactViewModel>();
                //x.For<IndividualResultViewModel>().Use<IndividualResultViewModel>();

                x.For<IWindowManager>().Singleton().Use<WindowManager>();
                x.For<IEventAggregator>().Singleton().Use<EventAggregator>();


                x.For<IBusyService>().Singleton().Use<DefaultBusyService>();
                x.For<IBackend>().Singleton().Use<FakeBackend>();
                x.For<IValidator>().Singleton().Use<DefaultValidator>();

                // First we create a new Setter Injection Policy that
                // forces StructureMap to inject all public properties
                // where the PropertyType is IGateway
                x.SetAllProperties(y =>
                {
                    y.OfType<IBackend>();
                });
                

            });
            //ObjectFactory.AssertConfigurationIsValid();

        }

        protected override object GetInstance(Type serviceType, string key)
        {

            return String.IsNullOrEmpty(key) ? ObjectFactory.Container.GetInstance(serviceType) : ObjectFactory.Container.GetInstance(serviceType, key);

            
         
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return ObjectFactory.Container.GetAllInstances(serviceType).AsEnumerable();            
        }

        protected override void BuildUp(object instance)
        {
            ObjectFactory.Container.BuildUp(instance);
        }  
    }
}
