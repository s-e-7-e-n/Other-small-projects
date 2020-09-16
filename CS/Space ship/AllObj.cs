using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	abstract class AllObj
	{
		public bool isDraw = true;
		public string[] View { get; set; }
		public int X, Y;
	}
}
