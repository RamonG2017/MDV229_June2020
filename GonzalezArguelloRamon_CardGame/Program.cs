using CardGame.Screens;

namespace CardGame
{
  public static class Program
  {
    private static readonly Welcome Welcome = new Welcome();

    private static void Main(string[] args)
    {
      Welcome.Header();
    }
  }
}