using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeild : MyBehaviour {
    public List<GameMass> mMassList;
    public MyBehaviour mMassContainer;
    public MyBehaviour mRouteContainer;
    public MyBehaviour mComaContainer;
    public MyBehaviour mWallContainer;
    public float mNorth;
    public float mEast;
    public float mSouth;
    public float mWest;
    public float mHeight;
    void Awake() {
        mMassList = new List<GameMass>();
        mMassContainer = this.createChild<MyBehaviour>("massContainer");
        mRouteContainer = this.createChild<MyBehaviour>("routeContainer");
        mComaContainer = this.createChild<MyBehaviour>("comaContainer");
        mWallContainer = this.createChild<MyBehaviour>("wallContainer");
    }
    //指定したプレイヤが所有している土地のリストを返す
    public List<LandMass> getOwnedLand(int aPlayerNumber) {
        List<LandMass> tLands = new List<LandMass>();
        foreach(GameMass tMass in mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner != aPlayerNumber) continue;
            tLands.Add(tLand);
        }
        return tLands;
    }
}
