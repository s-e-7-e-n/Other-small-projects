using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	class Stone: AllObj
	{
		public string[] view = { "[----]", "[----]", "[----]" };
		public int Velocity = 0;
		public Stone(int x, int y)
		{
			var rand = new Random();
			Velocity = rand.Next(1, 5) * 5; // рандомная скорость камня(чем меньше значение, тем быстрее будет премещение)
			isDraw = true;
			View = view;
			X = x;
			Y = y;
		}
	}
}
