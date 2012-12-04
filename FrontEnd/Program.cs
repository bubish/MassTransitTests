using System;
using System.Net;
using MassTransit;
using Common.Messages;

namespace FrontEnd {
  class Program {
    public class YourMessage { public string Text { get; set; } }
    static void Main() {
      var messageBus = ServiceBusFactory.New(config => {
        config.UseMsmq();
        config.VerifyMsmqConfiguration();
        config.UseMulticastSubscriptionClient();

        config.ReceiveFrom("msmq://localhost/FrontEnd");

        //config.UseRabbitMq();
        //config.ReceiveFrom("rabbitmq://localhost/FrontEnd");

      });

      Console.WriteLine("Send a message!");
      string text;

      while ((text = Console.ReadLine()) != "exit") {
        if (text.StartsWith("NoWrite: ", StringComparison.OrdinalIgnoreCase)) {
          messageBus.Publish(new FrontEndInteractionNoWrite { Text = text, Sent = DateTime.Now, Host = Dns.GetHostName() });
        }
        else {
          messageBus.Publish(new FrontEndInteraction { Text = text, Sent = DateTime.Now, Host = Dns.GetHostName() });
        }
      }

      messageBus.Dispose();
    }
  }
}
