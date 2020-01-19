using System.Collections.Generic;

namespace SchiffeVersenken
{
    public class Ship
    {
        public enum Type
        {
            Flagship,
            Cruiser,
            Destroyer,
            Submarine
        }

        private readonly Dictionary<Type, int> _sizes = new Dictionary<Type, int>
        {
            {Type.Flagship, 5}, {Type.Cruiser, 4}, {Type.Destroyer, 3}, {Type.Submarine, 2}
        };

        public int Size { get; }

        public Ship(Type type)
        {
            Size = _sizes[type];
        }
    }
}