using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BeanState {
     UP,
     DOWN,
     LEFT,
     RIGHT
}
public class BeanSingle : MonoBehaviour {


    void ChangeBeanState(BeanState beanstate) {
        switch (beanstate) {
            case BeanState.UP:
                transform.localEulerAngles = new Vector3(0, 0, 90.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                break;
            case BeanState.DOWN:
                transform.localEulerAngles = new Vector3(0, 0, 270.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
                break;
            case BeanState.LEFT:
                transform.localEulerAngles = new Vector3(0, 0,180.0f);
                transform.position = new Vector3(transform.position.x-1.0f, transform.position.y , transform.position.z);
                break;
            case BeanState.RIGHT:
                transform.localEulerAngles = new Vector3(0,0,0);
                transform.position = new Vector3(transform.position.x+1.0f, transform.position.y, transform.position.z);
                break;
        }
    }
	// Use this for initialization
	void Start () {
        ChangeBeanState(BeanState.RIGHT);
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnGUI()
    {
            Event e = Event.current;
        if (e.isKey) {
            switch (e.keyCode) {
                case KeyCode.A:
                    ChangeBeanState(BeanState.LEFT);
                    break;
                case KeyCode.D:
                    ChangeBeanState(BeanState.RIGHT);
                    break;
                case KeyCode.W:
                    ChangeBeanState(BeanState.UP);
                    break;
                case KeyCode.S:
                    ChangeBeanState(BeanState.DOWN);
                    break;
            }
            Debug.Log("Detected key code: " + e.keyCode);
        }
                
    }
}
