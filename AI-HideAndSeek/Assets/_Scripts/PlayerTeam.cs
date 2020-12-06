using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public GameObject Player;
    public List<Player> Players = new List<Player>();
    public void EnableIndicator(int id)
    {
        Debug.Log(id);
        Players[id - 1].Indicator.SetActive(true);
    }
    public void DisableIndicator()
    {
        for (int i = 0; i < Players.Count; ++i)
        {
            Players[i].Indicator.SetActive(false);
        }
    }
    public void TeamDecideMove(int id, int row, int column)
    {
        if (id == 0)
        {
            for (int i = 0; i < Players.Count; ++i)
                if (Players[i].Row == row && Players[i].Column == column)
                {
                    Players[i].EnableRender();
                    MapGenerator.Instance.GetCellByRowColumn(row, column).GetVision(0);
                    return;
                }
        }
        else if (id - 1 < Players.Count) {
            //MapGenerator.Instance.GetCellByRowColumn(row, column).GetVision(1);
            for (int i = 0; i < Players.Count; ++i)
                if (Players[i].Row == row && Players[i].Column == column) {
                    Players[i].EnableRender();
                    return;
                }
            Players[id - 1].DecideMove(row, column);
        }
    }
    public void InitAMember(int row, int column)
    {
        GameObject GO = Instantiate(Player, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
        GO.GetComponent<Player>().InitRC(row, column);
        Players.Add(GO.GetComponent<Player>());
    }
    public void DisableAllMember()
    {
        for (int i = 0; i < Players.Count; ++i)
        {
            //Players[i].DisableRender();
        }
    }
}