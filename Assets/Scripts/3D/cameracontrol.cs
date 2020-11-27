using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour {

    public float blockSize;
    Transform target;
    Transform rotTarget;
    Vector3 lastPos;
    Vector2 Lasttouch;
    float sensitivity = 0.25f;
    public float touchsens;
    public bool Enabled {
        get => enabled;
        set {
            enabled = value;
            cancelCurrentTouch = false;
        }
    }

    Vector2 initialPosition = Vector2.zero;
    Vector2 processedOffset = Vector2.zero;
    bool moveDownDetected;
    float touchBeginTime;
    readonly float tapMaxDuration = 0.25f;
    readonly float tapMaxOffset = 30.0f;
    readonly float swipeMaxDuration = 0.5f;
    bool cancelCurrentTouch;
    public Block _activeBlock;
    MoveFactory mf = new MoveFactory ();

    void Awake () {
        rotTarget = transform.parent;
        target = rotTarget.transform.parent;
    }

    public void Update () {
        transform.LookAt (target);
        if (Input.GetMouseButtonDown (0)) {
            lastPos = Input.mousePosition;
        }
        Orbit ();
        _activeBlock = Spawner.instance.activeBlock;

        if (Input.touchCount > 0) {
            var touch = Input.GetTouch (0);

            if (cancelCurrentTouch) {
                cancelCurrentTouch &= touch.phase != TouchPhase.Ended;
            } else if (touch.phase == TouchPhase.Began) {
                TouchBegan (touch);
            } else if (touch.phase == TouchPhase.Moved) {
                var offset = touch.position - initialPosition - processedOffset;
                HandleMove (touch, offset);
                Orbit2 (touch);
            } else if (touch.phase == TouchPhase.Ended) {
                var touchDuration = Time.time - touchBeginTime;
                var offset = (touch.position - initialPosition).magnitude;

                if (touchDuration < tapMaxDuration && offset < tapMaxOffset) {
                    //mf.PerformMove ("Up");
                    // _activeBlock.UpClick ();
                } else if (moveDownDetected && touchDuration < swipeMaxDuration) {
                    Spawner.instance.FallDown = true;
                }
            }
        } else {
            cancelCurrentTouch = false;
        }

    }

    public void Cancel () {
        cancelCurrentTouch |= Input.touchCount > 0;
    }

    void TouchBegan (Touch touch) {
        initialPosition = touch.position;
        processedOffset = Vector2.zero;
        moveDownDetected = false;
        touchBeginTime = Time.time;
    }

    void HandleMove (Touch touch, Vector2 offset) {
        if (Mathf.Abs (offset.x) >= blockSize) {
            HandleHorizontalMove (touch, offset.x);
            //ActionForHorizontalMoveOffset (offset.x);
        }
        if (offset.y <= -blockSize) {
            HandleVerticalMove (touch);
            //mf.PerformMove ("Down");
            //_activeBlock.DownClick ();
        }

    }

    void Orbit () {
        if (Input.GetMouseButton (0)) {
            Vector3 delta = Input.mousePosition - lastPos;
            float angleY = -delta.y * sensitivity;
            float angleX = delta.x * sensitivity;
            // x axis
            Vector3 angles = rotTarget.transform.eulerAngles;
            angles.x += angleY;
            angles.x = ClampAngle (angles.x, -85f, 85f);

            rotTarget.transform.eulerAngles = angles;

            //y axis
            target.RotateAround (target.position, Vector3.up, angleX);
            lastPos = Input.mousePosition;
        }
    }

    void Orbit2 (Touch touch) {
        Vector3 delta = touch.position - Lasttouch;
        float angleY = -delta.y * sensitivity;
        float angleX = delta.x * sensitivity;
        // x axis
        Vector3 angles = rotTarget.transform.eulerAngles;
        angles.x += angleY;
        angles.x = ClampAngle (angles.x, -85f, 85f);

        rotTarget.transform.eulerAngles = angles;

        //y axis
        target.RotateAround (target.position, Vector3.up, angleX);
        Lasttouch = touch.position;
    }

    float ClampAngle (float angle, float from, float to) {
        if (angle < 0) angle = 360 + angle;

        if (angle > 180f) return Mathf.Max (angle, 360 + from);

        return Mathf.Min (angle, to);
    }
    void HandleHorizontalMove (Touch touch, float offset) {
        processedOffset.x += Mathf.Sign (offset) * blockSize;
        processedOffset.y = (touch.position - initialPosition).y;
    }

    void HandleVerticalMove (Touch touch) {
        moveDownDetected = true;
        processedOffset.y -= blockSize;
        processedOffset.x = (touch.position - initialPosition).x;
    }

    /* void ActionForHorizontalMoveOffset (float offset) {
        if (offset > 0) {
            mf.PerformMove ("Right");

        } else {
            mf.PerformMove ("Left");
        }
    } */
}