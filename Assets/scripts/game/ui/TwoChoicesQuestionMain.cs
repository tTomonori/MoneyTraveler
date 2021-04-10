using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoChoicesQuestionMain : MonoBehaviour {
    [SerializeField]
    public TextMesh mMesh;
    void Start() {
        mMesh.text = MySceneManager.getArg("twoChoicesQuestion").get<string>("text");

        Subject.addObserver(new Observer("twoChoicesQuestionMain", (aMessage) => {
            switch (aMessage.name) {
                case "yesButtonPushed":
                    MySceneManager.closeScene("twoChoicesQuestion", new Arg(new Dictionary<string, object>() { { "answer", true } }));
                    break;
                case "noButtonPushed":
                    MySceneManager.closeScene("twoChoicesQuestion", new Arg(new Dictionary<string, object>() { { "answer", false } }));
                    break;
            }
        }));
    }
    private void OnDestroy() {
        Subject.removeObserver("twoChoicesQuestionMain");
    }
}
