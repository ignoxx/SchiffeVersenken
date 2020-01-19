using System;

namespace SchiffeVersenken
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*
             * Create gamefield, user specified size
             * 10 ships will be randomly placed onto the field
             * Print a blank gamefield (w/o ship positions)
             * Player has unlimited shots, shot by entering coordinates of the field
             * shot hits water = mark yellow; hits ship = red; default color = blue;
             * all ships destroyed = won
             */
            
            var field = new GameField();
            field.PlaceShipToField(new Ship(Ship.Type.Flagship), 1);
            field.PlaceShipToField(new Ship(Ship.Type.Cruiser), 2);
            field.PlaceShipToField(new Ship(Ship.Type.Destroyer), 3);
            field.PlaceShipToField(new Ship(Ship.Type.Submarine), 4);
            field.PrintField();
            do
            {
                Console.WriteLine("Try to hit all ships to win!");
                Console.Write("Enter x,y coordinates (0-9): ");
                var coordinates = Console.ReadLine().Split(',');
                var position = new GameField.Position
                {
                    x = Math.Clamp(int.Parse(coordinates[1]), 0, 9), 
                    y = Math.Clamp(int.Parse(coordinates[0]), 0, 9)
                };
                switch (field.GetFieldTypeAtPos(position))
                {
                    case GameField.FieldType.Water:
                        field.MarkFieldAs(position, GameField.FieldType.Miss);
                        break;
                    
                    case GameField.FieldType.Ship: //to be removed
                        field.MarkFieldAs(position, GameField.FieldType.Hit);
                        break;
                }

                field.PrintField();
            } while (field.HasShips());

            Console.WriteLine("Well done, you won!");
        }
    }
}