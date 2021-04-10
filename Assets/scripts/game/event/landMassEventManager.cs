using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MassEventManager {
    public void stopLand(PlayerStatus aTurnPlayer, Action aCallback) {
        LandMass tLand = (LandMass)mMaster.mFeild.mMassList[aTurnPlayer.mCurrentMassNumber];
        if (tLand.mOrner <= 0) stopFreeLand(aTurnPlayer, aCallback);
        else if (tLand.mOrner == aTurnPlayer.mPlayerNumber) stopMyLand(aTurnPlayer, aCallback);
        else stopOtherLand(aTurnPlayer, aCallback);
    }
    //空き地に止まった
    public void stopFreeLand(PlayerStatus aTurnPlayer, Action aCallback) {
        LandMass tLand = (LandMass)mMaster.mFeild.mMassList[aTurnPlayer.mCurrentMassNumber];
        //購入できない
        if (aTurnPlayer.mMoney < tLand.mPurchaseCost) {
            aCallback();
            return;
        }
        //購入できる
        aTurnPlayer.mAi.purchaseLand(aTurnPlayer, tLand, mMaster, (aAns) => {
            if (!aAns) {
                aCallback();
                return;
            }
            //購入する
            GameEffector.lostCoin(aTurnPlayer.mComa.position, (-tLand.mPurchaseCost).ToString(), () => {
                aTurnPlayer.mMoney -= tLand.mPurchaseCost;
                tLand.changeOrner(aTurnPlayer, () => {
                    tLand.changeIncreaseLevel(tLand.mIncreaseLevel, () => {
                        mMaster.updateStatus();
                        aCallback();
                        return;
                    });
                });
            });
        });
    }
    //自分の土地に止まった
    public void stopMyLand(PlayerStatus aTurnPlayer, Action aCallback) {
        LandMass tLand = (LandMass)mMaster.mFeild.mMassList[aTurnPlayer.mCurrentMassNumber];
        //最大まで増資済み
        if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) {
            aCallback();
            return;
        }
        //増資できない
        if (aTurnPlayer.mMoney < tLand.mIncreaseCost) {
            aCallback();
            return;
        }
        //増資できる
        aTurnPlayer.mAi.increaseLand(aTurnPlayer, tLand, mMaster, (aAns) => {
            if (!aAns) {
                aCallback();
                return;
            }
            //増資する
            GameEffector.lostCoin(aTurnPlayer.mComa.position, (-tLand.mIncreaseCost).ToString(), () => {
                aTurnPlayer.mMoney -= tLand.mIncreaseCost;
                tLand.changeIncreaseLevel(tLand.mIncreaseLevel + 1,()=> {
                    mMaster.updateStatus();
                    aCallback();
                    return;
                });
            });
        });
    }
    //自分以外の土地に止まった
    public void stopOtherLand(PlayerStatus aTurnPlayer, Action aCallback) {
        LandMass tLand = (LandMass)mMaster.mFeild.mMassList[aTurnPlayer.mCurrentMassNumber];
        GameEffector.lostCoin(aTurnPlayer.mComa.position, (-tLand.mFeeCost).ToString(), () => {
            aTurnPlayer.mMoney -= tLand.mFeeCost;
            PlayerStatus tStatus = mMaster.mPlayerStatus[tLand.mOrner - 1];
            GameEffector.getCoin(tStatus.mComa.position, tLand.mFeeCost.ToString(), () => {
                tStatus.mMoney += tLand.mFeeCost;
                mMaster.updateStatus();
                aCallback();
                return;
            });
        });
    }
}
