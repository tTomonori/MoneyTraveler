using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CpuAi : CharaAi {
    public override void openDice(DiceMain aDiceMain) {
        MyBehaviour.setTimeoutToIns(0.2f, aDiceMain.open1);
        MyBehaviour.setTimeoutToIns(0.6f, aDiceMain.open2);
        MyBehaviour.setTimeoutToIns(1f, aDiceMain.open3);
    }
    public override void endOpenDice() {
    }
}
