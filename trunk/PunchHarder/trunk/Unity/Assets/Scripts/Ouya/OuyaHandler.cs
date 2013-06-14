using UnityEngine;
using System.Collections;

public class OuyaHandler : MonoBehaviour, OuyaSDK.IPauseListener, OuyaSDK.IResumeListener
{
    void Awake()
    {
        OuyaSDK.registerPauseListener(this);
        OuyaSDK.registerResumeListener(this);

        OuyaInputManager.OuyaButtonEvent.addButtonEventListener(HandleButtonEvent);
    }

    void OnDestroy()
    {
        OuyaSDK.unregisterPauseListener(this);
        OuyaSDK.unregisterResumeListener(this);

        OuyaInputManager.OuyaButtonEvent.removeButtonEventListener(HandleButtonEvent);
        OuyaInputManager.initKeyStates();
    }

    public void OuyaOnPause()
    {
        throw new System.NotImplementedException();
    }

    public void OuyaOnResume()
    {
        throw new System.NotImplementedException();
    }

    private void HandleButtonEvent(OuyaSDK.OuyaPlayer p, OuyaSDK.KeyEnum b, OuyaSDK.InputAction bs)
    {
        if (b.Equals(OuyaSDK.KeyEnum.BUTTON_O) && bs.Equals(OuyaSDK.InputAction.KeyDown))
        {
            //we cna use this if we want event-based inputs instead
        }
    }
}
