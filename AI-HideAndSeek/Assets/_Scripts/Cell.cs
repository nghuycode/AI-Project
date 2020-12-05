using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material Normal, Red, Blue;
    public void Ping() {
        StartCoroutine(GoPing());
    }
    IEnumerator GoPing() {
        Material originalMat = this.GetComponent<MeshRenderer>().material;
        for (int i = 0; i < 3; ++i) {
            this.GetComponent<MeshRenderer>().material = Blue;
            yield return new WaitForSeconds(0.5f);
            this.GetComponent<MeshRenderer>().material = Normal;
            yield return new WaitForSeconds(0.5f);
        }
        this.GetComponent<MeshRenderer>().material = originalMat;
    }
    public void GetVision(int id)
    {
        if (id == 0)
            this.GetComponent<MeshRenderer>().material = Red;
        else if (id == 1)
            this.GetComponent<MeshRenderer>().material = Blue;
    }
    public void LostVision()
    {
        this.GetComponent<MeshRenderer>().sharedMaterial = Normal;
    }
}
