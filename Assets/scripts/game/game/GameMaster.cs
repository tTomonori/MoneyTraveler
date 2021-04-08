using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameMaster {
    public GameFeild mFeild;
    public List<PlayerStatus> mPlayerStatus;
    public PlayerStatusMain mPlayerStatusMain;
    //ターンの順番
    public List<PlayerStatus> mTurnOrder;

    public void start(GameFeild aFeild, List<PlayerStatus> aStatus,PlayerStatusMain aPlayerStatusMain) {
        mFeild = aFeild;
        mPlayerStatus = aStatus;
        mPlayerStatusMain = aPlayerStatusMain;

        decideTurnOrder();
        mPlayerStatusMain.sortInOrder(mTurnOrder,()=> {

        });
    }
    //ターンの順番を決定する
    public void decideTurnOrder() {
        mTurnOrder = new List<PlayerStatus>();
        foreach(PlayerStatus tStatus in mPlayerStatus) {
            if (tStatus == null) continue;
            mTurnOrder.Add(tStatus);
        }
        //シャッフル
        mTurnOrder = mTurnOrder.OrderBy(a => Guid.NewGuid()).ToList();
    }
}
