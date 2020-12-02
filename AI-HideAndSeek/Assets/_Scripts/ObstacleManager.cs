using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;
    public GameObject Obstacle;

    private void Awake() {
        Instance = this;
    }
    public void ClearObstacle() {
        for (int i = 0; i < this.transform.childCount; ++i)
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
    }
    public void SpawnObstacle(int row, int column) {
        Instantiate(Obstacle, MapGenerator.Instance.GetPositionByRowColumn(row, column), Quaternion.identity, this.transform);
    }
}
