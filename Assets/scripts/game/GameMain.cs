using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
    static public float mFeeRate;
    static public float mAcquisitionRate;

    public Arg mArg;
    public GameSetting mSetting;
    public Arg mStageData;
    public GameMaster mMaster;
    // Start is called before the first frame update
    void Start() {
        mArg = MySceneManager.getArg("game");
        mSetting = mArg.get<GameSetting>("setting");
        mStageData = new Arg(MyJson.deserializeResourse("stage/data/" + mSetting.mStageName));
        mFeeRate = mSetting.mFeeRate;
        mAcquisitionRate = mSetting.mAcqusitionRate;

        GameFeild tFeild = GameFeildFactory.create(mStageData);
        List<PlayerStatus> tStatus = PlayerFactory.create(mSetting, tFeild, mStageData);
        MySceneManager.openScene("playerStatus",null,(aScene)=> {
            PlayerStatusMain tMain = GameObject.Find("playerStatusMain").GetComponent<PlayerStatusMain>();
            tMain.initialize(tStatus);
            mMaster = new GameMaster();
            mMaster.start(tFeild, tStatus, tMain);
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
