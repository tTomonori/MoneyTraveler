using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatusMain : MonoBehaviour {
    //ステータスの表示欄(なしに指定したプレイヤの部分にはnullが入っている)
    public List<PlayerStatusDisplay> mDisplays;
    //表示場所
    public Vector2 getDisplayPosition(int aNumber) {
        return new Vector2(-5.5f + 3.66f * aNumber, -3.7f);
    }
    //情報の初期表示
    public void initialize(List<PlayerStatus> aStatus) {
        mDisplays = new List<PlayerStatusDisplay>();
        foreach(PlayerStatus tStatus in aStatus) {
            if (tStatus == null) {
                mDisplays.Add(null);
                continue;
            }
            PlayerStatusDisplay tDisplay = GameObject.Instantiate(Resources.Load<PlayerStatusDisplay>("prefabs/game/player/playerStatusDisplay"));
            tDisplay.setStatus(tStatus);
            tDisplay.position2D = getDisplayPosition(tStatus.mPlayerNumber - 1);
            tDisplay.positionZ = tStatus.mPlayerNumber;
            mDisplays.Add(tDisplay);
        }
    }
    //情報更新
    public void updateStatus(List<PlayerStatus> aStatus) {
        foreach(PlayerStatus tStatus in aStatus) {
            if (tStatus == null) continue;
            mDisplays[tStatus.mPlayerNumber - 1].updateStatus(tStatus);
        }
    }
    //ターンの順番に並び替え
    public void sortInOrder(List<PlayerStatus> aStatus,Action aCallback) {
        CallbackSystem tSystem = new CallbackSystem();
        for(int i = 0; i < aStatus.Count; i++) {
            PlayerStatus tStatus = aStatus[i];
            Action tCounter = tSystem.getCounter();
            mDisplays[tStatus.mPlayerNumber - 1].moveTo(getDisplayPosition(i),0.5f,()=> {
                tCounter();
            });
        }
        tSystem.then(aCallback);
    }
}
