namespace SchiffeVersenken
{
    public class Ship
    {
        public enum ShipType
        {
            Flagship,
            Cruiser,
            Destroyer,
            Submarine
        }
        public int Size { get; }
        public ShipType Type { get; }

        public Ship(int size, ShipType type)
        {
            Size = size;
            Type = type;
        }
    }
}