using System;
using System.Globalization;

namespace SchiffeVersenken
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Create gamefield, user specified size
             * 10 ships will be randomly placed onto the field
             * Print a blank gamefield (w/o ship positions)
             * Player has unlimited shots, shot by entering coordinates of the field
             * shot hits water = mark blue; hits ship = red;
             * all ships destroyed = won
             */

            GameField field = new GameField();
            Console.ReadKey();
        }
    }
}