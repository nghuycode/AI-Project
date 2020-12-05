using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerTeam Player;
    public Enemy Enemy;
    public int Row, Column;
    public Vector2 Ping;
    public int[,] Matrix = new int[100,100];

    public enum Turn {NULL, Player, Enemy};
    public Turn CurrentTurn;
    private void Awake() {
        Instance = this;
    }
    public void ApplyInfo(int _row, int _column, int type, int[][] _matrix, int pingX, int pingY) {

        //Disable Player + Enemy
        Player.DisableAllMember();
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
        PingCell();

        //ObstacleManager.Instance.ClearObstacle();
        MapGenerator.Instance.GenerateMap(Row, Column, type, Matrix);
    }
    public void SwitchTurn() {
        if (CurrentTurn == Turn.NULL)
            CurrentTurn = Turn.Enemy;
        else
        if (CurrentTurn == Turn.Player)
            CurrentTurn = Turn.Enemy;
        else    
            CurrentTurn = Turn.Player;
        //StartCoroutine(HoldToSwitchTurn());
    }
    IEnumerator HoldToSwitchTurn()
    {
        yield return new WaitForSeconds(1); 
    }
    private void Update() {
        CheckFound();
    }
    public void CheckFound() {
        for (int i = 0; i < Player.GetComponent<PlayerTeam>().Players.Count; ++i)
        {
            Player playerTMP = Player.GetComponent<PlayerTeam>().Players[i];
            if (playerTMP.Row == Enemy.Row && playerTMP.Column == Enemy.Column)
            {
                Debug.Log("END");
                playerTMP.EnableRender();
                playerTMP.Die();
                Enemy.EnableRender();
                Enemy.Kill();
            }
        }
    }
    public void PingCell() {
        if (Ping.x == -1 || Ping.y == -1) return;
        MapGenerator.Instance.GetCellByRowColumn((int)Ping.x, (int)Ping.y).Ping();
    }
}
