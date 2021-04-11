using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class GameEffector {
    static public void lostCoin(Vector3 aPosition, string aLabel, Action aCallback) {
        MyBehaviour tContainer = MyBehaviour.create<MyBehaviour>();
        tContainer.name = "lostCoin";
        tContainer.position = aPosition;

        MySoundPlayer.playSe("lost", false);

        CallbackSystem tSystem = new CallbackSystem();
        KinCoin tPrefab = Resources.Load<KinCoin>("prefabs/game/effect/coin");
        for (int i = 0; i < 10; i++) {
            Action tCounter = tSystem.getCounter();
            MyBehaviour.setTimeoutToIns(0.1f * i, () => {
                KinCoin tCoin = GameObject.Instantiate(tPrefab);
                tCoin.transform.SetParent(tContainer.transform, false);
                tCoin.lost(tCounter);
            });
        }
        tSystem.then(aCallback);
    }
    static public void getCoin(Vector3 aPosition, string aLabel, Action aCallback) {
        MyBehaviour tContainer = MyBehaviour.create<MyBehaviour>();
        tContainer.name = "getCoin";
        tContainer.position = aPosition;

        MySoundPlayer.playSe("get", false);

        CallbackSystem tSystem = new CallbackSystem();
        KinCoin tPrefab = Resources.Load<KinCoin>("prefabs/game/effect/coin");
        for (int i = 0; i < 7; i++) {
            Action tCounter = tSystem.getCounter();
            MyBehaviour.setTimeoutToIns(0.1f * i, () => {
                KinCoin tCoin = GameObject.Instantiate(tPrefab);
                tCoin.transform.SetParent(tContainer.transform, false);
                tCoin.get(tCounter);
            });
        }
        tSystem.then(aCallback);
    }
}
