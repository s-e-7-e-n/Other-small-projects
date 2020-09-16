using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	class MainMenu
	{
		static System.Media.SoundPlayer MenuMusic = new System.Media.SoundPlayer(Properties.Resources.MainMenu);
		static private readonly string[] ChoiceArray = new string[] {"Новая игра", "Продолжить", "Выйти"};
		static private int choice = 0;
		static public int Choice
		{
			get
			{
				return choice;
			}
			set
			{
				if (value <= 2 & value >= 0)
				{
					choice = value;
				}
			}
		}
		static public bool showingMenu;

		static public void ShowMenu()
		{
			showingMenu = true;
			if (!Program.isPlay)
			{
				MenuMusic.PlayLooping();
				Choice = 0;
			}
			Console.Clear();
			int Ypos = Console.BufferHeight / 2 - TitleBox.GameName.Length / 2 - 10;
			Drawing.DrawObject(TitleBox.GameName, Console.BufferWidth / 2 - TitleBox.GameName[0].Length/2, Console.BufferHeight / 2 - TitleBox.GameName.Length/2 - 10);
			while (showingMenu)
			{
				Console.CursorVisible = false;
				for (int i = 0; i < ChoiceArray.Length; i++)
				{
					if (i == choice)
					{
						Console.ForegroundColor = ConsoleColor.DarkRed;
					}
					if (i == 1 & Program.isPlay == false)
					{
						Console.ForegroundColor = ConsoleColor.DarkGray;
					}
					Console.SetCursorPosition(Console.BufferWidth/2 - ChoiceArray[i].Length/2, Ypos + TitleBox.GameName.Length + 2 + i*2);
					Console.Write(ChoiceArray[i]);
					Console.ResetColor();
				}
				if (Console.KeyAvailable == true)
				{
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.DownArrow:
							Choice++;
							if (Program.isPlay == false & choice == 1)
							{
								Choice++;
							}
							break;
						case ConsoleKey.UpArrow:
							Choice--;
							if (Program.isPlay == false & choice == 1)
							{
								Choice--;
							}
							break;
						case ConsoleKey.Enter:
							switch (choice)
							{
								case 0:
									showingMenu = false;
									if (Program.isPlay)
									{
										Program.isNewGame = true;
									}
									break;
								case 1:
									showingMenu = false;
									break;
								case 2:
									Environment.Exit(0);
									break;
							}
							break;
					}
				}
				System.Threading.Thread.Sleep(10);
			}
		}
	}
}
