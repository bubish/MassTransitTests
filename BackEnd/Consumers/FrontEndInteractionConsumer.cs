using System;
using System.Threading;
using MassTransit;
using Common.Messages;

namespace BackEnd.Consumers {
  public class FrontEndInteractionConsumer : Consumes<FrontEndInteraction>.All {
    private readonly IUnneededClass _foo;

    public FrontEndInteractionConsumer(IUnneededClass foo) {
      _foo = foo;
    }

    public void Consume(FrontEndInteraction message) {
      Console.WriteLine(_foo.foo + " " + message.Text);
    }
  }
}
