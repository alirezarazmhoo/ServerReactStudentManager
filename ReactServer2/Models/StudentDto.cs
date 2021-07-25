using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactServer2.Models
{
	public class StudentDto
	{
		public List<Student> students { get; set; }
		public bool hasNext { get; set; }
	}
}
