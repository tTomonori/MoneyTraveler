using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class PlayerFactory {
    static public List<PlayerStatus> create(GameSetting aSetting,GameFeild aFeild,Arg aStageData) {
        List<PlayerStatus> tStatusList = new List<PlayerStatus>();
        List<GameSetting.CharaData> tCharaDataList = new List<GameSetting.CharaData>();
        tCharaDataList.Add(aSetting.mCharaData1);
        tCharaDataList.Add(aSetting.mCharaData2);
        tCharaDataList.Add(aSetting.mCharaData3);
        tCharaDataList.Add(aSetting.mCharaData4);
        for (int i = 0; i < 4; i++) {
            GameSetting.CharaData tCharaData = tCharaDataList[i];
            if (tCharaData.mFile == "none") {
                tStatusList.Add(null);
                continue;
            }
            PlayerStatus tStatus = new PlayerStatus();
            //ai
            switch (tCharaData.mAi) {
                case "player":
                    tStatus.mAi = new PlayerAi();
                    break;
                case "solid":
                case "carefully":
                case "impulse":
                    tStatus.mAi = new SolidAi();
                    break;
            }
            //chara
            tStatus.mCharaFile = tCharaData.mFile;
            tStatus.mCharaName = tCharaData.mName;
            //金
            tStatus.mMoney = (int)(aStageData.get<int>("initialMoney") * aSetting.mInitialMoneyRate);
            tStatus.mProperty = 0;
            tStatus.mAssets = tStatus.mMoney;

            tStatus.mRank = 1;
            tStatus.mOrbit = 1;
            tStatus.mPlayerNumber = i + 1;
            //coma
            int tStartMassNumber = getStartMassNumber(aFeild, i + 1);
            tStatus.mCurrentMassNumber = tStartMassNumber;
            tStatus.mComa = GameObject.Instantiate(Resources.Load<PlayerComa>("prefabs/game/player/coma"));
            tStatus.mComa.mImg.sprite = Resources.Load<Sprite>("sprites/chara/" + tCharaData.mFile + "/" + tCharaData.mFile);
            tStatus.mComa.name = "coma : " + tStatus.mCharaFile;
            tStatus.mComa.position = aFeild.mMassList[tStartMassNumber].worldPosition;
            tStatus.mComa.transform.SetParent(aFeild.mComaContainer.transform, true);

            tStatusList.Add(tStatus);
        }
        return tStatusList;
    }
    static public int getStartMassNumber(GameFeild aFeild,int aPlayerNumber) {
        for(int i = 0; i < aFeild.mMassList.Count; i++) {
            if (aFeild.mMassList[i] is StartMass) return i;
        }
        return 0;
    }
}
