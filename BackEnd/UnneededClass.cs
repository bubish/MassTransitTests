namespace BackEnd {
  public interface IUnneededClass {
    string foo { get; }
  }

  class UnneededClass : IUnneededClass {
    private readonly string _bar;

    public UnneededClass(string bar) {
      _bar = bar;
    }

    public string foo {
      get { return _bar; }
    }
  }
}
