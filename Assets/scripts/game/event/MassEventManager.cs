using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MassEventManager {
    public GameMaster mMaster;
    //マス通過イベント実行
    public void runPassEvent(PlayerStatus aTurnPlayer,Action aCallback) {
        GameMass tMass = mMaster.mFeild.mMassList[aTurnPlayer.mCurrentMassNumber];
        switch (tMass) {
            case StartMass tStart:
                getOrbitBounus(aTurnPlayer, aCallback);
                return;
        }
        aCallback();
    }
    //マス到達イベント実行
    public void runStopEvent(PlayerStatus aTurnPlayer, Action aCallback) {
        GameMass tMass = mMaster.mFeild.mMassList[aTurnPlayer.mCurrentMassNumber];
        switch (tMass) {
            case LandMass tLand:
                stopLand(aTurnPlayer, aCallback);
                return;
            case EventMass tEvent:
                break;
            case StartMass tStart:
                getOrbitBounus(aTurnPlayer, aCallback);
                return;
        }
        aCallback();
    }
    //周回ボーナス取得
    public void getOrbitBounus(PlayerStatus aTurnPlayer, Action aCallback) {
        GameEffector.getCoin(aTurnPlayer.mComa.position, "+200", () => {
            aTurnPlayer.mMoney += 200;
            aTurnPlayer.mOrbit++;
            mMaster.updateStatus();
            aCallback();
        });
    }
}
