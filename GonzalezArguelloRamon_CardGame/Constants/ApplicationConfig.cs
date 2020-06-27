using System;
using System.Collections;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CardGame.Constants
{
  /// <summary>
  ///   Global Data Visualization Configurations
  /// </summary>
  public static class ApplicationConfig
  {
    /// <summary>
    ///   App configuration access
    /// </summary>
    public static IConfigurationRoot ConfigurationRoot => new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile(APP_CONFIG, false)
      .Build();

    /// <summary>
    ///   My portable configurations
    /// </summary>
    public static string APP_CONFIG => "appsettings.json";

    /// <summary>
    ///   Welcome message
    /// </summary>
    public static string WELCOME => "Hello Admin, What Would You Like To Do Today?\n";

    /// <summary>
    ///   Testing mode, will only take up to LIMIT reviews to process.
    ///   Set TestingMode = true in the configuration file to enable this behaviour.
    /// </summary>
    public static bool IS_TESTING => Convert.ToBoolean(ConfigurationRoot["TestingMode"]);

    /// <summary>
    ///   If testing mode is enable this property will set the limit of reviews
    ///   to be processed by the chart generator
    /// </summary>
    public static int LIMIT => Convert.ToInt32(ConfigurationRoot["TakeUpToRows"]);

    /// <summary>
    ///   Chart Base Scale
    /// </summary>
    public static int BASE_SCALE => Convert.ToInt32(ConfigurationRoot["ChartScale"]);

    /// <summary>
    ///   Chart generation delay default 300, to increase it edit the appsettings config.
    /// </summary>
    public static int CHART_DELAY => Convert.ToInt32(ConfigurationRoot["ChartDelay"]);

    /// <summary>
    ///   Animation loop times
    /// </summary>
    public static int ANIMATION_LOOP => Convert.ToInt32(ConfigurationRoot["AnimationLoop"]);

    /// <summary>
    ///   Random rating limits
    /// </summary>
    public static Hashtable RANDOM_LIMITS => new Hashtable
    {
      {"min", 10.4d}, // one char
      {"max", 99.7d} // ten chars
    };

    /// <summary>
    ///   JSON output path
    /// </summary>
    public static string OUTPUT_PATH => ConfigurationRoot["StoreJSON"];
  }
}