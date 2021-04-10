using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager {
    static float mMoveSpeed = 10;
    public GameMaster mMaster;
    public PlayerStatus mTurnPlayer;
    public MassEventManager mMassEventManager;
    //ターン開始
    public void startTurn(PlayerStatus aStatus, GameMaster aMaster, Action aCallback) {
        mMaster = aMaster;
        mTurnPlayer = aStatus;
        mMaster.mCamera.mTarget = aStatus.mComa;
        //diceで移動マス数を決定
        Action<DiceMain> tSetted = (aDicMain) => {
            aStatus.mAi.openDice(aDicMain);
        };
        Action<int> tOpened = (aNumber) => {
            aStatus.mAi.endOpenDice();
            MySceneManager.closeScene("dice");
            move(aNumber, aCallback);
        };
        MySceneManager.openScene("dice", new Arg(new Dictionary<string, object>() { { "setted", tSetted }, { "opened", tOpened } }));
    }
    //移動する
    public void move(int aNumber, Action aCallback) {
        if (aNumber <= 0) {
            aCallback();
            return;
        }
        //残り移動距離表示
        mTurnPlayer.mComa.displayNumber(aNumber.ToString());
        //移動
        moveNextMass(() => {
            if (aNumber > 1) {
                //残り移動距離更新
                mTurnPlayer.mComa.displayNumber((aNumber - 1).ToString());
                //通過イベント
                mMassEventManager.runPassEvent(mTurnPlayer,() => {
                    checkBankruptcy(() => {
                        if (mTurnPlayer.mMoney < 0) {
                            //破産した
                            aCallback();
                            return;
                        }
                        move(aNumber - 1, aCallback);
                    });
                });
            } else {
                mMaster.moveComaToWaitePosition(() => {
                    //到達イベント
                    mTurnPlayer.mComa.displayNumber("");
                    mMassEventManager.runStopEvent(mTurnPlayer,()=> {
                        checkBankruptcy(() => {
                            aCallback();
                        });
                    });
                });
            }
        });
    }
    //次のマスに移動する
    public void moveNextMass(Action aCallback) {
        int tNextMassNum = (mTurnPlayer.mCurrentMassNumber + 1) % mMaster.mFeild.mMassList.Count;
        mTurnPlayer.mComa.moveToWithSpeed(mMaster.mFeild.mMassList[tNextMassNum].position, mMoveSpeed, () => {
            mTurnPlayer.mCurrentMassNumber = tNextMassNum;
            MySoundPlayer.playSe("step", false);
            aCallback();
        });
    }
    //破産したプレイヤがいるかチェック
    public void checkBankruptcy(Action aCallback) {
        BankruptcyEventManager.checkBankruptcy(mMaster, aCallback);
    }
}
