using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusDisplay : MyBehaviour {
    [SerializeField]
    public SpriteRenderer mBack;
    public TextMesh mRankMesh;
    public SpriteRenderer mCharaImg;
    public TextMesh mCharaNameMesh;
    public TextMesh mMoneyMesh;
    public TextMesh mPropertyMesh;
    public TextMesh mAssetsMesh;
    public TextMesh mOrbitMesh;

    //情報を表示
    public void setStatus(PlayerStatus aStatus) {
        mBack.color = aStatus.playerColor;
        mCharaImg.sprite = Resources.Load<Sprite>("sprites/chara/" + aStatus.mCharaFile + "/" + aStatus.mCharaFile);
        mCharaNameMesh.text = aStatus.mCharaName;
        updateStatus(aStatus);
    }
    //情報を更新
    public void updateStatus(PlayerStatus aStatus) {
        mRankMesh.text = aStatus.mRank + "位";
        mOrbitMesh.text = aStatus.mOrbit.ToString();
        mMoneyMesh.text = aStatus.mMoney.ToString();
        mPropertyMesh.text = aStatus.mProperty.ToString();
        mAssetsMesh.text = aStatus.mAssets.ToString();
    }
}
