using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	static class Drawing
	{
		public static void Draw(List<AllObj> listOfObj)
		{
			// рисовка bg
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.Write(Background.GetBG());
			Console.ResetColor();

			foreach (AllObj DrawObj in listOfObj)
			{

				DrawObject(DrawObj.View, DrawObj.X, DrawObj.Y);

				if (DrawObj is Bullets)
				{
					if (DrawObj.Y > 0)
					{
						DrawObj.Y--;
					}
					else
					{
						DrawObj.isDraw = false;
					}
				}

				if (DrawObj is Stone)
				{
					if (DrawObj.Y > Console.BufferHeight-1)
					{
							DrawObj.isDraw = false;
							Program.Score--;
					}
				}
			}
			Console.SetCursorPosition(0, 0);
			Console.Write("Score: {0}\n", Program.Score);
			Ship ship = (Ship)listOfObj[0];
			Console.Write("Lives: ");
			for (int i = 0; i < ship.getLives(); i++)
			{
				Console.Write("\u2665 ");
			}
		}

		static public void DrawObject(string[] view, int Xpos, int Ypos)
		{
			int i = 0;
			foreach (string str in view)
			{
				if (Ypos+i < 0)
				{
					i++;
					continue;
				}
				if (Ypos + i >= Console.BufferHeight-1)
				{
					break;
				}
				Console.SetCursorPosition(Xpos, Ypos + i);
				Console.Write(str);
				i++;
			}
		}
	}
}
