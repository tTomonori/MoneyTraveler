using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeeSettingMain : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Subject.addObserver(new Observer("feeSettingMain", (aMessage) => {
            float tRate = 0;
            switch (aMessage.name) {
                case "0.5ButtonPushed":
                    tRate = 0.5f;
                    break;
                case "1ButtonPushed":
                    tRate = 1f;
                    break;
                case "2ButtonPushed":
                    tRate = 2f;
                    break;
                case "3ButtonPushed":
                    tRate = 3f;
                    break;
                case "4ButtonPushed":
                    tRate = 4f;
                    break;
                case "5ButtonPushed":
                    tRate = 5f;
                    break;
                default:
                    return;
            }
            MySceneManager.closeScene("feeSetting", new Arg(new Dictionary<string, object>() { { "rate", tRate } }));
        }));
    }

    private void OnDestroy() {
        Subject.removeObserver("feeSettingMain");
    }
}
