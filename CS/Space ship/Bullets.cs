using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	class Bullets: AllObj
	{
		readonly string[] view = { "*" };
		public Bullets(int x, int y)
		{
			isDraw = true;
			View = view;
			X = x;
			Y = y;
		}
	}
}
