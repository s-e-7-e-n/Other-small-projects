using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
namespace Space_Ship
{

	class Program
	{
		static public int Score = 0;
		static public bool isPlay = false;
		static public bool isNewGame = false; 
		static private void MainLoop(bool Run)
		{
			isPlay = true;
			Background.CreateBackground();
			Console.CursorVisible = false;
			Ship ship = new Ship();
			List<AllObj> list_of_allobj = new List<AllObj>() { ship };
			List<Stone> StoneBox = new List<Stone>();
			List<Bullets> BulletBox = new List<Bullets>();
			System.Media.SoundPlayer music = new System.Media.SoundPlayer(Properties.Resources._8bitMusic);
			var rand = new Random();
			int increasingComplexity = 100;
			int timerComlexity = 0;
			int timerOfStones = 0;
			bool run = Run;
			music.PlayLooping();
			while (run)
			{
				Thread hitSound = new Thread(new ThreadStart(Sound.Hit))
				{
					IsBackground = true
				};
				Thread shootSound = new Thread(new ThreadStart(Sound.Shoot))
				{
					IsBackground = true
				};
				// столкновения камня с кораблём
				foreach (Stone stone in StoneBox)
				{
					if (stone.X + stone.view[0].Length >= ship.X & stone.X <= ship.X + ship.view[0].Length)
					{
						if (stone.Y + stone.view.Length > ship.Y)
						{
							ship.Death();
							Sound.Crash();
							StoneBox.Remove(stone);
							list_of_allobj.Remove(stone);
							Thread.Sleep(1000);
							if (ship.getLives() == 0)
							{
								run = false;
							}
							break;
						}
					}
				}
				// добавление камня
				if (timerOfStones++ == increasingComplexity)
				{
					if (timerComlexity++ == 2)
					{
						if(increasingComplexity > 0)
						{
							increasingComplexity--;
						}
						timerComlexity = 0;
					}
					int xCoord = rand.Next(2, Console.BufferWidth - 9);
					int yCoord = -3;
					Stone stone = new Stone(xCoord, yCoord);
					list_of_allobj.Add(stone);
					StoneBox.Add(stone);
					timerOfStones = 0;
				}
				// Столкновение пули и камня
				foreach (Bullets bullet in BulletBox)
				{
					foreach (Stone stone in StoneBox)
					{
						if (bullet.X >= stone.X & bullet.X <= stone.X + 6)
						{
							if (bullet.Y >= stone.Y & bullet.Y <= stone.Y + 3)
							{
								if (hitSound.IsAlive == false)
								{
									hitSound.Start();
								}
								//hitSound.Start();
								bullet.isDraw = false;
								stone.isDraw = false;
								Score++;
							}
						}
					}
				}
				// движение камней 
				foreach (AllObj obj in list_of_allobj)
				{
					if (obj is Stone stone)
					{
						if (timerOfStones % stone.Velocity == 0)
						{
							stone.Y += 1;
						}
					}
				}
				// удаление ненужных объектов
				for (int j = 0; j < list_of_allobj.Count; j++)
				{
					if (list_of_allobj[j].isDraw == false)
					{
						if (list_of_allobj[j] is Stone stone)
						{
							StoneBox.Remove(stone);
						}
						if (list_of_allobj[j] is Bullets bullet)
						{
							BulletBox.Remove(bullet);
						}
						list_of_allobj.RemoveAt(j);
					}
				}
				// проверка действий
				if (Console.KeyAvailable == true)
				{
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.Escape:
							MainMenu.Choice = 1;
							MainMenu.ShowMenu();
							run = !isNewGame;
							break;
						case ConsoleKey.LeftArrow:
							if (ship.X > 2)
							{
								ship.X -= 2;
							}
							break;
						case ConsoleKey.RightArrow:
							if (ship.X < Console.BufferWidth - ship.view.Length - 10)
							{
								ship.X += 2;
							}
							break;
						case ConsoleKey.Spacebar:
							shootSound.Start();
							Bullets bullet = new Bullets(ship.X + 7, ship.Y);
							BulletBox.Add(bullet);
							list_of_allobj.Add(bullet);
							break;
					}
				}
				Drawing.Draw(list_of_allobj); // Отрисовка
				// если счёт <0 то конец игры
				if (Score < 0)
				{
					run = false;
					break;
				}
				if (isNewGame)
				{
					Console.Clear();
				}
				System.Threading.Thread.Sleep(17);
			}
			music.Stop();
		}

		static void Main(string[] args)
		{
			System.Media.SoundPlayer GO = new System.Media.SoundPlayer(Properties.Resources.GameOver);
			bool loop = true;
			while (loop)
			{
				Console.OutputEncoding = Encoding.Unicode;
				Console.Title = "Space Ship";
				Console.CursorVisible = false;
				Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
				Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
				MainMenu.ShowMenu();
				newgame:
				MainLoop(loop); // цикл игры
				isPlay = false;
				Score = 0;
				if (isNewGame)
				{
					isNewGame = false;
					goto newgame;
				}
				GO.PlayLooping();
				Thread.Sleep(50);
				Console.Clear();
				TitleBox.Animation_GO();
				Console.ReadKey();
				GO.Stop();
			}
		}
	}
}
