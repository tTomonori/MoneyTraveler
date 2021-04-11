using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MassEventManager {
    public void runBatEvent(PlayerStatus aTurnPlayer, Action aCallback) {
        List<(Action<PlayerStatus, Action>, float)> tEventList = new List<(Action<PlayerStatus, Action>, float)>();
        tEventList.Add((thiefInvades, 10));
        tEventList.Add((goHospital, 10));
        pickEvent(tEventList)(aTurnPlayer, aCallback);
    }
    //物件に泥棒が侵入
    public void thiefInvades(PlayerStatus aTurnPlayer, Action aCallback) {
        int tMoney = (int)(aTurnPlayer.mProperty * UnityEngine.Random.Range(3f, 7f) / 100);
        showEventBox("物件に泥棒が侵入\n+" + (-tMoney).ToString() + "金", () => {
            lostCoin(aTurnPlayer, tMoney, () => {
                mMaster.updateStatus();
                aCallback();
            });
        });
    }
    //怪我をして病院へ
    public void goHospital(PlayerStatus aTurnPlayer, Action aCallback) {
        int tMoney = (int)(70 * (1f + aTurnPlayer.mOrbit / 5f) * UnityEngine.Random.Range(1f, 3f));
        showEventBox("怪我をして病院へ\n" + (-tMoney).ToString() + "金", () => {
            lostCoin(aTurnPlayer, tMoney, () => {
                mMaster.updateStatus();
                aCallback();
            });
        });
    }
}
