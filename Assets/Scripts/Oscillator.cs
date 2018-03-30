using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {


    RectTransform arrow;

    float height; 

    // Use this for initialization
    void Start () {

        arrow = transform.Find("arrow").GetComponent<RectTransform>();
        height = transform.GetComponent<RectTransform>().sizeDelta.y;
        height /= 2;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetArrow(float value)
    {
        arrow.localPosition = new Vector2(arrow.localPosition.x, height * (value - 0.5f) * 2);
    }

}
