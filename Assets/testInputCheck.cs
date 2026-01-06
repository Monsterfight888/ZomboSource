using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInputCheck : MonoBehaviour
{
    public bool Joystick1Button0;
    public bool Joystick1Button1;
    public bool Joystick1Button2;
    public bool Joystick1Button3;
    public bool Joystick1Button4;
    public bool Joystick1Button5;
    public bool Joystick1Button6;
    public bool Joystick1Button7;
    public bool Joystick1Button8;
    public bool Joystick1Button9;
    public bool Joystick1Button10;
    public bool Joystick1Button11;
    public bool Joystick1Button12;
    public bool Joystick1Button13;
    public bool Joystick1Button14;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Joystick1Button0 = false;
        Joystick1Button1 = false;
        Joystick1Button2 = false;
        Joystick1Button3 = false;
        Joystick1Button4 = false;
        Joystick1Button5 = false;
        Joystick1Button6 = false;
        Joystick1Button7 = false;
        Joystick1Button8 = false;
        Joystick1Button9 = false;
        Joystick1Button10 = false;
        Joystick1Button11 = false;
        Joystick1Button12 = false;
        Joystick1Button13 = false;
        Joystick1Button14 = false;
        if(Input.GetKey(KeyCode.Joystick1Button0))
        {
            Joystick1Button0 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Joystick1Button2 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            Joystick1Button3 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            Joystick1Button4 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            Joystick1Button5 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            Joystick1Button6 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            Joystick1Button7 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            Joystick1Button8 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button9))
        {
            Joystick1Button9 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            Joystick1Button10 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button11))
        {
            Joystick1Button11 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button12))
        {
            Joystick1Button12 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button13))
        {
            Joystick1Button13 = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button14))
        {
            Joystick1Button14 = true;
        }
    }
}
