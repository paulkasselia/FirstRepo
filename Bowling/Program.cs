﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Only integer input allowed, characters exits game.");

            Game game = new Game();

            game.GameLoop();

        }
    }
}
