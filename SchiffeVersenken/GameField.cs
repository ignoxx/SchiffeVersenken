using System;
using System.Numerics;

namespace SchiffeVersenken
{
    public class GameField
    {
        enum FieldType
        {
            Water,
            Ship,
            Miss,
            Hit
        }

        enum Direction
        {
            Horizontal,
            Vertical
        }

        public int Size { get; }

        private FieldType[,] _field;

        public GameField(int size = 10)
        {
            Size = size;
            _field = new FieldType[Size, Size];

            GenerateField();
        }

        private void GenerateField()
        {
//            Random zufall = new Random();
//
//            for (int x = 0; x < _field.GetLength(0); x++)
//            for (int y = 0; y < _field.GetLength(1); y++)
//                _field[x, y] = (FieldType)zufall.Next(1, 4);
        }

        public bool PlaceShipToField(Ship ship, int amount)
        {
            Direction direction = RandomizeDirection();
            Vector2 position = RandomizePosition();

            return true;
        }

        private Direction RandomizeDirection()
        {
            Random random = new Random();
            return (Direction) random.Next(0, 2);
        }

        private Vector2 RandomizePosition()
        {
            Vector2 position = new Vector2();

            return position;
        }

        public void PrintField()
        {
            Console.WriteLine("(0,0) -> " + "(" + Size + ", " + Size + ")");

            for (int x = 0; x < _field.GetLength(0); x++)
            {
                for (int y = 0; y < _field.GetLength(1); y++)
                {
                    switch (_field[x, y])
                    {
                        case FieldType.Water:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case FieldType.Ship: //to be removed
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case FieldType.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case FieldType.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }

                    Console.Write('\u25A0' + " ");
                    Console.ResetColor();
                }

                Console.Write("\n");
            }
        }
    }
}