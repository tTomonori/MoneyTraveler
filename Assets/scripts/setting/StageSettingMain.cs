using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSettingMain : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Subject.addObserver(new Observer("stageSettingMain", (aMessage) => {
            switch (aMessage.name) {
                case "0.5ButtonPushed":
                case "1ButtonPushed":
                case "2ButtonPushed":
                case "3ButtonPushed":
                case "4ButtonPushed":
                case "5ButtonPushed":
                    break;
                default:
                    return;
            }
            MySceneManager.closeScene("stageSetting", new Arg(new Dictionary<string, object>() { { "stage", "standard" } }));
        }));
    }

    private void OnDestroy() {
        Subject.removeObserver("stageSettingMain");
    }
}
