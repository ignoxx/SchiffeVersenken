using System;
using System.IO;
using System.Numerics;

namespace SchiffeVersenken
{
    public class GameField
    {
        enum FieldType
        {
            Water,
            Locked,
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
        
        private struct Position
        {
            public int x;
            public int y;
        }

        public GameField(int size = 10)
        {
            Size = size;
            _field = new FieldType[Size, Size];

            
        }

        public void PlaceShipToField(Ship ship, int amount)
        {
            /*
             * 1. Pick a random position(x,y) and check if there's enough space depending on ship.Size
             * 2. Use this position to place the ship
             * 3. Repeat step 1 & 2 "amount" of times
             */

            Position position;
            Direction direction;
            bool ship_placed = false;
            bool check_passed = false;
            
            for (int i = 0; i < amount; i++) 
            {
                // Find a free position
                do
                {
                    position = RandomizePosition();
                    direction = RandomizeDirection();

                    if (PlaceFree(position))
                    {
                        int count = 0;
                        Position starting_position = position;
                        for (int j = 0; j < ship.Size; j++)
                        {
                            if (direction == Direction.Horizontal)
                                position.x++;
                            else
                                position.y++;

                            if (PlaceFree(position))
                                count++;
                            else
                                break;
                        }

                        // Place the ship
                        if (count == ship.Size)
                        {
                            count = 0;
                            position = starting_position;
                            for (int j = 0; j < ship.Size; j++)
                            {
                                if (direction == Direction.Horizontal)
                                    position.x++;
                                else
                                    position.y++;

                                if (PlaceFree(position))
                                {
                                    _field[position.x, position.y] = FieldType.Ship;
                                    count++;
                                }
                                else
                                    break;
                            }

                            if (count == ship.Size)
                                ship_placed = true;
                        }
                    }
                } while (!ship_placed);
            }
        }

        private bool PlaceFree(Position position)
        {
            if (position.x < 0 || position.x > Size || position.y < 0 || position.y > Size)
                return false;
            
            return _field[position.x, position.y] == FieldType.Water;
        }

        private Direction RandomizeDirection()
        {
            Random random = new Random();
            return (Direction) random.Next(0, 2);
        }

        private Position RandomizePosition()
        {
            Random zufall = new Random();
            Position position = new Position();
            
            position.x = zufall.Next(0, Size-1);
            position.y = zufall.Next(0, Size-1);

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
                        case FieldType.Locked: //to be removed
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case FieldType.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case FieldType.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }

                    Console.Write('\u25A0' + " ");
                }
                Console.Write("\n");
            }
        }
    }
}