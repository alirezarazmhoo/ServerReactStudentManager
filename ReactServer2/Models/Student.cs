using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactServer2.Models
{
	public class Student
	{
		public int id { get; set; }
		public string name { get; set; }
		public string lastname { get; set;  }
		public string nationalcode { get; set; }
		public string address { get; set;  }
		public string url { get; set; }

		public int MajorId { get; set;  }
		public virtual Major Major { get; set; }
	}
}
