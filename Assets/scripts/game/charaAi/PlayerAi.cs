using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAi : CharaAi {
    public override void openDice(DiceMain aDiceMain) {
        Subject.addObserver(new Observer("playerAi", (aMessage) => {
            switch (aMessage.name) {
                case "diceBackPushed":
                    aDiceMain.open();
                    break;
                case "dice1Pushed":
                    aDiceMain.open1();
                    break;
                case "dice2Pushed":
                    aDiceMain.open2();
                    break;
                case "dice3Pushed":
                    aDiceMain.open3();
                    break;
            }
        }));
    }
    public override void endOpenDice() {
        Subject.removeObserver("playerAi");
    }

    public override void purchaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        MySceneManager.openScene("twoChoicesQuestion",
            new Arg(new Dictionary<string, object>() { { "text", aLand.mPurchaseCost.ToString() + "金で" + aLand.mNameMesh.text + "を\n購入しますか" } }),
            null, (aArg) => {
                aCallback(aArg.get<bool>("answer"));
            });
    }
    public override void increaseLand(PlayerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        MySceneManager.openScene("twoChoicesQuestion",
            new Arg(new Dictionary<string, object>() { { "text", aLand.mIncreaseCost.ToString() + "金で" + aLand.mNameMesh.text + "を\n増資しますか" } }),
            null, (aArg) => {
                aCallback(aArg.get<bool>("answer"));
            });
    }
    public override void soldLand(PlayerStatus aMyStatus, GameMaster mMaster, Action<LandMass> aCallback) {
        mMaster.mCamera.mTarget = null;
        Subject.addObserver(new Observer("playerAi", (aMessage) => {
            switch (aMessage.name) {
                case "gamePadDragged":
                    Vector2 tVec = aMessage.getParameter<Vector2>("vector") / -10;
                    mMaster.mCamera.move(tVec);
                    break;
                case "gamePadClicked":
                    GameMass tMass = aMessage.getParameter<GameMass>("mass");
                    if (!(tMass is LandMass)) break;
                    LandMass tLand = (LandMass)tMass;
                    if (tLand.mOwner != aMyStatus.mPlayerNumber) break;
                    Subject.removeObserver("playerAi");
                    aCallback(tLand);
                    return;
            }
        }));
    }
}
