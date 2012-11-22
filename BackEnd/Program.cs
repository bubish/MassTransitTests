using System;
using System.Linq;
using Microsoft.Practices.Unity;
using MassTransit;
using BackEnd.Consumers;

namespace BackEnd {
  class Program {
    static void Main(string[] args) {
      var container = new UnityContainer();

      //var typesToRegister = AppDomain.CurrentDomain
      //                          .GetAssemblies()
      //                          .SelectMany(x => x.GetTypes())
      //                          .Where(x => x.GetInterfaces().Contains(typeof(IConsumer)) && x.IsClass && !x.IsGenericType)
      //                          .ToList();

      var typesToRegister =
        typeof (Program).Assembly.GetTypes().Where(
          x => !x.IsGenericType && x.GetInterfaces().Contains(typeof(IConsumer)));

      foreach (var type in typesToRegister) {
        container.RegisterType(typeof(IConsumer), type);
      }

      container.RegisterInstance(typeof(IUnneededClass), new UnneededClass("Test:"));

      //container.RegisterType(typeof(IConsumer), typeof(FrontEndInteractionConsumer));

      var messageBus = ServiceBusFactory.New(config => {
        config.UseMsmq();
        config.VerifyMsmqConfiguration();
        config.UseMulticastSubscriptionClient();

        config.ReceiveFrom("msmq://localhost/BackEnd");
        config.Subscribe(x => x.LoadFrom(container));
      });

      container.RegisterInstance(messageBus);

      Console.WriteLine("Hit enter to close");

      Console.ReadLine();
    }
  }
}
