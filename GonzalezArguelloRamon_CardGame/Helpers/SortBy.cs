namespace CardGame.Helpers
{
  /// <summary>
  ///   Supported Ways to Sort Restaurant Profiles
  /// </summary>
  public enum SortBy
  {
    /// <summary>
    ///   Fallback ENUM
    /// </summary>
    DEFAULT = 0,

    /// <summary>
    ///   Sort By Restaurant Name Ascending
    /// </summary>
    NAME_ASC = 1,

    /// <summary>
    ///   Sort By Restaurant Name Descending
    /// </summary>
    NAME_DESC = 2,

    /// <summary>
    ///   Sort by Best to Worst Descending
    /// </summary>
    BEST_DESC = 3,

    /// <summary>
    ///   Sort by Worst to Best Descending
    /// </summary>
    WORST_DESC = 4,

    /// <summary>
    ///   Filter The BEST
    /// </summary>
    THE_BEST = 5,

    /// <summary>
    ///   Filter by 4 stars or more
    /// </summary>
    IV_STARS_UP = 6,

    /// <summary>
    ///   Filter by 3 stars or more
    /// </summary>
    III_STARS_UP = 7,

    /// <summary>
    ///   Filter the WORST
    /// </summary>
    THE_WORST = 8,

    /// <summary>
    ///   Filter by unrated
    /// </summary>
    UNRATED = 9
  }
}