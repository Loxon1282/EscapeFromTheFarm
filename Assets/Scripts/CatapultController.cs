using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour
{


    [SerializeField]
    RectTransform wheel;

    [SerializeField]
    float maxDist;          //max distance from wheel center to track touch position

    [SerializeField]
    float minDist;          //min distance from wheel center to track touch position

    [SerializeField]
    float maxSpins;

    [SerializeField]
    float lounchTime;

    [SerializeField]
    float moveMargin;

    float fingerRotation;
    float fingerPrevRotation;
    float fingerDeltaRotation;

    float sum;
    float maxSum;

    float timer;
    bool fingerIn = false;
    bool timerActive = false;

    Launcher launcher;

    // Use this for initialization
    void Start()
    {
        sum = 0;
        maxSum = maxSpins * Mathf.PI * 2;
        launcher = GetComponent<Launcher>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < lounchTime && Mathf.Abs(sum) < maxSum) SpinLogic();
        else
        {
            Done();
            Reset();
        }

        if (timerActive) timer += Time.deltaTime;

    }


    void SpinLogic()
    {
        if (Input.touchCount >= 1)
        {

            float touchDist = Vector2.Distance(Input.GetTouch(0).position, wheel.position);

            if (Input.GetTouch(0).phase == TouchPhase.Began)                                                //Finger touch
            {

                if (touchDist >= minDist && touchDist <= maxDist)
                {
                    fingerPrevRotation = GetLookAtRotation(wheel.position, Input.GetTouch(0).position);
                    fingerIn = true;
                }

            }


            if (Input.GetTouch(0).phase == TouchPhase.Moved)                                                //Finger moving
            {

                if (touchDist >= minDist && touchDist <= maxDist)
                {
                    if (fingerIn)
                    {
                        fingerRotation = GetLookAtRotation(wheel.position, Input.GetTouch(0).position);
                        fingerDeltaRotation = fingerRotation - fingerPrevRotation;
                        fingerPrevRotation = fingerRotation;

                        if (fingerDeltaRotation > 5) fingerDeltaRotation -= Mathf.PI *2;
                        if (fingerDeltaRotation < -5) fingerDeltaRotation += Mathf.PI * 2;

                        sum += fingerDeltaRotation;

                        SetState();

                        if (!timerActive && Mathf.Abs(sum) >= moveMargin) timerActive = true;
                    }
                    else
                    {
                        fingerRotation = GetLookAtRotation(wheel.position, Input.GetTouch(0).position);
                        fingerPrevRotation = fingerRotation;
                        fingerIn = true;
                    }
                }
                else
                {
                    fingerIn = false;
                }

            }

        }
    }

    private void Reset()
    {
        sum = 0;
        timer = 0;
        fingerIn = false;
        timerActive = false;
    }

    float GetLookAtRotation(Vector2 from, Vector2 to)
    {
        Vector2 hlp = to - from;
        return Mathf.Atan2(hlp.y, hlp.x);
    }

    void SetState()
    {
        launcher.SetState(Mathf.Abs(sum) / maxSum);
    }

    void Done()
    {
        launcher.SetLaunch();
    }

}

