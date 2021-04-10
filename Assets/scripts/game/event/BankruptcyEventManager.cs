using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class BankruptcyEventManager {
    //破産したプレイヤがいるかチェック
    static public void checkBankruptcy(GameMaster aMaster,Action aCallback) {
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
    static public void checkBankruptcy(int aPlayerNumber,GameMaster aMaster,Action aCallback) {
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

    }
}
