using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquisitionSettingMain : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Subject.addObserver(new Observer("acquisitionSettingMain", (aMessage) => {
            float tRate = 0;
            switch (aMessage.name) {
                case "noneButtonPushed":
                    tRate = 0;
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
            MySceneManager.closeScene("acquisitionSetting", new Arg(new Dictionary<string, object>() { { "rate", tRate } }));
        }));
    }

    private void OnDestroy() {
        Subject.removeObserver("acquisitionSettingMain");
    }
}
