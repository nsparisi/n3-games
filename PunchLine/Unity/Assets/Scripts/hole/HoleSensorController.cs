using UnityEngine;
using System.Collections;

public class HoleSensorController : MonoBehaviour {
    
    public bool UpTouching
    {
        get
        {
            return up.IsTouching;
        }
    }
    
    public bool DownTouching
    {
        get
        {
            return down.IsTouching;
        }
    }
    
    public bool LeftTouching
    {
        get
        {
            return left.IsTouching;
        }
    }
    
    public bool RightTouching
    {
        get
        {
            return right.IsTouching;
        }
    }
    
    public bool CenterTouching
    {
        get
        {
            return center.IsTouching;
        }
    }

    HoleSensor up;
    HoleSensor down;
    HoleSensor left;
    HoleSensor right;
    HoleSensor center;

    void Start()
    {
        up = this.transform.FindChild("Up").GetComponent<HoleSensor>();
        down = this.transform.FindChild("Down").GetComponent<HoleSensor>();
        left = this.transform.FindChild("Left").GetComponent<HoleSensor>();
        right = this.transform.FindChild("Right").GetComponent<HoleSensor>();
        center = this.transform.FindChild("Center").GetComponent<HoleSensor>();
    }
}
