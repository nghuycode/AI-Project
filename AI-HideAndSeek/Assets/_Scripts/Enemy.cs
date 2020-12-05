using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Row, RowToGo, Column, ColumnToGo;
    public GameObject Render;
    public MapGenerator MapGenerator;
    public Animator Anim;
    public enum Direction {
        LEFT, RIGHT, UP, DOWN, LEFTUP, LEFTDOWN, RIGHTUP, RIGHTDOWN
    }
    public void InitRC(int r, int c) {
        EnableRender();
        Row = r;
        Column = c;
        this.transform.position = MapGenerator.GetPositionByRowColumn(Row, Column);
    }
    public void EnableRender() {
        Render.SetActive(true);
    }
    public void DisableRender() {
        Render.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyUp(KeyCode.A)) {
            Move(Direction.LEFT);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            Move(Direction.RIGHT);
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            Move(Direction.UP);
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            Move(Direction.DOWN);
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
            Move(Direction.LEFTUP);
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            Move(Direction.RIGHTUP);
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            Move(Direction.LEFTDOWN);
        }
        if (Input.GetKeyUp(KeyCode.C)) {
            Move(Direction.RIGHTDOWN);
        }
    }
    public void DecideMove(int newRow, int newColumn) 
    {
        // Debug.Log("New Row: " + newRow);
        // Debug.Log("New Column" + newColumn);
        EnableRender();
        if (newRow != Row && newColumn != Column) 
        {
            if (newRow > Row && newColumn > Column)
                Move(Direction.RIGHTDOWN);
            else if (newRow > Row && newColumn < Column)
                Move(Direction.LEFTDOWN);
            else if (newRow < Row && newColumn > Column)
                Move(Direction.RIGHTUP);
            else 
                Move(Direction.LEFTUP);
        }
        else
        if (newRow == Row) 
        {   
            if (newColumn > Column)
                Move(Direction.RIGHT);
            else if (newColumn < Column)
                Move(Direction.LEFT);
        }
        else if (newColumn != Column) 
        {
            if (newRow > Row)
                Move(Direction.DOWN);
            else if (newRow < Row)
                Move(Direction.UP);
        }
    }
    public void Move(Direction dir) {
        // if (GameManager.Instance.CurrentTurn != GameManager.Turn.Enemy)
        //     return;
        Debug.Log(this.gameObject.name + " : " + dir);
        GameManager.Instance.SwitchTurn();
        switch (dir) {
            case Direction.LEFT:
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(Row, --Column)));
                break;
            case Direction.RIGHT:
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(Row, ++Column)));
                break;
            case Direction.UP:
                this.transform.eulerAngles = new Vector3(0, -90, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, Column)));
                break;
            case Direction.DOWN:
                this.transform.eulerAngles = new Vector3(0, 90, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(++Row, Column)));
                break;
            case Direction.LEFTDOWN:
                this.transform.eulerAngles = new Vector3(0, 135, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(++Row, --Column)));
                break;
            case Direction.LEFTUP:
                this.transform.eulerAngles = new Vector3(0, -135, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, --Column)));
                break;
            case Direction.RIGHTDOWN:
                this.transform.eulerAngles = new Vector3(0, 45, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(++Row, ++Column)));
                break;
            case Direction.RIGHTUP:
                this.transform.eulerAngles = new Vector3(0, -45, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, ++Column)));
                break;
        }
    }
    public void Kill() 
    {
        //this.GetComponent<Animator>().SetTrigger("Kill");
    }
    private IEnumerator Move(Vector3 targetPosition) {
        Anim.SetBool("IsWalking", true);
        while (Mathf.Abs(this.transform.position.x - targetPosition.x) > 0.3f || Mathf.Abs(this.transform.position.z - targetPosition.z) > 0.3f) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        this.transform.position = targetPosition;
        Anim.SetBool("IsWalking", false);
    }
}
