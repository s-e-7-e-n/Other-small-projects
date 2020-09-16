using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Space_Ship
{
	static class TitleBox
	{
		static readonly public string[] GameName = {
			" $$  $$$   $$   $$  $$$$    $$  $  $ $$$ $$$",
			"$  $ $  $ $  $ $  $ $      $  $ $  $  $  $  $",
			" $   $$$  $$$$ $    $$$     $   $$$$  $  $$$",
			"  $  $    $  $ $    $        $  $  $  $  $",
			"$  $ $    $  $ $  $ $      $  $ $  $  $  $",
			" $$  $    $  $  $$  $$$$    $$  $  $ $$$ $",
		};
		static readonly public string[] GameOver = {
			" GGGG     AA    MM   MM  EEEEEE  !!!",
			"GG  GG   AAAA   MM   MM  EE      !!!",
			"GG  GG  AA  AA  MMM MMM  EE      !!!",
			"GG      AA  AA  MM M MM  EE      !!!",
			"GG      AA  AA  MM M MM  EEEEE   !!!",
			"GG GGG  AAAAAA  MM M MM  EE      !!!",
			"GG  GG  AA  AA  MM   MM  EE      !!!",
			"GG  GG  AA  AA  MM   MM  EE      !!!",
			" GGGGG  AA  AA  MM   MM  EEEEEE  !!!",
			"                                 !!!",
			" OOOO   VV  VV  EEEEEE   RRRRR   !!!",
			"OO  OO  VV  VV  EE       RR  RR  !!!",
			"OO  OO  VV  VV  EE       RR  RR  !!!",
			"OO  OO  VV  VV  EE       RR  RR  !!!",
			"OO  OO  VV  VV  EEEEE    RRRRR   !!!",
			"OO  OO  VV  VV  EE       RR RR   !!!",
			"OO  OO  VV  VV  EE       RR  RR",
			"OO  OO   VVVV   EE       RR  RR  !!!",
			" OOOO     VV    EEEEEE   RR  RR  !!!"
		};

		static public void Animation_GO()
		{
			int Xpos = Console.BufferWidth / 2 - TitleBox.GameOver[0].Length / 2;
			int Ypos = Console.BufferHeight / 2 - TitleBox.GameOver.Length / 2;
			for (int i = 1; i <= 6; i++)
			{
				Drawing.DrawObject(TitleBox.GameOver, Xpos, Ypos);
				Thread.Sleep(60);
				Console.Clear();
				Thread.Sleep(60);
			}
			string[] YARIC = { "Ярик - ЛОХ!" };
			string[] info = { "Нажмите любую клавишу" };
			Drawing.DrawObject(TitleBox.GameOver, Xpos, Ypos);
			Drawing.DrawObject(YARIC, Console.BufferWidth / 2 - YARIC[0].Length / 2, Ypos + 20);
			Thread.Sleep(200);
			Drawing.DrawObject(info, Console.BufferWidth / 2 - info[0].Length/2, Ypos + 20);
		}
	}
}
