using System;
using System.Collections.Generic;
using System.Linq;

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

            var history = new List<string>();
            var historyVisibleLength = 5;
            string historyString;
            do
            {
                Console.WriteLine("Try to hit all ships to win!");

                Console.Write("Enter x,y coordinates (0-9): ");
                string[] coordinates;
                GameField.Position position;
                
                try
                {
                    coordinates = Console.ReadLine().Split(',');
                    position = new GameField.Position
                    {
                        x = Math.Clamp(int.Parse(coordinates[1]), 0, field.Size-1), 
                        y = Math.Clamp(int.Parse(coordinates[0]), 0, field.Size-1)
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Example: 2,5");
                    continue;
                }
                
                historyString = $"Shot on ({position.x}|{position.y}), it was a ";
                
                switch (field.GetFieldTypeAtPos(position))
                {
                    case GameField.FieldType.Water:
                        field.MarkFieldAs(position, GameField.FieldType.Miss);
                        historyString += "miss..";
                        break;
                    
                    case GameField.FieldType.Ship: //to be removed
                        field.MarkFieldAs(position, GameField.FieldType.Hit);
                        historyString += "hit!!";
                        break;
                    case GameField.FieldType.Miss:
                    case GameField.FieldType.Hit:
                        historyString += "bad shot.. you targeted it already..";
                        break;
                }

                history.Add(historyString);
                field.PrintField();
                if (history.Count > 0)
                {
                    Console.WriteLine("----------------");
                    // for (int i = historyVisibleLength; i > 0; i--)
                    // {
                    //     Console.WriteLine(history[i]);
                    // }
                    foreach (var entry in history)
                    {
                        Console.WriteLine(entry);
                    }
                    Console.WriteLine("----------------");
                }
            } while (field.HasShips());

            Console.WriteLine("Well done, you won!");
        }
    }
}