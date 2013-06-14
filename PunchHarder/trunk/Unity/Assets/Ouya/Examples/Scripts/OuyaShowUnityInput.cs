using System;
using UnityEngine;

public class OuyaShowUnityInput : MonoBehaviour,
    OuyaSDK.IPauseListener, OuyaSDK.IResumeListener,
    OuyaSDK.IMenuAppearingListener
{
    /// <summary>
    /// The current selected controller
    /// </summary>
    private OuyaSDK.OuyaPlayer m_player = OuyaSDK.OuyaPlayer.player1;

    #region Model reference fields

    public Material ControllerMaterial;

    public MeshRenderer RendererAxisLeft = null;
    public MeshRenderer RendererAxisRight = null;
    public MeshRenderer RendererButtonA = null;
    public MeshRenderer RendererButtonO = null;
    public MeshRenderer RendererButtonU = null;
    public MeshRenderer RendererButtonY = null;
    public MeshRenderer RendererDpadDown = null;
    public MeshRenderer RendererDpadLeft = null;
    public MeshRenderer RendererDpadRight = null;
    public MeshRenderer RendererDpadUp = null;
    public MeshRenderer RendererLB = null;
    public MeshRenderer RendererLT = null;
    public MeshRenderer RendererRB = null;
    public MeshRenderer RendererRT = null;

    #endregion

    void Awake()
    {
        OuyaSDK.registerMenuAppearingListener(this);
        OuyaSDK.registerPauseListener(this);
        OuyaSDK.registerResumeListener(this);
    }

    void OnDestroy()
    {
        OuyaSDK.unregisterMenuAppearingListener(this);
        OuyaSDK.unregisterPauseListener(this);
        OuyaSDK.unregisterResumeListener(this);
    }

    private void Start()
    {
        UpdatePlayerButtons();
        Input.ResetInputAxes();
    }

    public void SetPlayer1()
    {
        m_player = OuyaSDK.OuyaPlayer.player1;
        UpdatePlayerButtons();
    }
    public void SetPlayer2()
    {
        m_player = OuyaSDK.OuyaPlayer.player2;
        UpdatePlayerButtons();
    }
    public void SetPlayer3()
    {
        m_player = OuyaSDK.OuyaPlayer.player3;
        UpdatePlayerButtons();
    }
    public void SetPlayer4()
    {
        m_player = OuyaSDK.OuyaPlayer.player4;
        UpdatePlayerButtons();
    }
    public void SetPlayer5()
    {
        m_player = OuyaSDK.OuyaPlayer.player5;
        UpdatePlayerButtons();
    }
    public void SetPlayer6()
    {
        m_player = OuyaSDK.OuyaPlayer.player6;
        UpdatePlayerButtons();
    }
    public void SetPlayer7()
    {
        m_player = OuyaSDK.OuyaPlayer.player7;
        UpdatePlayerButtons();
    }
    public void SetPlayer8()
    {
        m_player = OuyaSDK.OuyaPlayer.player8;
        UpdatePlayerButtons();
    }

    public void OuyaMenuAppearing()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaOnPause()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaOnResume()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    #region Mapping Helpers

    float GetAxis(string ouyaMapping, OuyaSDK.OuyaPlayer player)
    {
        string axisName = string.Empty;
#if !UNITY_EDITOR && UNITY_ANDROID
        switch (ouyaMapping)
        {
            case "LX":
                axisName = string.Format("Joy{0} Axis 1", (int)player);
                break;
            case "LY":
                axisName = string.Format("Joy{0} Axis 2", (int)player);
                break;
            case "RX":
                axisName = string.Format("Joy{0} Axis 3", (int)player);
                break;
            case "RY":
                axisName = string.Format("Joy{0} Axis 4", (int)player);
                break;
            case "LT":
                axisName = string.Format("Joy{0} Axis 5", (int)player);
                break;
            case "RT":
                axisName = string.Format("Joy{0} Axis 6", (int)player);
                break;
            default:
                return 0f;
        }
#else
        switch (ouyaMapping)
        {
            case "LX":
                axisName = string.Format("Joy{0} Axis 1", (int)player);
                break;
            case "LY":
                axisName = string.Format("Joy{0} Axis 2", (int)player);
                break;
            case "RX":
                axisName = string.Format("Joy{0} Axis 4", (int)player);
                break;
            case "RY":
                axisName = string.Format("Joy{0} Axis 5", (int)player);
                break;
            case "LT":
                axisName = string.Format("Joy{0} Axis 9", (int)player);
                break;
            case "RT":
                axisName = string.Format("Joy{0} Axis 10", (int)player);
                break;
            default:
                return 0f;
        }
#endif
        return Input.GetAxis(axisName);
    }

    string GetKeyCode(OuyaSDK.OuyaPlayer player, int buttonNum)
    {
        switch (player)
        {
            case OuyaSDK.OuyaPlayer.none:
                return string.Format("JoystickButton{0}", buttonNum);
            default:
                return string.Format("Joystick{0}Button{1}", ((int)player), buttonNum);
        }

        return string.Empty;
    }

    private bool GetButton(OuyaSDK.OuyaPlayer player, int buttonNum)
    {
        string keyCode = GetKeyCode(player, buttonNum);
        if (string.IsNullOrEmpty(keyCode))
        {
            return false;
        }
        OuyaKeyCode key = (OuyaKeyCode)Enum.Parse(typeof(OuyaKeyCode), keyCode);
        return Input.GetKey((KeyCode)(int)key);
    }

    private bool GetButton(OuyaSDK.OuyaPlayer player, OuyaSDK.KeyEnum keyCode)
    {
        switch (keyCode)
        {
            case OuyaSDK.KeyEnum.BUTTON_LB:
                return GetButton(player, 4);
            case OuyaSDK.KeyEnum.BUTTON_RB:
                return GetButton(player, 5);
            case OuyaSDK.KeyEnum.BUTTON_O:
                return GetButton(player, 0);
            case OuyaSDK.KeyEnum.BUTTON_U:
                return GetButton(player, 1);
            case OuyaSDK.KeyEnum.BUTTON_Y:
                return GetButton(player, 2);
            case OuyaSDK.KeyEnum.BUTTON_A:
                return GetButton(player, 3);
            case OuyaSDK.KeyEnum.BUTTON_L3:
                return GetButton(player, 6);
            case OuyaSDK.KeyEnum.BUTTON_R3:
                return GetButton(player, 7);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_UP:
                return GetButton(player, 8);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN:
                return GetButton(player, 9);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT:
                return GetButton(player, 10);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT:
                return GetButton(player, 11);
            case OuyaSDK.KeyEnum.BUTTON_LT:
                return GetButton(player, 12);
            case OuyaSDK.KeyEnum.BUTTON_RT:
                return GetButton(player, 13);
            default:
                return false;
        }
    }

    #endregion

    #region Presentation

    private void UpdateLabels()
    {
        try
        {
            OuyaNGUIHandler nguiHandler = GameObject.Find("_NGUIHandler").GetComponent<OuyaNGUIHandler>();
            if (nguiHandler != null)
            {
                string[] joystickNames = Input.GetJoystickNames();
                for (int i = 0; i < joystickNames.Length || i < 8; i++)
                {
                    string joyName = "N/A";
                    if (i < joystickNames.Length)
                    {
                        joyName = joystickNames[i];
                    }
                    switch (i)
                    {
                        case 0:
                            nguiHandler.controller1.text = m_player == OuyaSDK.OuyaPlayer.player1 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 1:
                            nguiHandler.controller2.text = m_player == OuyaSDK.OuyaPlayer.player2 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 2:
                            nguiHandler.controller3.text = m_player == OuyaSDK.OuyaPlayer.player3 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 3:
                            nguiHandler.controller4.text = m_player == OuyaSDK.OuyaPlayer.player4 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 4:
                            nguiHandler.controller5.text = m_player == OuyaSDK.OuyaPlayer.player5 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 5:
                            nguiHandler.controller6.text = m_player == OuyaSDK.OuyaPlayer.player6 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 6:
                            nguiHandler.controller7.text = m_player == OuyaSDK.OuyaPlayer.player7 ? string.Format("*{0}", joyName) : joyName;
                            break;
                        case 7:
                            nguiHandler.controller8.text = m_player == OuyaSDK.OuyaPlayer.player8 ? string.Format("*{0}", joyName) : joyName;
                            break;

                    }
                }

                nguiHandler.button1.text = GetButton(m_player, 0).ToString();
                nguiHandler.button2.text = GetButton(m_player, 1).ToString();
                nguiHandler.button3.text = GetButton(m_player, 2).ToString();
                nguiHandler.button4.text = GetButton(m_player, 3).ToString();
                nguiHandler.button5.text = GetButton(m_player, 4).ToString();
                nguiHandler.button6.text = GetButton(m_player, 5).ToString();
                nguiHandler.button7.text = GetButton(m_player, 6).ToString();
                nguiHandler.button8.text = GetButton(m_player, 7).ToString();
                nguiHandler.button9.text = GetButton(m_player, 8).ToString();
                nguiHandler.button10.text = GetButton(m_player, 9).ToString();
                nguiHandler.button11.text = GetButton(m_player, 10).ToString();
                nguiHandler.button12.text = GetButton(m_player, 11).ToString();
                nguiHandler.button13.text = GetButton(m_player, 12).ToString();
                nguiHandler.button14.text = GetButton(m_player, 13).ToString();
                nguiHandler.button15.text = GetButton(m_player, 14).ToString();
                nguiHandler.button16.text = GetButton(m_player, 15).ToString();
                nguiHandler.button17.text = GetButton(m_player, 16).ToString();
                nguiHandler.button18.text = GetButton(m_player, 17).ToString();
                nguiHandler.button19.text = GetButton(m_player, 18).ToString();
                nguiHandler.button20.text = GetButton(m_player, 19).ToString();

                //nguiHandler.rawOut.text = OuyaGameObject.InputData;
            }
        }
        catch (System.Exception)
        {
        }
    }

    void Update()
    {
        OuyaNGUIHandler nguiHandler = GameObject.Find("_NGUIHandler").GetComponent<OuyaNGUIHandler>();
        if (nguiHandler != null)
        {
            // Input.GetAxis("Joy1 Axis1");
            nguiHandler.axis1.text = Input.GetAxis(string.Format("Joy{0} Axis 1", (int) m_player)).ToString("F2");
            nguiHandler.axis2.text = Input.GetAxis(string.Format("Joy{0} Axis 2", (int) m_player)).ToString("F2");
            nguiHandler.axis3.text = Input.GetAxis(string.Format("Joy{0} Axis 3", (int) m_player)).ToString("F2");
            nguiHandler.axis4.text = Input.GetAxis(string.Format("Joy{0} Axis 4", (int) m_player)).ToString("F2");
            nguiHandler.axis5.text = Input.GetAxis(string.Format("Joy{0} Axis 5", (int) m_player)).ToString("F2");
            nguiHandler.axis6.text = Input.GetAxis(string.Format("Joy{0} Axis 6", (int) m_player)).ToString("F2");
            nguiHandler.axis7.text = Input.GetAxis(string.Format("Joy{0} Axis 7", (int) m_player)).ToString("F2");
            nguiHandler.axis8.text = Input.GetAxis(string.Format("Joy{0} Axis 8", (int) m_player)).ToString("F2");
            nguiHandler.axis9.text = Input.GetAxis(string.Format("Joy{0} Axis 9", (int) m_player)).ToString("F2");
            nguiHandler.axis10.text = Input.GetAxis(string.Format("Joy{0} Axis 10", (int) m_player)).ToString("F2");
        }
    }

    void FixedUpdate()
    {
        UpdateController();
        UpdateLabels();

        if (Input.GetMouseButtonDown(0))
        {
            int index = ((int) m_player + 1)%9;
            if (index == 0)
            {
                ++index;
            }
            m_player = (OuyaSDK.OuyaPlayer)index;
            UpdatePlayerButtons();
        }
    }

    void UpdatePlayerButtons()
    {
        OuyaNGUIHandler nguiHandler = GameObject.Find("_NGUIHandler").GetComponent<OuyaNGUIHandler>();
        if (nguiHandler != null)
        {
            nguiHandler.player1.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player1);
            nguiHandler.player2.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player2);
            nguiHandler.player3.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player3);
            nguiHandler.player4.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player4);
            nguiHandler.player5.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player5);
            nguiHandler.player6.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player6);
            nguiHandler.player7.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player7);
            nguiHandler.player8.SendMessage("OnHover", m_player == OuyaSDK.OuyaPlayer.player8);

        }
    }

    #endregion

    #region Extra

    private void UpdateHighlight(MeshRenderer mr, bool highlight, bool instant = false)
    {
        float time = Time.deltaTime * 10;
        if (instant) { time = 1000; }

        if (highlight)
        {
            Color c = new Color(0, 10, 0, 1);
            mr.material.color = Color.Lerp(mr.material.color, c, time);
        }
        else
        {
            mr.material.color = Color.Lerp(mr.material.color, Color.white, time);
        }
    }

    private void UpdateController()
    {
        #region Axis Code

        UpdateHighlight(RendererAxisLeft, Mathf.Abs(GetAxis("LX", m_player)) > 0.25f ||
            Mathf.Abs(GetAxis("LY", m_player)) > 0.25f);

        RendererAxisLeft.transform.localRotation = Quaternion.Euler(GetAxis("LY", m_player) * 15, 0, GetAxis("LX", m_player) * 15);

        UpdateHighlight(RendererAxisRight, Mathf.Abs(GetAxis("RX", m_player)) > 0.25f ||
            Mathf.Abs(GetAxis("RY", m_player)) > 0.25f);

        RendererAxisRight.transform.localRotation = Quaternion.Euler(GetAxis("RY", m_player) * 15, 0, GetAxis("RX", m_player) * 15);

        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_L3))
        {
            RendererAxisLeft.transform.localPosition = Vector3.Lerp(RendererAxisLeft.transform.localPosition,
                                                                     new Vector3(5.503977f, 0.75f, -3.344945f), Time.fixedDeltaTime);
        }
        else
        {
            RendererAxisLeft.transform.localPosition = Vector3.Lerp(RendererAxisLeft.transform.localPosition,
                                                                     new Vector3(5.503977f, 1.127527f, -3.344945f), Time.fixedDeltaTime);
        }

        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_R3))
        {
            RendererAxisRight.transform.localPosition = Vector3.Lerp(RendererAxisRight.transform.localPosition,
                                                                     new Vector3(-2.707688f, 0.75f, -1.354063f), Time.fixedDeltaTime);
        }
        else
        {
            RendererAxisRight.transform.localPosition = Vector3.Lerp(RendererAxisRight.transform.localPosition,
                                                                     new Vector3(-2.707688f, 1.11295f, -1.354063f), Time.fixedDeltaTime);
        }

        #endregion


        #region Button Code

        #region BUTTONS O - A
        //Check O button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_O))
        {
            UpdateHighlight(RendererButtonO, true, true);
        }
        else
        {
            UpdateHighlight(RendererButtonO, false, true);
        }

        //Check U button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_U))
        {
            UpdateHighlight(RendererButtonU, true, true);
        }
        else
        {
            UpdateHighlight(RendererButtonU, false, true);
        }

        //Check Y button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_Y))
        {
            UpdateHighlight(RendererButtonY, true, true);
        }
        else
        {
            UpdateHighlight(RendererButtonY, false, true);
        }

        //Check A button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_A))
        {
            UpdateHighlight(RendererButtonA, true, true);
        }
        else
        {
            UpdateHighlight(RendererButtonA, false, true);
        }

        //Check L3 button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_L3))
        {
            UpdateHighlight(RendererAxisLeft, true, true);
        }
        else
        {
            UpdateHighlight(RendererAxisLeft, false, true);
        }

        //Check R3 button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_R3))
        {
            UpdateHighlight(RendererAxisRight, true, true);
        }
        else
        {
            UpdateHighlight(RendererAxisRight, false, true);
        }
        #endregion

        #region Bumpers
        //Check LB button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_LB))
        {
            UpdateHighlight(RendererLB, true, true);
        }
        else
        {
            UpdateHighlight(RendererLB, false, true);
        }

        //Check RB button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_RB))
        {
            UpdateHighlight(RendererRB, true, true);
        }
        else
        {
            UpdateHighlight(RendererRB, false, true);
        }
        #endregion

        #region triggers
        //Check LT button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_LT))
        {
            UpdateHighlight(RendererLT, true, true);
        }
        else
        {
            UpdateHighlight(RendererLT, false, true);
        }

        //Check RT button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_RT))
        {
            UpdateHighlight(RendererRT, true, true);
        }
        else
        {
            UpdateHighlight(RendererRT, false, true);
        }
        #endregion

        #region DPAD
        //Check DPAD UP button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_DPAD_UP))
        {
            UpdateHighlight(RendererDpadUp, true, true);
        }
        else
        {
            UpdateHighlight(RendererDpadUp, false, true);
        }

        //Check DPAD DOWN button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN))
        {
            UpdateHighlight(RendererDpadDown, true, true);
        }
        else
        {
            UpdateHighlight(RendererDpadDown, false, true);
        }

        //Check DPAD LEFT button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT))
        {
            UpdateHighlight(RendererDpadLeft, true, true);
        }
        else
        {
            UpdateHighlight(RendererDpadLeft, false, true);
        }

        //Check DPAD RIGHT button for down state
        if (GetButton(m_player, OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT))
        {
            UpdateHighlight(RendererDpadRight, true, true);
        }
        else
        {
            UpdateHighlight(RendererDpadRight, false, true);
        }
        #endregion

        #endregion
    }

    #endregion
}