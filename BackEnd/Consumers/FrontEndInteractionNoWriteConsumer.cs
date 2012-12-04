using System;
using MassTransit;
using Common.Messages;

namespace BackEnd.Consumers {
  public class FrontEndInteractionNoWriteConsumer : Consumes<FrontEndInteractionNoWrite>.All {

    public void Consume(FrontEndInteractionNoWrite message) {

      Console.WriteLine("+=+=+=+=!No Write!+=+=+=+=");
      Console.WriteLine("Message: " + message.Text);
      Console.WriteLine("From Host: " + message.Host);
      Console.WriteLine("At: " + message.Sent);
      Console.WriteLine("+=+=+=+=!No Write!+=+=+=+=");
    }
  }
}
