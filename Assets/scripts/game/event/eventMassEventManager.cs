using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class MassEventManager {
    //リストの中から実行するイベントを決める
    public Action<PlayerStatus, Action> pickEvent(List<(Action<PlayerStatus, Action>, float)> aEventList) {
        float tTotalWeight = 0;
        foreach ((Action<PlayerStatus, Action>, float) tTwople in aEventList) {
            tTotalWeight += tTwople.Item2;
        }
        float tTargetWeight = UnityEngine.Random.Range(0, tTotalWeight);
        foreach ((Action<PlayerStatus, Action>, float) tTwople in aEventList) {
            tTargetWeight -= tTwople.Item2;
            if (tTargetWeight <= 0) {
                return tTwople.Item1;
            }
        }
        return null;
    }
    //イベント情報を表示
    public void showEventBox(string aText, Action aCallback) {
        MySceneManager.openScene("eventBox", new Arg(new Dictionary<string, object>() { { "text", aText } }), null, (aArg)=> {
            aCallback();
        });
    }
}
