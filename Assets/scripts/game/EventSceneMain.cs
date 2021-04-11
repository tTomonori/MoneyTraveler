using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSceneMain : MonoBehaviour {
    [SerializeField]
    public MyBehaviour mBox;
    public MyBehaviour mEventBox;
    public MyBehaviour mWindow;
    public TextMesh mText;
    private void Start() {
        Arg tArg = MySceneManager.getArg("eventBox");
        mText.text = tArg.get<string>("text");
        mWindow.scale2D = new Vector2(0, 0);

        mBox.moveTo(new Vector3(0, 1, 0), 0.4f, () => {
            MyBehaviour.setTimeoutToIns(0.2f, () => {
                MySoundPlayer.playSe("open", false);
                mEventBox.scaleTo(new Vector2(0, 0), 0.3f);
                mWindow.scaleTo(new Vector2(1, 1), 0.4f);
                for(int i = 0; i < 5; i++) {
                    throwLight(mBox.worldPosition);
                }
                MyBehaviour.setTimeoutToIns(1.5f, () => {
                    MySceneManager.closeScene("eventBox");
                });
            });
        });
    }
    public void throwLight(Vector2 aPosition) {
        MyBehaviour tLight = MyBehaviour.create<MyBehaviour>();
        tLight.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/game/dice/light");
        float tScale = Random.Range(0.3f, 0.6f);
        tLight.scale2D = new Vector2(tScale, tScale);
        tLight.transform.SetParent(this.transform, false);
        tLight.position = aPosition;
        tLight.changeLayer(11);
        float tMoveDistance = 2 * Random.Range(1, 4);
        float tMoveSpeed = 3 * Random.Range(1, 3);
        Vector2 tDirection = Quaternion.Euler(0, 0, Random.Range(0, 359)) * new Vector2(tMoveDistance, 0);
        tLight.moveByWithSpeed(tDirection, tMoveSpeed, () => {
            tLight.delete();
        });
    }
}
