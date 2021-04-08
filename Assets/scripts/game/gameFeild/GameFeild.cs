using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeild : MyBehaviour {
    public List<GameMass> mMassList;
    public MyBehaviour mMassContainer;
    public MyBehaviour mRouteContainer;
    public MyBehaviour mComaContainer;
    void Awake() {
        mMassList = new List<GameMass>();
        mMassContainer = this.createChild<MyBehaviour>("massContainer");
        mRouteContainer = this.createChild<MyBehaviour>("routeContainer");
        mComaContainer = this.createChild<MyBehaviour>("comaContainer");
    }
}
