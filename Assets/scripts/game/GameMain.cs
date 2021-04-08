using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
    static public float mFeeRate;
    static public float mAcquisitionRate;

    public Arg mArg;
    public GameSetting mSetting;
    public Arg mStageData;
    // Start is called before the first frame update
    void Start() {
        mArg = MySceneManager.getArg("game");
        mSetting = mArg.get<GameSetting>("setting");
        mStageData = new Arg(MyJson.deserializeResourse("stage/data/" + mSetting.mStageName));
        mFeeRate = mSetting.mFeeRate;
        mAcquisitionRate = mSetting.mAcqusitionRate;

        GameFeildFactory.create(mStageData);
    }

    // Update is called once per frame
    void Update() {

    }
}
