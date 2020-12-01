using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;
    public GameObject Cell, Cam, Player, Enemy;
    public int row, column;
    public Vector3 OriginalPosition, CellSize;
    public int[,] Matrix = new int[100,100];
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        CellSize = Cell.GetComponent<BoxCollider>().size;
        GenerateMap();
    }
    private void Update() {
        
    }
    private void GenerateMap() {
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
        for (int i = 0; i < row; ++i) 
        {
            for (int j = 0; j < column; ++j) 
            {
                switch (Matrix[i, j]) 
                {
                    case 2:
                        Player.GetComponent<Player>().InitRC(i, j);
                        break;
                    case 3:
                        Enemy.GetComponent<Player>().InitRC(i, j); 
                        break;
                }
            }
        }
        // Player.GetComponent<Player>().InitRC(0, 0);
        // Enemy.GetComponent<Enemy>().InitRC(row - 1, column - 1);
    }
    public Vector3 GetPositionByRowColumn(int id_r, int id_c) {
        int id = id_r * column + id_c;
        return this.transform.GetChild(id).transform.position;
    }
    public Vector3 GetCenterPosition() {
        Vector3 centerPosition;
        int idr = row - 1, idc = column - 1;
        Debug.Log("Row:" + idr / 2 + "_" + "Column:" + idc / 2);
        centerPosition = GetPositionByRowColumn(idr / 2, idc / 2);
        if (row % 2 == 0)
            centerPosition += new Vector3((float)CellSize.x / (float)2, 0, 0);
        if (column % 2 == 0)
            centerPosition += new Vector3(0, 0, (float)CellSize.z / (float)2);
        return centerPosition;
    } 
}

