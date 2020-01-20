using System;
using System.Collections.Generic;

namespace SchiffeVersenken
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var field = new GameField();
            field.PlaceShipToField(new Ship(Ship.Type.Flagship), 1);
            field.PlaceShipToField(new Ship(Ship.Type.Cruiser), 2);
            field.PlaceShipToField(new Ship(Ship.Type.Destroyer), 3);
            field.PlaceShipToField(new Ship(Ship.Type.Submarine), 4);
            field.PrintField();

            var history = new List<string>();
            var historyVisibleLength = 5;

            // Save users statistics of missed and hit shots
            var miss = 0;
            var hits = 0;

            do
            {
                Console.Write("Enter x,y coordinates (0-9): ");
                GameField.Position position;

                try
                {
                    var coordinates = Console.ReadLine().Split(',');
                    position = new GameField.Position
                    {
                        x = Math.Clamp(int.Parse(coordinates[1]), 0, field.Size - 1),
                        y = Math.Clamp(int.Parse(coordinates[0]), 0, field.Size - 1)
                    };
                }
                catch (Exception e)
                {
                    position = new GameField.Position
                    {
                        x = 0,
                        y = 0
                    };
                }

                var historyString = $"Shot on ({position.y}|{position.x}), it was a ";

                switch (field.GetFieldTypeAtPos(position))
                {
                    case GameField.FieldType.Water:
                        field.MarkFieldAs(position, GameField.FieldType.Miss);
                        historyString += "miss..";
                        miss++;
                        break;

                    case GameField.FieldType.Ship: //to be removed
                        field.MarkFieldAs(position, GameField.FieldType.Hit);
                        historyString += "hit!!";
                        hits++;
                        break;
                    case GameField.FieldType.Miss:
                    case GameField.FieldType.Hit:
                        historyString += "bad shot.. you targeted it already..";
                        miss++;
                        break;
                }

                history.Add(historyString);
                if (history.Count > historyVisibleLength) history.RemoveAt(0);

                field.PrintField();
                Console.WriteLine($"Hits: {hits} | Hits left: {field.NumOfShips()} | Miss: {miss}");
                if (history.Count > 0)
                {
                    Console.WriteLine("----------------");
                    foreach (var entry in history) Console.WriteLine(entry);

                    Console.WriteLine("----------------");
                }
            } while (field.HasShips());

            Console.WriteLine("Well done, you won!");
        }
    }
}