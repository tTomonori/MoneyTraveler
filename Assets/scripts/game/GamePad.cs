using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePad : MyPad {
    public Camera mCamera;
    private void Start() {
        mCamera = this.GetComponentInParent<Camera>();
    }
    private void OnMouseDrag() {
        mouseDrag();
        if (mIsDragging) {
            dragged();
        }
    }
    private void OnMouseDown() {
        mouseDown();
    }
    private void OnMouseUp() {
        mouseUp();
        if (mIsTapped) {
            clicked();
        }
    }
    private void dragged() {
        Subject.sendMessage(new Message("gamePadDragged", new Arg(new Dictionary<string, object>() { { "vector", mDelta } })));
    }
    private void clicked() {
        Ray tRay = mCamera.ScreenPointToRay(Input.mousePosition);
        GameMass tMass;
        foreach (RaycastHit tHit in Physics.RaycastAll(tRay)) {
            tMass = tHit.collider.GetComponent<GameMass>();
            if (tMass != null) {
                Subject.sendMessage(new Message("gamePadClicked", new Arg(new Dictionary<string, object>() { { "mass", tMass } })));
                return;
            }
        }
    }
}
