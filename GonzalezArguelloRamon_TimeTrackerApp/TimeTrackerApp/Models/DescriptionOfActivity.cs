using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTrackerApp.Models
{
    // Description Of Activity Entity
  public class DescriptionOfActivity
  {
      public int Id { get; set; }
      public string ActivityDescription { get; set; }
      private DescriptionByCategory Categories { get; set; }

      public DescriptionOfActivity()
      {
        Id = default(int);
        ActivityDescription = string.Empty;
        Categories = new DescriptionByCategory();
      }

      public int GetCategoryId()
      {
        return Categories.List.First(s => s.DescriptionId == Id).CategoryId;
      }
  }
}
