using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStick : Singleton<JoyStick>
{
    [SerializeField]
    Transform Root,Pad;
    [SerializeField]
    float MaxR = 1;

    Vector2 Origin = new Vector2(0,0);
    [SerializeField]
    bool _IsOriginSet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ListenJoyStick2();
        ReleaseTouch();
    }

    void ListenJoyStick()  // single touch
    {  

        if(!Input.GetMouseButton(0))
            return;

        if(Input.mousePosition.x > Screen.width /2 && _IsOriginSet == false)
        {
            return;
        }

        if (Input.mousePosition.y > Screen.height / 2 && _IsOriginSet == false)
        {
            return;
        }

        if (!_IsOriginSet){
            _IsOriginSet = true;
            Origin = Input.mousePosition;
            Root.position = Origin;
            Pad.transform.position = Origin;
            Root.gameObject.SetActive(true);
            return;
        }
        Vector2 currentTouch = (Vector2)Input.mousePosition-Origin;
        if(currentTouch == Vector2.zero)
            return;
        if(currentTouch.magnitude <= MaxR){
            Pad.transform.position = Input.mousePosition;
            return;
        }

        float currentAngle = Mathf.Atan2(currentTouch.y,currentTouch.x);
        float X = Origin.x + MaxR * Mathf.Cos(currentAngle);
        float Y = Origin.y + MaxR * Mathf.Sin(currentAngle);
        Pad.transform.position = new Vector2(X,Y);

       
    }
    void ListenJoyStick2()  // multi touch
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.position.x > Screen.width / 2 && _IsOriginSet == false)
        {
            return;
        }

        if (touch.position.y > Screen.height / 2 && _IsOriginSet == false)
        {
            return;
        }
        if (!_IsOriginSet)
        {
            _IsOriginSet = true;
            Origin = touch.position;
            Root.position = Origin;
            Pad.transform.position = Origin;
            Root.gameObject.SetActive(true);
            return;
        }
        Vector2 currentTouch = (Vector2)touch.position - Origin;
        if (currentTouch == Vector2.zero)
            return;
        if (currentTouch.magnitude <= MaxR)
        {
            Pad.transform.position = touch.position;
            return;
        }

        float currentAngle = Mathf.Atan2(currentTouch.y, currentTouch.x);
        float X = Origin.x + MaxR * Mathf.Cos(currentAngle);
        float Y = Origin.y + MaxR * Mathf.Sin(currentAngle);
        Pad.transform.position = new Vector2(X, Y);

        
    }
    void ReleaseTouch(){
        if(!_IsOriginSet)
            return;
        if(Input.GetMouseButtonUp(0)){
            _IsOriginSet = false;
            Root.gameObject.SetActive(false);
        }
    }

    public Vector2 GetJoyVector(){  // for top-down
        if(!_IsOriginSet)
            return Vector2.zero;
        Vector2 tmp = (Vector2)Input.mousePosition-Origin;
        return tmp.normalized;
    }

    public Vector2 GetJoyVectorRaw()
    {
        Vector2 JoyVector = this.GetJoyVector();
        if(Mathf.Abs(JoyVector.x) > Mathf.Abs(JoyVector.y))
        {
            //Joystick huong sang ngang
            if(JoyVector.x > 0)
            {
                //sang phai
                return Vector2.right;
            }
            else if( JoyVector.x < 0)
            {
                //sang trai
                return Vector2.left;
            }
        }
        else if(Mathf.Abs(JoyVector.x) < Mathf.Abs(JoyVector.y))
        {
            //joystick huong theo chieu doc
            if(JoyVector.y > 0)
            {
                //len tren
                return Vector2.up;
            }
            else if(JoyVector.y < 0)
            {
                //xuong duoi
                return Vector2.down;
            }
        }
               
        
        return Vector2.zero;
    }
}
