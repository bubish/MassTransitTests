using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MassTransit;
using Common.Messages;
using BackEnd.Consumers;

namespace BackEnd {
  class Program {
    static void Main(string[] args) {
      var container = new UnityContainer();

      //var typesToRegister = AppDomain.CurrentDomain
      //                          .GetAssemblies()
      //                          .SelectMany(x => x.GetTypes())
      //                          .Where(x => x.GetInterfaces().Contains(typeof(IConsumer)))
      //                          .ToList(); ;

      //foreach (var type in typesToRegister) {
      //  container.RegisterType(typeof(IConsumer), type);
      //}

      container.RegisterType(typeof(IConsumer), typeof(FrontEndInteractionConsumer));

      var messageBus = ServiceBusFactory.New(config => {
        config.UseMsmq();
        config.VerifyMsmqConfiguration();

        config.ReceiveFrom("msmq://localhost/BackEnd");
        config.Subscribe(x => x.LoadFrom(container));
      });

      container.RegisterInstance<IServiceBus>(messageBus);

      while (true) {
        var foo = Console.ReadLine();
        Console.WriteLine("You typed: " + foo);
      }
    }
  }
}
