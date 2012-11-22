using System;
using MassTransit;
using Common.Messages;

namespace FrontEnd {
  class Program {
    public class YourMessage { public string Text { get; set; } }
    static void Main(string[] args) {
      var messageBus = ServiceBusFactory.New(config => {
        config.UseMsmq();
        config.VerifyMsmqConfiguration();
        config.UseMulticastSubscriptionClient();

        config.ReceiveFrom("msmq://localhost/FrontEnd");
      });

      Console.WriteLine("Send a message!");

      while (true) {
        var text = Console.ReadLine();
        if(string.Equals(text, "exit", StringComparison.OrdinalIgnoreCase)) break;
        messageBus.Publish(new FrontEndInteraction { Text = text });  
      }
      
    }
  }
}
