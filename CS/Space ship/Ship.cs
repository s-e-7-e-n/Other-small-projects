using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	class Ship : AllObj
	{
		private int Lives;
		private readonly int StartX = Console.LargestWindowWidth / 2;
		private readonly int StartY = Console.LargestWindowHeight - 8;
		public string[] view = {
		 "       ^      ",
		@"      / \      ",
		 "      |U|      ",
		 "______|S|______",
		@"\_____|S|_____/",
		@" \____|R|____/ ",
		@"      /х\      " };
		//new public int X, Y;
		public Ship()
		{
			Lives = 3;
			View = view;
			X = StartX;
			Y = StartY;
		}

		public void Death()
		{
			Lives--;
		}

		public void giveLive()
		{
			Lives++;
		}

		public int getLives()
		{
			return Lives;
		}
	}
}
