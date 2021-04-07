using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting {
    public CharaData mCharaData1;
    public CharaData mCharaData2;
    public CharaData mCharaData3;
    public CharaData mCharaData4;
    public float mInitialMoneyRate;
    public float mFeeRate;
    public float mAcqusitionRate;
    public string mStageName;
    public GameSetting() {
        mCharaData1 = new CharaData();
        mCharaData1.mFile = "marie";
        mCharaData1.mName = "マリー";
        mCharaData1.mAi = "player";
        mCharaData1.mAiName = "プレイヤー";

        mCharaData2 = new CharaData();
        mCharaData2.mFile = "rear";
        mCharaData2.mName = "リア";
        mCharaData2.mAi = "solid";
        mCharaData2.mAiName = "堅実";

        mCharaData3 = new CharaData();
        mCharaData3.mFile = "maru";
        mCharaData3.mName = "マル";
        mCharaData3.mAi = "solid";
        mCharaData3.mAiName = "堅実";

        mCharaData4 = new CharaData();
        mCharaData4.mFile = "chiara";
        mCharaData4.mName = "キアラ";
        mCharaData4.mAi = "solid";
        mCharaData4.mAiName = "堅実";

        mInitialMoneyRate = 1;
        mFeeRate = 1;
        mAcqusitionRate = 2;
        mStageName = "standard";
    }
    public class CharaData {
        public string mFile;
        public string mName;
        public string mAi;
        public string mAiName;
    }
}
