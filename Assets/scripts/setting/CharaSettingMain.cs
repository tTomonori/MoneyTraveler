using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSettingMain : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        setNg();
        Subject.addObserver(new Observer("charaSettingMain", (aMessage) => {
            string tFile = "";
            string tName = "";
            switch (aMessage.name) {
                case "noneButtonPushed":
                    tFile = "none";
                    tName = "none";
                    break;
                case "marieButtonPushed":
                    tFile = "marie";
                    tName = "マリー";
                    break;
                case "rearButtonPushed":
                    tFile = "rear";
                    tName = "リア";
                    break;
                case "maruButtonPushed":
                    tFile = "maru";
                    tName = "マル";
                    break;
                case "chiaraButtonPushed":
                    tFile = "chiara";
                    tName = "キアラ";
                    break;
                default:
                    return;
            }
            MySceneManager.closeScene("charaSetting", new Arg(new Dictionary<string, object>() { { "file", tFile }, { "name", tName } }));
        }));
    }
    //選択不可のキャラの設定
    private void setNg() {
        Arg aData = MySceneManager.getArg("charaSetting");
        int tCount = 0;
        foreach(string tFile in aData.get<string[]>("ng")) {
            if (tFile == "none") continue;
            MyBehaviour tBehaviour = GameObject.Find(tFile).GetComponent<MyBehaviour>();
            tBehaviour.findChild<SpriteRenderer>("ng").color = new Color(0, 0, 0, 0.6f);
            tBehaviour.findChild<MyButton>("button").gameObject.SetActive(false);
            tCount++;
        }
        if (tCount <= 1) {
            MyBehaviour tBehaviour = GameObject.Find("noneChara").GetComponent<MyBehaviour>();
            tBehaviour.findChild<SpriteRenderer>("ng").color = new Color(0, 0, 0, 0.6f);
            tBehaviour.findChild<MyButton>("button").gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        Subject.removeObserver("charaSettingMain");
    }
}
