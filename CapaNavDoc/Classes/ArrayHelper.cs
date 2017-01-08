using System;

namespace CapaNavDoc.Classes
{
    public class ArrayHelper
    {
        public static bool[][] GetBoolArray(int rows, int cols)
        {
            bool[][] array = new bool[rows][];
            for (int i = 0; i < rows; i++) array[i] = new bool[cols];
            return array;
        }

        public static bool[][] GetInitialized(int rows, int cols, Func<int, int, bool> getValue)
        {
            bool[][] array = new bool[rows][];
            for (int i = 0; i < rows; i++)
            {
                array[i] = new bool[cols];
                for (int j = 0; j < cols; j++)
                {
                    array[i][j] = getValue(i, j);
                }
            }
            return array;

        }
    }
}