using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    
    public delegate void OnFinish();
    public event OnFinish finishEvent;


    private void OnTriggerEnter(Collider other) {
        print("Collision enter");
        finishEvent?.Invoke();
    }
}
