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
                switch (tEvent.mEventType) {
                    case "heart":
                        runHeartEvent(aTurnPlayer, aCallback);
                        return;
                    case "bat":
                        runBatEvent(aTurnPlayer, aCallback);
                        return;
                    case "god":
                        runGodEvent(aTurnPlayer, aCallback);
                        return;
                }
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
    //金取得
    public void getCoin(PlayerStatus aPlayer,int aMoney,Action aCallback) {
        mMaster.mCamera.mTarget = aPlayer.mComa;
        GameEffector.getCoin(aPlayer.mComa.worldPosition, "+" + aMoney.ToString(), () => {
            aPlayer.mMoney += aMoney;
            aCallback();
        });
    }
    public void getCoinAll(List<int> aMoney, Action aCallback) {
        List<PlayerStatus> tPlayers = mMaster.mTurnOrder;
        Action<int> tGet = null;
        tGet = (aNum) => {
            if (aMoney.Count <= aNum) {
                aCallback();
                return;
            }
            if (aMoney[aNum] == 0) {
                tGet(aNum + 1);
                return;
            }
            getCoin(tPlayers[aNum], aMoney[aNum], () => {
                tGet(aNum + 1);
            });
        };
        tGet(0);
    }
    //金損失
    public void lostCoin(PlayerStatus aPlayer, int aMoney, Action aCallback) {
        mMaster.mCamera.mTarget = aPlayer.mComa;
        GameEffector.lostCoin(aPlayer.mComa.worldPosition, (-aMoney).ToString(), () => {
            aPlayer.mMoney -= aMoney;
            aCallback();
        });
    }
    public void lostCoinAll(List<int> aMoney, Action aCallback) {
        List<PlayerStatus> tPlayers = mMaster.mTurnOrder;
        Action<int> tLost = null;
        tLost = (aNum) => {
            if (aMoney.Count <= aNum) {
                aCallback();
                return;
            }
            if (aMoney[aNum] == 0) {
                tLost(aNum + 1);
                return;
            }
            lostCoin(tPlayers[aNum], aMoney[aNum], () => {
                tLost(aNum + 1);
            });
        };
        tLost(0);
    }
}
