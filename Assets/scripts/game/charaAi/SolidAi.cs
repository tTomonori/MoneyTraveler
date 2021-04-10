using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidAi : CpuAi {
    public override void purchaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //手持ちの金が料金の平均を下回らなければ購入する
        int tAve = getFeeAverage(aMyStatus, mMaster.mFeild);
        if (aMyStatus.mMoney - aLand.mPurchaseCost < tAve) {
            aCallback(false);
            return;
        }
        aCallback(true);
    }
    public override void increaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //手持ちの金が料金の平均を下回らなければ増資する
        int tAve = getFeeAverage(aMyStatus, mMaster.mFeild);
        if (aMyStatus.mMoney - aLand.mIncreaseCost < tAve) {
            aCallback(false);
            return;
        }
        aCallback(true);
    }
    //自分以外の土地の料金の平均を求める
    public int getFeeAverage(PlayerStatus aMyStatus,GameFeild aFeild) {
        int tTotalFee = 0;
        int tTotalNum = 0;
        foreach(GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOrner == aMyStatus.mPlayerNumber) continue;
            tTotalFee += tLand.mFeeCost;
            tTotalNum++;
        }
        if (tTotalFee == 0) return 0;
        return tTotalFee / tTotalNum;
    }
}
