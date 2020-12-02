using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Row, Column;
    public Vector2 Ping;
    public int[,] Matrix = new int[100,100];
    private void Awake() {
        Instance = this;
    }
    public void ApplyInfo(int _row, int _column, int[][] _matrix, int pingX, int pingY) {
        //Apply row + column
        Row = _row;
        Column = _column;

        Debug.Log(_matrix[0][2]);
        Matrix = new int[Row, Column];
        //Apply the matrix
        for (int i = 0; i < Row; ++i) 
            for (int j = 0; j < Column; ++j) 
                Matrix[i,j] = _matrix[i][j];

        //Apply the announce pos
        Ping = new Vector2(pingX, pingY);

        ObstacleManager.Instance.ClearObstacle();
        MapGenerator.Instance.GenerateMap(Row, Column, Matrix);
    }
    public void PingCell() {
        MapGenerator.Instance.GetCellByRowColumn((int)Ping.x, (int)Ping.y).Ping();
    }
}
