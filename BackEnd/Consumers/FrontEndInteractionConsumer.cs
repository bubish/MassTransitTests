using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using Common.Messages;

namespace BackEnd.Consumers {
  class FrontEndInteractionConsumer : Consumes<FrontEndInteraction>.Context, IBusService {
    private IServiceBus _bus;
    
    public void Consume(IConsumeContext<FrontEndInteraction> context) {
      var foo = context.Message;
      Console.WriteLine(foo.Text);
    }

    public void Start(IServiceBus bus) {
      _bus = bus;
      Console.WriteLine("Message consumer started");
    }

    public void Stop() { 
      Console.WriteLine("Message consumer stopping");
    }

    public void Dispose() {
      _bus.Dispose();
    }
  }
}
