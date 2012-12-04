using System;
using BackEnd.Entities;
using MassTransit;
using Common.Messages;
using MongoDB.Driver;

namespace BackEnd.Consumers {
  public class FrontEndInteractionConsumer : Consumes<FrontEndInteraction>.All {
    private readonly MongoCollection<InteractionRecord> _interactions;

    public FrontEndInteractionConsumer(MongoDatabase db) {
      _interactions = db.GetCollection<InteractionRecord>("InteractionRecords");
    }

    public void Consume(FrontEndInteraction message) {

      Console.WriteLine("+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");
      Console.WriteLine("Message: " + message.Text);
      Console.WriteLine("From Host: " + message.Host);
      Console.WriteLine("At: " + message.Sent);
      Console.WriteLine("+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");

      var record = new InteractionRecord {Host = message.Host, Sent = message.Sent, Text = message.Text};
      _interactions.Insert(record);
    }
  }
}
