using System;

namespace SchiffeVersenken
{
    public class GameField
    {
        public int Size { get;}

        private int[,] _field;
        public GameField(int size = 10)
        {
            Size = size;
            _field = new int[Size, Size];
            
            GenerateField();
        }

        private void GenerateField()
        {
            for (int i = 0; i < Size; i++)
            {
                Console.WriteLine(_field[i,i]);
            }
        }
    }
}