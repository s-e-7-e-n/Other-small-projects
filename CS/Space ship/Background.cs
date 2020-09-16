using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	static class Background
	{
		static int timer = 0;
		//static private List<StringBuilder> background = new List<StringBuilder>();
		static private StringBuilder Bg = new StringBuilder();

		static public void CreateBackground()
		{
			Bg.Clear();
			for (int h = 0; h < Console.LargestWindowHeight-1; h++)
			{
				System.Threading.Thread.Sleep(10);
				Bg.Append(getOneStr());
				//Console.Clear();
				//Console.Write(Bg);
			}
		}

		static private StringBuilder getOneStr()
		{
			var rand = new Random();
			StringBuilder StrOfBg = new StringBuilder();
			int whereAstro = rand.Next(0, Console.LargestWindowWidth);
			for (int w = 0; w < Console.LargestWindowWidth; w++)
			{
				if (w == whereAstro)
				{
					StrOfBg.Append('+');
				}
				else
				{
					StrOfBg.Append(' ');
				}
			}
			return StrOfBg;
		}

		static public StringBuilder GetBG()
		{
			if (timer++ == 5)
			{
				Bg.Remove(Bg.Length - Console.LargestWindowWidth, Console.LargestWindowWidth);
				Bg.Insert(0, getOneStr());
				timer = 0;
			}
			return Bg;
		}

	}
}
