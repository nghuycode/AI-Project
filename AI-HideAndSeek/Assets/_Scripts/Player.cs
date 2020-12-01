using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Row, RowToGo, Column, ColumnToGo;
    public MapGenerator MapGenerator;
    public Animator Anim;
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
