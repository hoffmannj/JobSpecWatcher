using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSpecApi.Models
{
	public class CallParams
	{
		public string Skills { get; set; }  //';' delimited string
		public int LastNDays { get; set; }
		public int ResultDays { get; set; }
	}
}
