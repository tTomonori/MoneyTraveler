using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBox : MonoBehaviour {
    [SerializeField]
    public MyBehaviour mBox;
    public MyBehaviour mNumber;
    public TextMesh mNumMesh;
    public bool mIsOpen = false;
    public int number { get { return int.Parse(mNumMesh.text); } }
    public void open(int aNum) {
        if (mIsOpen) return;
        mIsOpen = true;
        mBox.scaleTo(new Vector2(0, 0), 0.3f);
        mNumber.scaleTo(new Vector2(3, 3), 0.3f);
        mNumMesh.text = aNum.ToString();

        //sound
        MySoundPlayer.playSe("open", false);

        //light
        for (int i = 0; i < 5; i++)
            throwLight();
    }
    public void throwLight() {
        MyBehaviour tLight = MyBehaviour.create<MyBehaviour>();
        tLight.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/game/dice/light");
        float tScale = Random.Range(0.3f, 0.6f);
        tLight.scale2D = new Vector2(tScale, tScale);
        tLight.transform.SetParent(this.transform, false);
        tLight.changeLayer(10);
        float tMoveDistance = 2 * Random.Range(1, 4);
        float tMoveSpeed = 3 * Random.Range(1, 3);
        Vector2 tDirection = Quaternion.Euler(0, 0, Random.Range(0, 359)) * new Vector2(tMoveDistance, 0);
        tLight.moveByWithSpeed(tDirection, tMoveSpeed, () => {
            tLight.delete();
        });
    }
}
