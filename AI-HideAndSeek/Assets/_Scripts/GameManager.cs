using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player Player;
    public Enemy Enemy;
    public int Row, Column;
    public Vector2 Ping;
    public int[,] Matrix = new int[100,100];

    public enum Turn {Player, Enemy};
    public Turn CurrentTurn;
    private void Awake() {
        Instance = this;
    }
    public void ApplyInfo(int _row, int _column, int[][] _matrix, int pingX, int pingY) {

        //Disable Player + Enemy
        Player.DisableRender();
        Enemy.DisableRender();

        //Apply row + column
        Row = _row;
        Column = _column;

        Matrix = new int[Row, Column];
        //Apply the matrix
        for (int i = 0; i < Row; ++i) 
            for (int j = 0; j < Column; ++j) 
                Matrix[i,j] = _matrix[i][j];

        //Apply the announce pos
        Ping = new Vector2(pingX, pingY);

        //ObstacleManager.Instance.ClearObstacle();
        MapGenerator.Instance.GenerateMap(Row, Column, Matrix);
    }
    public void SwitchTurn() {
        StartCoroutine(HoldToSwitchTurn());
    }
    IEnumerator HoldToSwitchTurn()
    {
        yield return new WaitForSeconds(1);
        if (CurrentTurn == Turn.Player)
            CurrentTurn = Turn.Enemy;
        else    
            CurrentTurn = Turn.Player;
    }
    private void Update() {
        CheckFound();
    }
    public void CheckFound() {
        if (Player.Row == Enemy.Row && Player.Column == Enemy.Column)
        {
            Debug.Log("END");
            Player.EnableRender();
            Player.Die();
            Enemy.EnableRender();
            Enemy.Kill();
        }
    }
    public void PingCell() {
        MapGenerator.Instance.GetCellByRowColumn((int)Ping.x, (int)Ping.y).Ping();
    }
}
