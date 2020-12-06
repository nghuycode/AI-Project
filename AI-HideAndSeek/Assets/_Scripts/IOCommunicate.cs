using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class IOCommunicate : MonoBehaviour
{
    public string[] datas, mat = new string[100];
    public int[][] matrix = new int[100][];
    public string[] listPing;

    public static IOCommunicate Instance;
    private void Awake() {
        Instance = this;
    }
    public void Save(string data) {

    }
    public void Load(string data) {
        //Read row and column
        int row, column;
        //datas = File.ReadAllLines("Assets/data.txt");
        datas = data.Split('\n');
        string[] rowcolumn = datas[0].Split(' ');
        row = int.Parse(rowcolumn[0]);
        column = int.Parse(rowcolumn[1]);

        //read type of agent
        int type = int.Parse(datas[1]);

        //Read the matrix
        mat = new string[row];
        for (int i = 0; i < row; ++i) 
            mat[i] = datas[i + 2];
        matrix = mat
               .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
               .ToArray();
        
        //Read the ping position
        if (datas[row + 2][datas[row + 2].Length - 1] == ' ')
            datas[row + 2] = datas[row + 2].Remove(datas[row + 2].Length - 1);
        listPing = datas[row + 2].Split(' ');
        Vector2[] pings;
        if (type == -1)
        {
            pings = new Vector2[2];
            pings[0].x = -1;
            pings[1].y = -1;
        }
        else if (type == 0) {
            pings = new Vector2[listPing.Length];
            for (int i = 0; i < listPing.Length; ++i)
                if (i % 2 == 0)
                    pings[i].x = int.Parse(listPing[i]);
                else    
                    pings[i].y = int.Parse(listPing[i]);
        }
        else {
            pings = new Vector2[2];
            pings[0].x = int.Parse(listPing[0]);
            pings[1].y = int.Parse(listPing[1]);
        }

        GameManager.Instance.ApplyInfo(row, column, type, matrix, pings);
    }
}
