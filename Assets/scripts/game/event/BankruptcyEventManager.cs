using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class BankruptcyEventManager {
    //破産したプレイヤがいるかチェック
    static public void checkBankruptcy(GameMaster aMaster, Action aCallback) {
        checkBankruptcy(1, aMaster, () => {
            checkBankruptcy(2, aMaster, () => {
                checkBankruptcy(3, aMaster, () => {
                    checkBankruptcy(4, aMaster, () => {
                        aCallback();
                    });
                });
            });
        });
    }
    static public void checkBankruptcy(int aPlayerNumber, GameMaster aMaster, Action aCallback) {
        PlayerStatus tStatus = aMaster.mPlayerStatus[aPlayerNumber - 1];
        if (tStatus == null) {//プレイヤなし
            aCallback();
            return;
        }
        if (tStatus.isEnd()) {//ゲームオーバー済み
            aCallback();
            return;
        }
        if (tStatus.mMoney >= 0) {//破産していない
            aCallback();
            return;
        }
        //破産している
        bankruptcy(aPlayerNumber, aMaster, aCallback);
    }
    static public void bankruptcy(int aPlayerNumber, GameMaster aMaster, Action aCallback) {
        PlayerStatus tStatus = aMaster.mPlayerStatus[aPlayerNumber - 1];
        if (tStatus.mProperty <= 0) {
            //ゲームオーバー
            aMaster.gameover(aPlayerNumber, aCallback);
            return;
        }
        //土地を売却
        tStatus.mAi.soldLand(tStatus, aMaster, (aLand) => {
            soldLand(aLand, aMaster, () => {
                if (tStatus.mMoney >= 0) {
                    //破産回避
                    aCallback();
                    return;
                }
                //まだ破産状態
                bankruptcy(aPlayerNumber, aMaster, aCallback);
            });
        });
    }
    static public void soldLand(LandMass aLand, GameMaster aMaster, Action aCallback) {
        PlayerStatus tOwner = aMaster.mPlayerStatus[aLand.mOwner - 1];
        aLand.changeOrner(null, () => {});
        aMaster.mCamera.mTarget = aLand;
        GameEffector.lostCoin(aLand.worldPosition, "", () => {
            aMaster.mCamera.mTarget = tOwner.mComa;
            GameEffector.getCoin(tOwner.mComa.worldPosition, "+" + aLand.mSellCost.ToString(), () => {
                tOwner.mMoney += aLand.mSellCost;
                aMaster.updateStatus();
                aCallback();
            });
        });
    }
}
