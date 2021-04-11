using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseAi : CpuAi {
    public override void purchaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        aCallback(true);
    }
    public override void increaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        aCallback(true);
    }
}
