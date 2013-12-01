using UnityEngine;
using System.Collections;

public class HoleSensor : MonoBehaviour {

    public bool IsTouching { get; private set; }

    bool waitAFrame;

    void FixedUpdate()
    {
        if (!waitAFrame)
        {
            IsTouching = false;
        } 
        waitAFrame = false;
    }

    void OnTriggerStay (Collider other)
    {
        IsTouching = true;
        waitAFrame = true;
    }
}
