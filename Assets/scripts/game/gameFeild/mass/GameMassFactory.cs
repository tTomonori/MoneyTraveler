using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class GameMassFactory {
    static public GameMass create(Arg aMassData) {
        switch (aMassData.get<string>("type")) {
            case "land":
                LandMass tLand = GameObject.Instantiate(Resources.Load<LandMass>("prefabs/game/mass/landMass"));
                tLand.mNameMesh.text = aMassData.get<string>("name");
                tLand.name = aMassData.get<string>("name");
                tLand.worldPosition = vector2ToPosition(aMassData.get<Vector2>("position"));
                tLand.mBaseValue = aMassData.get<int>("value");
                tLand.mBuildingRenderer.sprite = null;
                //属性
                tLand.mAttributes = aMassData.get<List<string>>("attribute");
                if (tLand.mAttributes[0] == "none") {
                    tLand.mAttribute1.sprite = null;
                } else {
                    tLand.mAttribute1.sprite = Resources.Load<Sprite>("sprites/feild/attribute/" + tLand.mAttributes[0]);
                }
                if (tLand.mAttributes[1] == "none") {
                    tLand.mAttribute2.sprite = null;
                } else {
                    tLand.mAttribute2.sprite = Resources.Load<Sprite>("sprites/feild/attribute/" + tLand.mAttributes[1]);
                }
                tLand.updateValueDisplay();
                return tLand;
            case "bat":
            case "heart":
            case "god":
                EventMass tEvent = GameObject.Instantiate(Resources.Load<EventMass>("prefabs/game/mass/" + aMassData.get<string>("type") + "Mass"));
                tEvent.name = aMassData.get<string>("type");
                tEvent.worldPosition = vector2ToPosition(aMassData.get<Vector2>("position"));
                return tEvent;
            case "start":
                StartMass tStart = GameObject.Instantiate(Resources.Load<StartMass>("prefabs/game/mass/startMass"));
                tStart.name = "start";
                tStart.worldPosition = vector2ToPosition(aMassData.get<Vector2>("position"));
                return tStart;
        }
        throw new Exception("不明なマスタイプ : " + aMassData.get<string>("type"));
    }
    static public Vector3 vector2ToPosition(Vector2 aVec) {
        return new Vector3(aVec.x, 0, aVec.y);
    }
}
