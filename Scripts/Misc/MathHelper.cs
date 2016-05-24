using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Misc
{
	class MathHelper
	{
		public static int Scale(int input, int percent)
		{
			return (input * percent) / 100;
		}
	}
}
