using System;

namespace Common.Messages {
  public class FrontEndInteractionNoWrite {
    public string Text { get; set; }
    public DateTime Sent { get; set; }
    public string Host { get; set; }
  }
}
