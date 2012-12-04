using System;
using MongoDB.Bson;

namespace BackEnd.Entities {
  public class InteractionRecord {

    public ObjectId Id { get; set; }
    public string Text { get; set; }
    public DateTime Sent { get; set; }
    public string Host { get; set; }
  }
}
