using System;

namespace CardGame.Extensions
{
  /// <summary>
  ///   Random extension methods
  /// </summary>
  public static class RandomExtension
  {
    /// <summary>
    ///   Random double value
    /// </summary>
    /// <param name="random">system type</param>
    /// <param name="min">min limit</param>
    /// <param name="max">max limit</param>
    /// <returns></returns>
    public static double NextRandom(this Random random, double min, double max)
    {
      return random.NextDouble() * (max - min) + min;
    }

    /// <summary>
    ///   Random int value
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int NextRandom(this Random random, int min, int max)
    {
      return (int) Math.Floor(random.NextDouble() * (max - min) + min);
    }
  }
}