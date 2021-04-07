using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMain : MonoBehaviour {
    private GameSetting mSetting;
    void Start() {
        mSetting = new GameSetting();
        setSetting(mSetting);

        Subject.addObserver(new Observer("settingMain", (aMessage) => {
            switch (aMessage.name) {
                case "initialMoneyRateButtonPushed":
                    MySceneManager.openScene("initialMoneySetting", null, null, (aData) => {
                        mSetting.mInitialMoneyRate = aData.get<float>("rate");
                        setSetting(mSetting);
                    });
                    break;
                case "feeRateButtonPushed":
                    MySceneManager.openScene("feeSetting", null, null, (aData) => {
                        mSetting.mFeeRate = aData.get<float>("rate");
                        setSetting(mSetting);
                    });
                    break;
                case "acquisitionRateButtonPushed":
                    MySceneManager.openScene("acquisitionSetting", null, null, (aData) => {
                        mSetting.mAcqusitionRate = aData.get<float>("rate");
                        setSetting(mSetting);
                    });
                    break;
                case "stageButtonPushed":
                    MySceneManager.openScene("stageSetting", null, null, (aData) => {
                        mSetting.mStageName = aData.get<string>("stage");
                        setSetting(mSetting);
                    });
                    break;
                //ai設定
                case "ai1ButtonPushed":
                    MySceneManager.openScene("aiSetting", null, null, (aData) => {
                        mSetting.mCharaData1.mAi = aData.get<string>("ai");
                        mSetting.mCharaData1.mAiName = aData.get<string>("name");
                        setSetting(mSetting);
                    });
                    break;
                case "ai2ButtonPushed":
                    MySceneManager.openScene("aiSetting", null, null, (aData) => {
                        mSetting.mCharaData2.mAi = aData.get<string>("ai");
                        mSetting.mCharaData2.mAiName = aData.get<string>("name");
                        setSetting(mSetting);
                    });
                    break;
                case "ai3ButtonPushed":
                    MySceneManager.openScene("aiSetting", null, null, (aData) => {
                        mSetting.mCharaData3.mAi = aData.get<string>("ai");
                        mSetting.mCharaData3.mAiName = aData.get<string>("name");
                        setSetting(mSetting);
                    });
                    break;
                case "ai4ButtonPushed":
                    MySceneManager.openScene("aiSetting", null, null, (aData) => {
                        mSetting.mCharaData4.mAi = aData.get<string>("ai");
                        mSetting.mCharaData4.mAiName = aData.get<string>("name");
                        setSetting(mSetting);
                    });
                    break;
                //キャラ設定
                case "chara1ButtonPushed":
                    MySceneManager.openScene("charaSetting",
                        new Arg(new Dictionary<string, object>() { { "ng", new string[3] { mSetting.mCharaData2.mFile, mSetting.mCharaData3.mFile, mSetting.mCharaData4.mFile } } }),
                        null, (aData) => {
                            mSetting.mCharaData1.mFile = aData.get<string>("file");
                            mSetting.mCharaData1.mName = aData.get<string>("name");
                            setSetting(mSetting);
                        });
                    break;
                case "chara2ButtonPushed":
                    MySceneManager.openScene("charaSetting",
                        new Arg(new Dictionary<string, object>() { { "ng", new string[3] { mSetting.mCharaData1.mFile, mSetting.mCharaData3.mFile, mSetting.mCharaData4.mFile } } }),
                        null, (aData) => {
                            mSetting.mCharaData2.mFile = aData.get<string>("file");
                            mSetting.mCharaData2.mName = aData.get<string>("name");
                            setSetting(mSetting);
                        });
                    break;
                case "chara3ButtonPushed":
                    MySceneManager.openScene("charaSetting",
                        new Arg(new Dictionary<string, object>() { { "ng", new string[3] { mSetting.mCharaData1.mFile, mSetting.mCharaData2.mFile, mSetting.mCharaData4.mFile } } }),
                        null, (aData) => {
                            mSetting.mCharaData3.mFile = aData.get<string>("file");
                            mSetting.mCharaData3.mName = aData.get<string>("name");
                            setSetting(mSetting);
                        });
                    break;
                case "chara4ButtonPushed":
                    MySceneManager.openScene("charaSetting",
                        new Arg(new Dictionary<string, object>() { { "ng", new string[3] { mSetting.mCharaData1.mFile, mSetting.mCharaData2.mFile, mSetting.mCharaData3.mFile } } }),
                        null, (aData) => {
                            mSetting.mCharaData4.mFile = aData.get<string>("file");
                            mSetting.mCharaData4.mName = aData.get<string>("name");
                            setSetting(mSetting);
                        });
                    break;
                case "startButtonPushed":
                    startGame();
                    break;
                default:
                    return;
            }
        }));
    }
    //設定内容を表示
    void setSetting(GameSetting aSetting) {
        setCharaSetting(aSetting.mCharaData1, GameObject.Find("charaData1").GetComponent<MyBehaviour>());
        setCharaSetting(aSetting.mCharaData2, GameObject.Find("charaData2").GetComponent<MyBehaviour>());
        setCharaSetting(aSetting.mCharaData3, GameObject.Find("charaData3").GetComponent<MyBehaviour>());
        setCharaSetting(aSetting.mCharaData4, GameObject.Find("charaData4").GetComponent<MyBehaviour>());
        GameObject.Find("initialMoneyRate").GetComponent<TextMesh>().text = "x" + aSetting.mInitialMoneyRate.ToString();
        GameObject.Find("feeRate").GetComponent<TextMesh>().text = "x" + aSetting.mFeeRate.ToString();
        if (aSetting.mAcqusitionRate <= 0) {
            GameObject.Find("acquisitionRate").GetComponent<TextMesh>().text = "なし";
        } else {
            GameObject.Find("acquisitionRate").GetComponent<TextMesh>().text = "x" + aSetting.mAcqusitionRate.ToString();
        }
        Arg tStageData = new Arg(MyJson.deserializeResourse("stage/data/" + aSetting.mStageName));
        GameObject.Find("stageName").GetComponent<TextMesh>().text = tStageData.get<string>("name");
    }
    //キャラの設定内容を表示
    void setCharaSetting(GameSetting.CharaData aData, MyBehaviour aDataDisplay) {
        if (aData.mFile == "none") {
            aDataDisplay.findChild<SpriteRenderer>("charaImg").sprite = null;
            aDataDisplay.findChild<TextMesh>("charaName").text = "なし";
            aDataDisplay.findChild<TextMesh>("none").color = new Color(1, 1, 1, 1);
        } else {
            aDataDisplay.findChild<SpriteRenderer>("charaImg").sprite = Resources.Load<Sprite>("sprites/chara/" + aData.mFile + "/" + aData.mFile);
            aDataDisplay.findChild<TextMesh>("charaName").text = aData.mName;
            aDataDisplay.findChild<TextMesh>("none").color = new Color(1, 1, 1, 0);
        }
        aDataDisplay.findChild<TextMesh>("aiName").text = aData.mAiName;
    }
    //ゲーム開始
    void startGame() {
        MySceneManager.changeScene("game", new Arg(new Dictionary<string, object>() { { "setting", mSetting } }));
    }
    private void OnDestroy() {
        Subject.removeObserver("settingMain");
    }
}
