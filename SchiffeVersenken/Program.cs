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
             * shot hits water = mark yellow; hits ship = red; default color = blue;
             * all ships destroyed = won
             */

            GameField field = new GameField();

            field.PlaceShipToField(new Ship(5, Ship.ShipType.Flagship), 1);
            field.PlaceShipToField(new Ship(4, Ship.ShipType.Cruiser), 2);
            field.PlaceShipToField(new Ship(3, Ship.ShipType.Destroyer), 3);
            field.PlaceShipToField(new Ship(2, Ship.ShipType.Submarine), 4);
            

            field.PrintField();
        }
    }
}