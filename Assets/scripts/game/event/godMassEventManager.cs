using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MassEventManager{
    public void runGodEvent(PlayerStatus aTurnPlayer, Action aCallback) {
        List<(Action<PlayerStatus, Action>, float)> tEventList = new List<(Action<PlayerStatus, Action>, float)>();
        tEventList.Add((gotGrace, 6));
        tEventList.Add((hugeEarthquake, 10));
        pickEvent(tEventList)(aTurnPlayer, aCallback);
    }
    //神の恵み
    public void gotGrace(PlayerStatus aTurnPlayer, Action aCallback) {
        int tMoney = (int)(100 * (1f+aTurnPlayer.mOrbit / 4f) * UnityEngine.Random.Range(0.7f, 2f));
        List<int> tMoneys = new List<int>();
        foreach (PlayerStatus aStatus in mMaster.mTurnOrder)
            tMoneys.Add(tMoney);
        showEventBox("神からの恵み\n全員+" + tMoney.ToString() + "金", () => {
            getCoinAll(tMoneys, () => {
                mMaster.updateStatus();
                aCallback();
            });
        });
    }
    //巨大地震
    public void hugeEarthquake(PlayerStatus aTurnPlayer, Action aCallback) {
        int tRate = UnityEngine.Random.Range(3, 7);
        List<int> tLosts = new List<int>();
        foreach (PlayerStatus aStatus in mMaster.mTurnOrder)
            tLosts.Add(aStatus.mProperty*tRate/100);
        showEventBox("巨大地震が発生\n全員物件の"+tRate.ToString()+"%の被害", () => {
            lostCoinAll(tLosts, () => {
                mMaster.updateStatus();
                aCallback();
            });
        });
    }
}
