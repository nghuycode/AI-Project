using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Cell, Cam;
    public int row, column;
    public Vector3 OriginalPosition, CellSize;
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
        Cam.transform.position = GetCenterPosition() + new Vector3(0, 10, 0);
    }
    public int GetIDByRowColumn(int id_r, int id_c) {
        return id_r * column + id_c;
    }
    public Vector3 GetCenterPosition() {
        Vector3 centerPosition;
        int idr = row - 1, idc = column - 1;
        int centerID = GetIDByRowColumn(idr / 2, idc / 2);
        Debug.Log("Row:" + idr / 2 + "_" + "Column:" + idc / 2);
        centerPosition = this.transform.GetChild(centerID).transform.position;
        if (row % 2 == 0)
            centerPosition += new Vector3((float)CellSize.x / (float)2, 0, 0);
        if (column % 2 == 0)
            centerPosition += new Vector3(0, 0, (float)CellSize.z / (float)2);
        return centerPosition;
    } 
}

