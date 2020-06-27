using System;

namespace CardGame.Screens
{
  public interface IVisualizationOption
  {
    /// <summary>
    ///   Selected Option
    /// </summary>
    private int Selection
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    ///   Run the selected option
    /// </summary>
    private void ProcessSelection()
    {
      throw new NotImplementedException();
    }
  }
}