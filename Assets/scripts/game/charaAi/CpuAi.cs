using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CpuAi : CharaAi {
    public override void openDice(DiceMain aDiceMain) {
        MyBehaviour.setTimeoutToIns(0.2f, aDiceMain.open1);
        MyBehaviour.setTimeoutToIns(0.6f, aDiceMain.open2);
        MyBehaviour.setTimeoutToIns(1f, aDiceMain.open3);
    }
    public override void endOpenDice() {
    }
    public override void soldLand(PlayerStatus aMyStatus, GameMaster mMaster, Action<LandMass> aCallback) {
        //価値が最も低い土地を売る
        LandMass tCheapest = null;
        foreach(LandMass tLand in mMaster.mFeild.getOwnedLand(aMyStatus.mPlayerNumber)) {
            if (tCheapest == null) {
                tCheapest = tLand;
                continue;
            }
            if (tCheapest.mTotalValue < tLand.mTotalValue) {
                tCheapest = tLand;
            }
        }
        aCallback(tCheapest);
    }
}
