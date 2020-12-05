﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;
    public GameObject Cell, Player, Enemy, Wall;
    public Vector3 OriginalPosition, CellSize;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        CellSize = Cell.GetComponent<BoxCollider>().size;
    }
    public void GenerateMap(int row, int column, int[,] Matrix) {
        if (this.transform.childCount == 0) {
            InitState(row, column, Matrix);
        }
        else 
            NormalState(row, column, Matrix);
    }
    private void InitState(int row, int column, int[,] Matrix) {
        //Spawn cell of map
        Vector3 desiredPosition = OriginalPosition;
        for (int i = 0; i < row; ++i) 
        {
            for (int j = 0; j < column; ++j)
            {
                GameObject GO = Instantiate(Cell, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
                desiredPosition = new Vector3(CellSize.x * i, 0, CellSize.z * j);
                GO.transform.position = desiredPosition;
            }
        }

        //init info of matrix on map
        for (int i = 0; i < row; ++i) 
        {
            for (int j = 0; j < column; ++j) 
            {
                switch (Matrix[i, j]) 
                {
                    case 0:
                        break;
                    case 1:
                        ObstacleManager.Instance.SpawnObstacle(i, j, 1);
                        break;
                    case 2:
                        Player.GetComponent<Player>().InitRC(i, j);
                        break;
                    case 3:
                        Enemy.GetComponent<Enemy>().InitRC(i, j); 
                        break;
                    // case 4:
                    //     ObstacleManager.Instance.SpawnObstacle(i, j, 4);
                    //     break;
                }
            }
        }
    }
    private void NormalState(int row, int column, int[,] Matrix) {
        for (int i = 0; i < row; ++i) 
        {
            for (int j = 0; j < column; ++j) 
            {
                switch (Matrix[i, j]) 
                {
                    case 2:
                        Player.GetComponent<Player>().DecideMove(i, j);
                        break;
                    case 3:
                        Enemy.GetComponent<Enemy>().DecideMove(i, j); 
                        break;
                    // case 4:
                    //     ObstacleManager.Instance.SpawnObstacle(i, j);
                    //     break;
                }
            }
        }
    }
    public Cell GetCellByRowColumn(int row, int column) {
        return this.transform.GetChild(row * GameManager.Instance.Column + column).GetComponent<Cell>();
    }
    public Vector3 GetPositionByRowColumn(int row, int column) {
        Debug.Log(row.ToString() + '-' + column.ToString());
        int id = row * GameManager.Instance.Column + column;
        return this.transform.GetChild(id).transform.position;
    }
    public Vector3 GetCenterPosition() {
        Vector3 centerPosition;
        int idr = GameManager.Instance.Row - 1, idc = GameManager.Instance.Column - 1;
        Debug.Log("Row:" + idr / 2 + "_" + "Column:" + idc / 2);
        centerPosition = GetPositionByRowColumn(idr / 2, idc / 2);
        if (GameManager.Instance.Row % 2 == 0)
            centerPosition += new Vector3((float)CellSize.x / (float)2, 0, 0);
        if (GameManager.Instance.Column % 2 == 0)
            centerPosition += new Vector3(0, 0, (float)CellSize.z / (float)2);
        return centerPosition;
    } 
}

