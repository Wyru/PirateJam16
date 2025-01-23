using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldUtils
{

    public static int[] TileNeighbors(int index, int radius, int width)
    {
        int x = TwoDCoordinatesX(index, width);
        int z = TwoDCoordinatesZ(index, width);

        List<int> neighbors = new List<int>();

        IEnumerable<int> xRange = Enumerable.Range(x - radius, 2 * radius + 1);
        IEnumerable<int> zRange = Enumerable.Range(z - radius, 2 * radius + 1);


        foreach (int xC in xRange)
        {
            foreach (int zC in zRange)
            {
                int linearPosition = LinearPosition(xC, zC, width);
                if (linearPosition < 0) continue;
                neighbors.Add(linearPosition);
            }
        }

        return neighbors.ToArray();
    }


    // -1 means the passed coordinates are invalid
    public static int LinearPosition(int x, int y, int width)
    {
        return Mathf.FloorToInt(x * width) + y;
    }

    public static int TwoDCoordinatesZ(int index, int width)
    {
        return index % width;
    }
    public static int TwoDCoordinatesX(int index, int width)
    {
        return index / (width - 1);
    }
}
