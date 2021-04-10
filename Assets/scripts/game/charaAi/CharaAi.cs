using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class CharaAi {
    abstract public void openDice(DiceMain aDiceMain);
    abstract public void endOpenDice();

    abstract public void purchaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback);
    abstract public void increaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback);
}
