using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour
{


    [SerializeField]
    RectTransform wheel;        //wheel  object

    [SerializeField]
    Oscillator osciallator;    //oscillator graphical representation

    [SerializeField]
    float maxDist;              //max distance from wheel center to track touch position

    [SerializeField]
    float minDist;              //min distance from wheel center to track touch position

    [SerializeField]
    float maxSpins;             // how many spins do you need to get full value

    [SerializeField]
    float lounchTime;           // time limit after which launcher will launch

    [SerializeField]
    float moveMargin;           // touch move distance margin 

    [SerializeField]
    float oExtSpeed;            // oscillation speed added to base speed

    [SerializeField]
    float oMinSpeed;            // min oscillator speed

    float fingerId;             // controlling finger
    float fingerAngel;
    float fingerPrevAngle;      // rotation from previous frame
    float fingerDeltaAngle;

    float sum;                  // rotation
    float maxSum;               // max rotation

    float timer;                // value of the timer
    bool fingerIn;              // is finger in range of wheel
    bool timerActive;
    bool launched;

    float aState;               // State of rotation(0-1, 0 = 0, 1 = maxSum)
    float oState;               // Oscillation state(0-1, 0.5 perfect value)
    float oDirection;           // direction of oscillation

    Launcher launcher;

    // Use this for initialization
    void Start()
    {
        sum = 0;
        oState = 0.5f;
        oDirection = 1;
        fingerIn = false;
        timerActive = false;
        launched = false;
        maxSum = maxSpins * Mathf.PI * 2;
        launcher = GetComponent<Launcher>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!launched && timer < lounchTime && Mathf.Abs(sum) < maxSum) // if not launched, have time and can rotate
        {
            SpinLogic();
            OscillationLogic();
        }
        else // else shoot and reset
        {
            Done();
            Reset();
        }

        if (timerActive) timer += Time.deltaTime; // ad time to timer

    }


    void SpinLogic()
    {
        if (Input.touchCount >= 1)
        {

            float touchDist = Vector2.Distance(Input.GetTouch(0).position, wheel.position); // calculates distance between touch and center of wheel

            if (Input.GetTouch(0).phase == TouchPhase.Began)                                                //Finger touch
            {

                if (touchDist >= minDist && touchDist <= maxDist)
                {
                    fingerPrevAngle = GetLookAtRotation(wheel.position, Input.GetTouch(0).position);
                    fingerId = Input.GetTouch(0).fingerId;
                    launched = false;
                    fingerIn = true;
                }

            }


            if (Input.GetTouch(0).phase == TouchPhase.Moved)                                                //Finger moving
            {

                if (touchDist >= minDist && touchDist <= maxDist)
                {
                    if (fingerIn) // if finger was inside range of wheel in previous frame 
                    {
                        fingerAngel = GetLookAtRotation(wheel.position, Input.GetTouch(0).position);    
                        fingerDeltaAngle = fingerAngel - fingerPrevAngle;
                        fingerPrevAngle = fingerAngel;

                        if (fingerDeltaAngle > 5) fingerDeltaAngle -= Mathf.PI *2; // GetLookAtRotation jumps from -180 to 180(or back) so this is zeroing that jump
                        if (fingerDeltaAngle < -5) fingerDeltaAngle += Mathf.PI * 2;

                        sum += fingerDeltaAngle;

                        SetWheelState();

                        if (!timerActive && Mathf.Abs(sum) >= moveMargin) timerActive = true; // if finger moved enough start timer
                    }
                    else // if finger was outside range we can add angel
                    {
                        fingerAngel = GetLookAtRotation(wheel.position, Input.GetTouch(0).position);
                        fingerPrevAngle = fingerAngel;
                        fingerIn = true;
                    }
                }
                else
                {
                    fingerIn = false;
                }

            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended && fingerId == Input.GetTouch(0).fingerId)                                                //Finger raised
            {
                fingerId = -1;
                launched = true;
            }

        }
    }


    void OscillationLogic()
    {
        float step = (oExtSpeed * aState + oMinSpeed) * oDirection;

        if (oState + step >= 1) 
        {
            oState = 1 - ((oState + step) - 1);
            oDirection = -1;
        }
        else if ((oState + step) <= 0)
        {
            oState = (oState + step) * -1;
            oDirection = 1;
        }
        else oState += step;

        osciallator.SetArrow(oState);
    }

    void Reset()
    {
        sum = 0;
        timer = 0;
        fingerIn = false;
        timerActive = false;
        launched = false;
    }

    float GetLookAtRotation(Vector2 from, Vector2 to)
    {
        Vector2 hlp = to - from;
        return Mathf.Atan2(hlp.y, hlp.x);
    }

    void SetWheelState()
    {
        aState = Mathf.Abs(sum) / maxSum;
        launcher.SetState(aState);
    }

    void Done() // launch launcher and passes state of oscillator
    {
        print(oState);
        launcher.SetLaunch(oState);
    }

}

