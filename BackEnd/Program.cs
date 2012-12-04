using System;
using System.Linq;
using MassTransit.BusConfigurators;
using Microsoft.Practices.Unity;
using MassTransit;
using MongoDB.Driver;

namespace BackEnd {
  class Program {
    static void Main() {
      //set up the Mongo connection
      const string connectionString = "mongodb://192.168.182.1";
      var client = new MongoClient(connectionString);
      var server = client.GetServer();
      var database = server.GetDatabase("BackEnd");

      //set up unity
      var container = new UnityContainer();

      var typesToRegister =
        typeof(Program).Assembly.GetTypes().Where(
          x => !x.IsGenericType && x.GetInterfaces().Contains(typeof(IConsumer)));

      foreach(var type in typesToRegister) {
        container.RegisterType(type);
      }

      container.RegisterInstance(database);

      //set up the message bus
      var messageBus = ServiceBusFactory.New(config => {
        config.UseMsmq();
        config.VerifyMsmqConfiguration();
        config.UseMulticastSubscriptionClient();

        config.ReceiveFrom("msmq://localhost/BackEnd");

        //config.UseRabbitMq();
        //config.ReceiveFrom("rabbitmq://localhost/BackEnd");

        config.Subscribe(x => x.LoadFrom(container));
      });

      container.RegisterInstance(messageBus);

      //wait for messages
      Console.WriteLine("Hit enter to close");

      Console.ReadLine();

      messageBus.Dispose();
    }
  }
}
