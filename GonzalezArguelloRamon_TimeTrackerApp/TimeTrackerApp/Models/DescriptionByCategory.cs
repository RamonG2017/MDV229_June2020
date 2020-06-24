using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerApp.Models
{
  public class DescriptionByCategory
  {
    public List<DxC> List { get; set; }

    public DescriptionByCategory()
    {
      List = new List<DxC>()
      {
        new DxC
        {
          DescriptionId = 1,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 2,
          CategoryId = 2
        },
        new DxC
        {
          DescriptionId = 3,
          CategoryId = 2
        },
        new DxC
        {
          DescriptionId = 4,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 5,
          CategoryId = 3
        },
        new DxC
        {
          DescriptionId = 6,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 7,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 8,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 9,
          CategoryId = 4
        },
        new DxC
        {
          DescriptionId = 10,
          CategoryId = 4
        },
        new DxC
        {
          DescriptionId = 11,
          CategoryId = 4
        },
        new DxC
        {
          DescriptionId = 12,
          CategoryId = 4
        },
        new DxC
        {
          DescriptionId = 13,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 14,
          CategoryId = 6
        },
        new DxC
        {
          DescriptionId = 15,
          CategoryId = 5
        },
        new DxC
        {
          DescriptionId = 16,
          CategoryId = 7
        }
      };
    }
  }

  public class DxC
  {
    public int DescriptionId { get; set; }
    public int CategoryId { get; set; }
  }
}
