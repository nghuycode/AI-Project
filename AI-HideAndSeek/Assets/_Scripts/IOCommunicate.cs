using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class IOCommunicate : MonoBehaviour
{
    public string[] datas, mat, ping = new string[100];
    public int[][] matrix = new int[100][];

    public static IOCommunicate Instance;
    private void Awake() {
        Instance = this;
    }
    public void Save(string data) {

    }
    public void Load(string data) {
        //Read row and column
        int row, column;
        datas = File.ReadAllLines("Assets/data.txt");
        string[] rowcolumn = datas[0].Split(' ');
        row = int.Parse(rowcolumn[0]);
        column = int.Parse(rowcolumn[1]);

        //Read the matrix
        mat = new string[row];
        for (int i = 0; i < row; ++i) 
            mat[i] = datas[i + 1];
        matrix = mat
               .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
               .ToArray();
        
        Debug.Log(matrix[0][2]);
        //Read the ping position
        int pingX, pingY;
        ping = datas[row + 1].Split(' ');
        pingX = int.Parse(ping[0]);
        pingY = int.Parse(ping[1]);

        GameManager.Instance.ApplyInfo(row, column, matrix, pingX, pingY);
    }
}
