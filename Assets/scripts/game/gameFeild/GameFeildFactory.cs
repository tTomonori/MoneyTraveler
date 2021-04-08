using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameFeildFactory {
    static public GameFeild create(Arg aStageData) {
        GameFeild tFeild = MyBehaviour.create<GameFeild>();
        tFeild.name = "gameFeild";
        //マス作成
        foreach (Arg tData in aStageData.get<List<Arg>>("mass")) {
            GameMass tMass = GameMassFactory.create(tData);
            tFeild.mMassList.Add(tMass);
            tMass.transform.SetParent(tFeild.mMassContainer.transform, true);
        }
        //ルート作成
        for (int i = 0; i < tFeild.mMassList.Count - 1; i++) {
            MyBehaviour tRoute = createRoute(tFeild.mMassList[i], tFeild.mMassList[i + 1]);
            tRoute.transform.SetParent(tFeild.mRouteContainer.transform, true);
        }
        MyBehaviour tRouteLast = createRoute(tFeild.mMassList[tFeild.mMassList.Count - 1], tFeild.mMassList[0]);
        tRouteLast.transform.SetParent(tFeild.mRouteContainer.transform, true);

        return tFeild;
    }
    //2つのマスを結ぶ線を生成
    static public MyBehaviour createRoute(GameMass aMass1, GameMass aMass2) {
        MyBehaviour tRoute = MyBehaviour.create<MyBehaviour>();
        SpriteRenderer tRenderer = tRoute.gameObject.AddComponent<SpriteRenderer>();
        tRoute.name = "route";
        tRenderer.sprite = Resources.Load<Sprite>("sprites/squareMask");
        tRenderer.color = new Color(0.9f, 0.9f, 0.9f, 0.8f);
        tRoute.position = (aMass1.worldPosition + aMass2.worldPosition) / 2f;
        tRoute.positionY = -0.1f;
        tRoute.scale = new Vector3(Vector3.Distance(aMass1.worldPosition, aMass2.worldPosition), 0.5f, 1);
        tRoute.rotateX = 90;
        tRoute.rotateY = VectorCalculator.corner(new Vector2(1, 0), new Vector2(aMass1.positionX - aMass2.positionX, aMass1.positionZ - aMass2.positionZ));
        return tRoute;
    }
}
