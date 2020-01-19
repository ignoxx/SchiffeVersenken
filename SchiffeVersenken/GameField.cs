using System;

namespace SchiffeVersenken
{
    public class GameField
    {
        public enum FieldType
        {
            Water,
            Ship,
            Miss,
            Hit
        }

        private enum Direction
        {
            Horizontal,
            Vertical
        }

        public int Size { get; }
        private readonly FieldType[,] _field;

        public struct Position
        {
            public int x;
            public int y;
            public override string ToString()
            {
                return $"{x},{y}";
            }
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
            
            for (var i = 0; i < amount; i++)
            {
                var shipPlaced = false;
                // Find a free position
                do
                {
                    var position = RandomizePosition();
                    var direction = RandomizeDirection();
                    if (PlaceFree(position))
                    {
                        var count = 0;
                        var startingPosition = position;
                        for (var j = 0; j < ship.Size; j++)
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
                            position = startingPosition;
                            for (var j = 0; j < ship.Size; j++)
                            {
                                if (PlaceFree(position))
                                {
                                    _field[position.x, position.y] = FieldType.Ship;
                                    count++;
                                }
                                else 
                                    break;

                                if (direction == Direction.Horizontal)
                                    position.x++;
                                else
                                    position.y++;
                            }

                            if (count == ship.Size) shipPlaced = true;
                        }
                    }
                } while (!shipPlaced);
            }
        }

        private bool PlaceFree(Position position)
        {
            if (position.x < 0 || position.x > Size - 1 || position.y < 0 || position.y > Size - 1) 
                return false;
            
            return _field[position.x, position.y] == FieldType.Water;
        }

        private Direction RandomizeDirection()
        {
            var random = new Random();
            return (Direction) random.Next(0, 2);
        }

        private Position RandomizePosition()
        {
            var ran = new Random();
            var position = new Position
            {
                x = ran.Next(0, Size - 1), 
                y = ran.Next(0, Size - 1)
            };
            
            return position;
        }

        public bool HasShips()
        {
            for (var x = 0; x < _field.GetLength(0); x++)
            for (var y = 0; y < _field.GetLength(1); y++)
                if (_field[x, y] == FieldType.Ship)
                    return true;
            
            return false;
        }

        public FieldType GetFieldTypeAtPos(Position position)
        {
            return _field[position.x, position.y];
        }

        public void MarkFieldAs(Position pos, FieldType type)
        {
            _field[pos.x, pos.y] = type;
        }

        public void PrintField()
        {
            Console.Clear();
            Console.WriteLine($"(0,0) -> ({Size - 1},{Size - 1})");
            for (var x = 0; x < _field.GetLength(0); x++)
            {
                for (var y = 0; y < _field.GetLength(1); y++)
                {
                    switch (_field[x, y])
                    {
                        case FieldType.Water:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        // case FieldType.Ship: //to be removed
                        //     Console.ForegroundColor = ConsoleColor.White;
                        //     break;
                        case FieldType.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case FieldType.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                    }

                    Console.Write('\u25A0' + " ");
                }

                Console.Write("\n");
            }

            Console.ResetColor();
        }
    }
}