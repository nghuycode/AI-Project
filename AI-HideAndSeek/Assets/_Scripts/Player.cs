using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Row, RowToGo, Column, ColumnToGo;
    public MapGenerator MapGenerator;
    public Animator Anim;
    public enum Direction {
        LEFT, RIGHT, UP, DOWN, LEFTUP, LEFTDOWN, RIGHTUP, RIGHTDOWN
    }
    public void InitRC(int r, int c) {
        Row = r;
        Column = c;
        this.transform.position = MapGenerator.GetPositionByRowColumn(Row, Column);
    }
    private void Update() {
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(Row, --Column)));
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(Row, ++Column)));
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            this.transform.eulerAngles = new Vector3(0, -90, 0);
            StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, Column)));
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            this.transform.eulerAngles = new Vector3(0, 90, 0);
            StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(++Row, Column)));
        }
    }
    public void DecideMove(int newRow, int newColumn) 
    {
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
        if (newRow == Row) 
        {   
            if (newColumn > Column)
                Move(Direction.RIGHT);
            else    
                Move(Direction.LEFT);
        }
        else if (newColumn != Column) 
        {
            if (newRow > Row)
                Move(Direction.DOWN);
            else
                Move(Direction.UP);
        }
    }
    public void Move(Direction dir) {
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
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, Column)));
                break;
            case Direction.LEFTUP:
                this.transform.eulerAngles = new Vector3(0, -135, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, Column)));
                break;
            case Direction.RIGHTDOWN:
                this.transform.eulerAngles = new Vector3(0, 45, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, Column)));
                break;
            case Direction.RIGHTUP:
                this.transform.eulerAngles = new Vector3(0, -45, 0);
                StartCoroutine(Move(MapGenerator.GetPositionByRowColumn(--Row, Column)));
                break;
        }
    }
    private IEnumerator Move(Vector3 targetPosition) {
        Debug.Log("walk");
        Anim.SetBool("IsWalking", true);
        while (Mathf.Abs(this.transform.position.x - targetPosition.x) > 0.3f || Mathf.Abs(this.transform.position.z - targetPosition.z) > 0.3f) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, 0.02f);
            Debug.Log("walk");
            yield return new WaitForSeconds(0.02f);
        }
        this.transform.position = targetPosition;
        Anim.SetBool("IsWalking", false);
    }
}
