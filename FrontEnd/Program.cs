﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;

namespace FrontEnd {
  class Program {
    static void Main(string[] args) {
      var messageBus = ServiceBusFactory.New(bus => bus.ReceiveFrom("loopback://localhost/queue"));

    }
  }
}
