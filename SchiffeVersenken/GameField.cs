using System;
using System.IO;
using System.Numerics;

namespace SchiffeVersenken
{
    public class GameField
    {
        public enum FieldType
        {
            Water,
            Locked,
            Ship0,
            Ship1,
            Ship2,
            Ship3,
            Miss,
            Hit
        }

        public enum Direction
        {
            Horizontal,
            Vertical
        }

        public int Size { get; }

        private FieldType[,] _field;
        
        public struct Position
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
                            if (PlaceFree(position))
                                count++;
                            else
                                break;
                            
                            if (direction == Direction.Horizontal)
                                position.x++;
                            else
                                position.y++;
                        }

                        // Place the ship
                        if (count == ship.Size)
                        {
                            count = 0;
                            position = starting_position;
                            for (int j = 0; j < ship.Size; j++)
                            {
                                if (PlaceFree(position))
                                {
                                    Console.WriteLine($"Placing {ship.Type} @ {position.x}|{position.y}");
                                    _field[position.x, position.y] = GetShipFieldType(ship);
                                    count++;
                                }
                                else
                                    break;
                                
                                if (direction == Direction.Horizontal)
                                    position.x++;
                                else
                                    position.y++;
                            }

                            if (count == ship.Size)
                            {
                                Console.WriteLine("--------");
                                ship_placed = true;
                            }
                                
                        }
                    }
                } while (!ship_placed);
            }
        }

        public FieldType GetShipFieldType(Ship ship)
        {
            switch (ship.Type)
            {
                case Ship.ShipType.Flagship:
                    return FieldType.Ship0;
                    break;
                
                case Ship.ShipType.Cruiser:
                    return FieldType.Ship1;
                    break;
                
                case Ship.ShipType.Destroyer:
                    return FieldType.Ship2;
                    break;
                
                case Ship.ShipType.Submarine:
                    return FieldType.Ship3;
                    break;
            }

            return FieldType.Miss;
        }

        public bool PlaceFree(Position position)
        {
            if (position.x < 0 || position.x > Size-1 || position.y < 0 || position.y > Size-1)
                return false;
            
            return _field[position.x, position.y] == FieldType.Water;
        }

        public Direction RandomizeDirection()
        {
            Random random = new Random();
            return (Direction) random.Next(0, 2);
        }

        public Position RandomizePosition()
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
                        
                        case FieldType.Ship0: //to be removed
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case FieldType.Ship1: //to be removed
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case FieldType.Ship2: //to be removed
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case FieldType.Ship3: //to be removed
                            Console.ForegroundColor = ConsoleColor.White;
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