using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        //東西南北
        foreach (GameMass tMass in tFeild.mMassList) {
            if (tFeild.mNorth < tMass.worldPosition.z) tFeild.mNorth = tMass.worldPosition.z;
            if (tFeild.mEast < tMass.worldPosition.x) tFeild.mEast = tMass.worldPosition.x;
            if (tFeild.mSouth > tMass.worldPosition.z) tFeild.mSouth = tMass.worldPosition.z;
            if (tFeild.mWest > tMass.worldPosition.x) tFeild.mWest = tMass.worldPosition.x;
        }
        tFeild.mNorth += 10;
        tFeild.mEast += 10;
        tFeild.mSouth -= 15;
        tFeild.mWest -= 10;
        tFeild.mHeight = 10;
        //壁
        float tFloorOffset = -0.5f;
        //床
        MyBehaviour tWall = MyBehaviour.create<MyBehaviour>();
        tWall.name = "floor";
        tWall.transform.SetParent(tFeild.mWallContainer.transform);
        tWall.gameObject.AddComponent<Canvas>();
        Image tImage = tWall.gameObject.AddComponent<Image>();
        tImage.sprite = Resources.Load<Sprite>("sprites/wall/" + aStageData.get<string>("floor"));
        tImage.type = Image.Type.Tiled;
        tImage.pixelsPerUnitMultiplier = Mathf.Max(tFeild.mEast - tFeild.mWest, tFeild.mNorth - tFeild.mSouth);
        tWall.rotateX = 90;
        tWall.position = new Vector3((tFeild.mWest + tFeild.mEast) / 2, tFloorOffset, (tFeild.mNorth + tFeild.mSouth) / 2);
        tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(tFeild.mEast - tFeild.mWest, tFeild.mNorth - tFeild.mSouth);
        //北の壁
        tWall = MyBehaviour.create<MyBehaviour>();
        tWall.name = "north";
        tWall.transform.SetParent(tFeild.mWallContainer.transform);
        tWall.gameObject.AddComponent<Canvas>();
        tImage = tWall.gameObject.AddComponent<Image>();
        tImage.sprite = Resources.Load<Sprite>("sprites/wall/" + aStageData.get<string>("wall"));
        tImage.type = Image.Type.Tiled;
        tImage.pixelsPerUnitMultiplier = Mathf.Max(tFeild.mEast - tFeild.mWest, tFeild.mHeight + tFloorOffset);
        tWall.position = new Vector3((tFeild.mWest + tFeild.mEast) / 2, (tFeild.mHeight + tFloorOffset) / 2, tFeild.mNorth);
        tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(tFeild.mEast - tFeild.mWest, tFeild.mHeight - tFloorOffset);
        //東の壁
        tWall = MyBehaviour.create<MyBehaviour>();
        tWall.name = "east";
        tWall.transform.SetParent(tFeild.mWallContainer.transform);
        tWall.gameObject.AddComponent<Canvas>();
        tImage = tWall.gameObject.AddComponent<Image>();
        tImage.sprite = Resources.Load<Sprite>("sprites/wall/" + aStageData.get<string>("wall"));
        tImage.type = Image.Type.Tiled;
        tImage.pixelsPerUnitMultiplier = Mathf.Max(tFeild.mNorth - tFeild.mSouth, tFeild.mHeight + tFloorOffset);
        tWall.rotateY = -90;
        tWall.position = new Vector3(tFeild.mEast, (tFeild.mHeight + tFloorOffset) / 2, (tFeild.mNorth + tFeild.mSouth) / 2);
        tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(tFeild.mNorth - tFeild.mSouth, tFeild.mHeight - tFloorOffset);
        //西の壁
        tWall = MyBehaviour.create<MyBehaviour>();
        tWall.name = "west";
        tWall.transform.SetParent(tFeild.mWallContainer.transform);
        tWall.gameObject.AddComponent<Canvas>();
        tImage = tWall.gameObject.AddComponent<Image>();
        tImage.sprite = Resources.Load<Sprite>("sprites/wall/" + aStageData.get<string>("wall"));
        tImage.type = Image.Type.Tiled;
        tImage.pixelsPerUnitMultiplier = Mathf.Max(tFeild.mNorth - tFeild.mSouth, tFeild.mHeight + tFloorOffset);
        tWall.rotateY = 90;
        tWall.position = new Vector3(tFeild.mWest, (tFeild.mHeight + tFloorOffset)/2, (tFeild.mNorth + tFeild.mSouth) / 2);
        tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(tFeild.mNorth - tFeild.mSouth, tFeild.mHeight - tFloorOffset);

        return tFeild;
    }
    //2つのマスを結ぶ線を生成
    static public MyBehaviour createRoute(GameMass aMass1, GameMass aMass2) {
        MyBehaviour tRoute = MyBehaviour.create<MyBehaviour>();
        SpriteRenderer tRenderer = tRoute.gameObject.AddComponent<SpriteRenderer>();
        tRoute.name = "route";
        tRenderer.sprite = Resources.Load<Sprite>("sprites/squareMask");
        tRenderer.material = Resources.Load<Material>("material/My_Translucent");
        tRenderer.color = new Color(0.9f, 0.9f, 0.9f, 0.8f);
        tRoute.position = (aMass1.worldPosition + aMass2.worldPosition) / 2f;
        tRoute.positionY = -0.1f;
        tRoute.scale = new Vector3(Vector3.Distance(aMass1.worldPosition, aMass2.worldPosition), 0.5f, 1);
        tRoute.rotateX = 90;
        tRoute.rotateY = VectorCalculator.corner(new Vector2(1, 0), new Vector2(aMass1.positionX - aMass2.positionX, aMass1.positionZ - aMass2.positionZ));
        return tRoute;
    }
}
