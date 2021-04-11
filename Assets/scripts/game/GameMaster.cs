using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameMaster {
    public GameMainCamera mCamera;
    public GameFeild mFeild;
    public List<PlayerStatus> mPlayerStatus;
    public PlayerStatusMain mPlayerStatusMain;
    //ターンの順番
    public List<PlayerStatus> mTurnOrder;
    public int mTurnNum = -1;
    public TurnManager mTurnManager;

    public void start(GameFeild aFeild, List<PlayerStatus> aStatus, PlayerStatusMain aPlayerStatusMain) {
        mFeild = aFeild;
        mPlayerStatus = aStatus;
        mPlayerStatusMain = aPlayerStatusMain;

        decideTurnOrder();
        mCamera.shoot(mTurnOrder[0].mComa);
        moveComaToWaitePosition(() => {
            mPlayerStatusMain.sortInOrder(mTurnOrder, () => {
                mTurnManager = new TurnManager();
                mTurnManager.mMassEventManager = new MassEventManager();
                mTurnManager.mMassEventManager.mMaster = this;
                nextTurn();
            });
        });
    }
    //ターンの順番を決定する
    public void decideTurnOrder() {
        mTurnOrder = new List<PlayerStatus>();
        foreach (PlayerStatus tStatus in mPlayerStatus) {
            if (tStatus == null) continue;
            mTurnOrder.Add(tStatus);
        }
        //シャッフル
        mTurnOrder = mTurnOrder.OrderBy(a => Guid.NewGuid()).ToList();
    }
    public void nextTurn() {
        if (mTurnOrder.Count <= 1) {
            //ゲーム終了
            gameEnd();
            return;
        }
        mTurnNum = (mTurnNum + 1) % mTurnOrder.Count;
        while (mTurnOrder[mTurnNum].isEnd())
            mTurnNum = (mTurnNum + 1) % mTurnOrder.Count;

        mTurnManager.startTurn(mTurnOrder[mTurnNum], this, () => {
            nextTurn();
        });
    }
    //コマの待機位置調整
    public void moveComaToWaitePosition(Action aCallback) {
        CallbackSystem tSystem = new CallbackSystem();
        foreach (PlayerStatus tStatus in mTurnOrder) {
            if (tStatus.isEnd()) continue;
            int tDup = 0;
            int tOrd = 0;
            foreach (PlayerStatus tStatus2 in mTurnOrder) {
                if (tStatus2.isEnd()) continue;
                if (tStatus.mCurrentMassNumber != tStatus2.mCurrentMassNumber) continue;
                tDup++;
                if (tStatus.mPlayerNumber > tStatus2.mPlayerNumber)
                    tOrd++;
            }
            Action tCallback = tSystem.getCounter();
            tStatus.mComa.moveTo(mFeild.mMassList[tStatus.mCurrentMassNumber].position + getWaitePosition(tDup, tOrd), 0.2f, tCallback);
        }
        tSystem.then(aCallback);
    }
    //プレイヤのデータ表示更新
    public void updateStatus() {
        updatePropertyValueStatus();
        mPlayerStatusMain.updateStatus(mPlayerStatus);
    }
    //プレイヤの物件,総資産更新
    public void updatePropertyValueStatus() {
        foreach (PlayerStatus tStatus in mTurnOrder) {
            int tProperty = 0;
            foreach (GameMass tMass in mFeild.mMassList) {
                if (!(tMass is LandMass)) continue;
                LandMass tLand = (LandMass)tMass;
                if (tLand.mOwner != tStatus.mPlayerNumber) continue;
                tProperty += tLand.mTotalValue;
            }
            tStatus.mProperty = tProperty;
            tStatus.mAssets = tStatus.mMoney + tStatus.mProperty;
        }
    }
    /// <summary>
    /// 移動後の待機座標取得(マスからの相対座標)
    /// </summary>
    /// <param name="aDup">同じますにあるコマのかず</param>
    /// <param name="aOrd">同マス内でのコマの順番</param>
    /// <returns></returns>
    public Vector3 getWaitePosition(int aDup, int aOrd) {
        switch (aDup) {
            case 1:
                return new Vector3(0, 0, 0.2f);
            case 2:
                if (aOrd == 0) return new Vector3(-1, 0, 0.2f);
                else return new Vector3(1, 0, 0.2f);
            case 3:
                if (aOrd == 0) return new Vector3(-1, 0, -0.5f);
                else if (aOrd == 1) return new Vector3(0, 0, 0.2f);
                else return new Vector3(1, 0, -0.5f);
            case 4:
                if (aOrd == 0) return new Vector3(-1, 0, -0.5f);
                else if (aOrd == 1) return new Vector3(1, 0, -0.5f);
                else if (aOrd == 2) return new Vector3(-1, 0, 0.2f);
                else return new Vector3(1, 0, 0.2f);
        }
        throw new Exception("不正な配置 : " + aDup + " " + aOrd);
    }
    public void gameover(int aPlayerNumber, Action aCallback) {
        //ターン順の配列から取り除く
        mTurnOrder.Remove(mPlayerStatus[aPlayerNumber - 1]);
        //コマを消す
        mPlayerStatus[aPlayerNumber - 1].mComa.gameObject.SetActive(false);
        aCallback();
    }
    //ゲーム終了
    public void gameEnd() {
        Debug.Log("finish");
    }
}
