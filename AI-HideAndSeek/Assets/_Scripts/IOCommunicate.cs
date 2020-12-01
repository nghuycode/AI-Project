using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class IOCommunicate : MonoBehaviour
{
    public string[] datas = new string[1000];
    public int[][] matrix = new int[100][];
    public static IOCommunicate Instance;
    private void Awake() {
        Instance = this;
    }
    public void Save(string data) {

    }
    public void Load(string data) {
        matrix = File.ReadAllLines("Assets/data.txt")
               .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
               .ToArray();
        Debug.Log(matrix[1][2]);
    }
}
