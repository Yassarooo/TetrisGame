﻿using UnityEngine;

public class SwipeManager2 : MonoBehaviour {
    /*public float blockSize;
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
    MoveFactory mf = new MoveFactory();

    public void Update () {
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
            } else if (touch.phase == TouchPhase.Ended) {
                var touchDuration = Time.time - touchBeginTime;
                var offset = (touch.position - initialPosition).magnitude;

                if (touchDuration < tapMaxDuration && offset < tapMaxOffset) {
                    mf.PerformMove("Up");
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
            ActionForHorizontalMoveOffset (offset.x);
        }
        if (offset.y <= -blockSize) {
            HandleVerticalMove (touch);
            mf.PerformMove("Down");
            //_activeBlock.DownClick ();
        }
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

    void ActionForHorizontalMoveOffset (float offset) {
        if (offset > 0)
            mf.PerformMove("Right");
        else
           mf.PerformMove("Left");
    }*/
}