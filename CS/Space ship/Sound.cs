using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Ship
{
	static class Sound
	{
		static public void Hit()
		{
			Console.Beep(300, 200);
		}

		static public void Shoot()
		{
			Console.Beep(400, 100);
		}

		static public void Crash()
		{
			Console.Beep(300, 1000);
		}
	}
}
