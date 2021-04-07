using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSettingMain : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Subject.addObserver(new Observer("aiSettingMain", (aMessage) => {
            string tAi = "ai";
            string tAiName = "ai";
            switch (aMessage.name) {
                case "playerButtonPushed":
                    tAi = "player";
                    tAiName = "プレイヤー";
                    break;
                case "solidButtonPushed":
                    tAi = "solid";
                    tAiName = "堅実";
                    break;
                case "carefullyButtonPushed":
                    tAi = "carefully";
                    tAiName = "慎重";
                    break;
                case "impulseButtonPushed":
                    tAi = "impulse";
                    tAiName = "衝動";
                    break;
                default:
                    return;
            }
            MySceneManager.closeScene("aiSetting", new Arg(new Dictionary<string, object>() { { "ai", tAi }, { "name", tAiName } }));
        }));
    }

    private void OnDestroy() {
        Subject.removeObserver("aiSettingMain");
    }
}
