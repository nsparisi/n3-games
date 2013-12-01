using UnityEngine;
using System.Collections;

public class HoleCollider : MonoBehaviour 
{	
    int enabledLayer;
    int disabledLayer;

    void Start()
    {
        disabledLayer = this.gameObject.layer;
        enabledLayer = this.gameObject.layer | LayerMask.NameToLayer("HoleCollider");

        ActivateHoleCollisions();
    }

    public void ActivateHoleCollisions()
    {
        if (this.gameObject.layer != enabledLayer)
        {
            this.gameObject.layer = enabledLayer;
        }
    }

    public void DeactivateHoleCollisions()
    {
        if (this.gameObject.layer != disabledLayer)
        {
            this.gameObject.layer = disabledLayer;
        }
    }
}