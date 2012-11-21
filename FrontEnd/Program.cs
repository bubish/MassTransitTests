using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Text;
using MassTransit;
using MassTransit.UnityIntegration;
using Common.Messages;

namespace FrontEnd {
  class Program {
    static void Main(string[] args) {
      //var container = new UnityContainer();

      //var typesToRegister = AppDomain.CurrentDomain
      //                          .GetAssemblies()
      //                          .SelectMany(x => x.GetTypes())
      //                          .Where(x=>x.GetInterfaces().Contains(typeof(IConsumer)));

      //foreach (var type in typesToRegister) {
      //  container.RegisterType(type, type);
      //}

      var messageBus = ServiceBusFactory.New(config => 
      {
        config.UseMsmq();
        config.VerifyMsmqConfiguration();

        config.ReceiveFrom("msmq://localhost/FrontEnd");
        //bus.Subscribe(x => x.LoadFrom(container));
      });

      //container.RegisterInstance<IServiceBus>(messageBus);
      if (args.Length > 0) {
        messageBus.Publish(new FrontEndInteraction { Text = args[0]});
      }
    }
  }
}
