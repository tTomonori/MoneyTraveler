using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MassEventManager {
    public void runHeartEvent(PlayerStatus aTurnPlayer, Action aCallback) {
        List<(Action<PlayerStatus, Action>, float)> tEventList = new List<(Action<PlayerStatus, Action>, float)>();
        tEventList.Add((winLottery, 10));
        tEventList.Add((findDent, 10));
        pickEvent(tEventList)(aTurnPlayer, aCallback);
    }
    //宝くじが当たった
    public void winLottery(PlayerStatus aTurnPlayer, Action aCallback) {
        int tMoney = (int)(70 * (1f + aTurnPlayer.mOrbit / 5f) * UnityEngine.Random.Range(1f, 3f));
        showEventBox("宝くじが当たった\n+" + tMoney.ToString() + "金", () => {
            getCoin(aTurnPlayer, tMoney, () => {
                mMaster.updateStatus();
                aCallback();
            });
        });
    }
    //へそくりを見つけた
    public void findDent(PlayerStatus aTurnPlayer, Action aCallback) {
        int tMoney = (int)(aTurnPlayer.mAssets * UnityEngine.Random.Range(5f, 20f) / 100);
        showEventBox("へそくりを見つけた\n+" + tMoney.ToString() + "金", () => {
            getCoin(aTurnPlayer, tMoney, () => {
                mMaster.updateStatus();
                aCallback();
            });
        });
    }
}
