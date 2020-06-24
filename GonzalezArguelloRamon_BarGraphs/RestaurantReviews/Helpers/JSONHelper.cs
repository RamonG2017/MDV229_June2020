using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Helpers
{
  /// <summary>
  ///   JSON Converter Helper
  /// </summary>
  public class JSONHelper
  {
    /// <summary>
    ///   Convert collection to JSON
    /// </summary>
    /// <param name="collection">Generic collection</param>
    /// <param name="skip">list of properties not to be added to the final output</param>
    /// <typeparam name="T">Collection Type</typeparam>
    /// <returns>JSON output</returns>
    public string ToJSON<T>(IEnumerable<T> collection, IEnumerable<string> skip)
    {
      // container for huge text
      var output = new StringBuilder($"Restaurant Review:[{Environment.NewLine}");
      // iterate over the collection
      for (var i = 0; i < collection.Count(); i++)
      {
        // get element at the current position of the iteration
        var entity = collection.ElementAt(i);
        // open JSON Object Notation
        output.Append("\t{").Append(Environment.NewLine);
        // get all members of the generic class using reflection
        var properties = entity.GetType().GetProperties();
        // iterate over all properties
        for (var j = 0; j < properties.Length; j++)
        {
          // get current property
          var property = properties.ElementAt(j);
          // if the property name is not to be skipped continue
          if (skip.Contains(property.Name)) continue;
          // append property name and content value
          output.Append($"\t\t\"{property.Name}\": \"{property.GetValue(entity, null)}\"");
          // should add coma to the property?
          if (j < properties.Length - skip.Count() - 1)
            output.Append(",");
          output.Append(Environment.NewLine);
        }

        // should add coma to the object?
        if (i < collection.Count() - 1)
          output.Append("\t},").Append(Environment.NewLine);
        else
          output.Append("\t}").Append(Environment.NewLine);
      }

      // close our JSON object
      output.Append("]");
      return output.ToString();
    }
  }
}