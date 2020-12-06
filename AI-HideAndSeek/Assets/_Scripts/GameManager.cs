using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ScoreText;
    public int score;
    public static GameManager Instance;
    public PlayerTeam Player;
    public Enemy Enemy;
    public int Row, Column, KillCount;
    public Vector2 Ping;
    public int[,] Matrix = new int[100,100];

    public enum Turn {NULL, Player, Enemy};
    public Turn CurrentTurn;
    public bool CanPlay;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        //RunPython();
    }
    public void ApplyInfo(int _row, int _column, int type, int[][] _matrix, Vector2[] pings) {

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


        //ObstacleManager.Instance.ClearObstacle();
        MapGenerator.Instance.GenerateMap(Row, Column, type, Matrix);

        //Apply the announce pos
        for (int i = 0; i < pings.Length; i += 2)
            PingCell((int)pings[i].x, (int)pings[i + 1].y);
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
        ScoreText.text = "Score: " + score.ToString();
        if (CanPlay)
            CheckFound();
    }
    public void CheckFound() {
        for (int i = 0; i < Player.GetComponent<PlayerTeam>().Players.Count; ++i)
        {
            Player playerTMP = Player.GetComponent<PlayerTeam>().Players[i];
            if (playerTMP.Row == Enemy.Row && playerTMP.Column == Enemy.Column && !playerTMP.IsDie)
            {
                Debug.Log("KILL");
                playerTMP.EnableRender();
                playerTMP.Die();
                Enemy.EnableRender();
                Enemy.Kill();
                KillCount++;
                score += 20;
                if (KillCount == Player.transform.childCount)
                    CanPlay = false;
            }
        }
    }
    public void PingCell(int row, int column) {
        //Debug.Log(row + " - " + column);
        if (column == -1 || row == -1) return;
        MapGenerator.Instance.GetCellByRowColumn(row, column).Ping();
    }
}
