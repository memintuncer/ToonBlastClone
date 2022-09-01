using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    int MatrixIndexX;
    int MatrixIndexY;

    public Point(int x, int y)
    {
        this.MatrixIndexX = x;
        this.MatrixIndexY = y;
    }

    public int GetMatrixIndexX()
    {
        return MatrixIndexX;
    }
    public int GetMatrixIndexY()
    {
        return MatrixIndexY;
    }

}
