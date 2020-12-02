using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Animator Anim;
    private void Awake() {
        Anim = this.GetComponent<Animator>();
    }
    public void Ping() {
        Anim.SetTrigger("Ping");
    }
}
