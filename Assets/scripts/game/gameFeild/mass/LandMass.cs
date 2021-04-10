using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandMass : GameMass {
    static public int mMaxIncreaseLevel = 3;
    public int mOrner = 0;
    public int mIncreaseLevel = 0;
    public int mBaseValue = 0;
    public List<string> mAttributes;
    [SerializeField]
    public SpriteRenderer mMass;
    public TextMesh mNameMesh;
    public MyBehaviour mSelling;
    public TextMesh mPurchaseMesh;
    public MyBehaviour mSold;
    public TextMesh mIncreaseMesh;
    public TextMesh mFeeMesh;
    public SpriteRenderer mAttribute1;
    public SpriteRenderer mAttribute2;
    public MyBehaviour mBuilding;
    public SpriteRenderer mBuildingRenderer;

    public int getExpansionCost(int aExpansionLevel) {
        return mBaseValue / 2 * (int)Mathf.Pow(2, aExpansionLevel);
    }

    public int mPurchaseCost { get { return mBaseValue; } }
    public int mFeeCost { get { return (int)(GameMain.mFeeRate * mBaseValue / 10 * Mathf.Pow(3, mIncreaseLevel)); } }
    public int mIncreaseCost { get { return getExpansionCost(mIncreaseLevel); } }
    public int mAcquisitionCost { get { return (int)(mTotalValue * GameMain.mAcquisitionRate); } }
    public int mSellCost { get { return (int)(mTotalValue * 0.8f); } }
    public int mTotalValue {
        get {
            int tTotal = mBaseValue;
            for (int i = 0; i < mIncreaseLevel; i++) {
                tTotal += getExpansionCost(i);
            }
            return tTotal;
        }
    }
    //購入増資等の欄更新
    public void updateValueDisplay() {
        if (mOrner <= 0) {
            mSelling.gameObject.SetActive(true);
            mSold.gameObject.SetActive(false);
            mPurchaseMesh.text = mPurchaseCost.ToString();
        } else {
            mSelling.gameObject.SetActive(false);
            mSold.gameObject.SetActive(true);
            mIncreaseMesh.text = mIncreaseCost.ToString();
            mFeeMesh.text = mFeeCost.ToString();
            if (mIncreaseLevel == mMaxIncreaseLevel) {
                mIncreaseMesh.text = "MAX";
            }
        }
    }
    public void changeOrner(PlayerStatus aStatus,Action aCallback) {
        if (aStatus == null) {
            mOrner = 0;
            mMass.color = new Color(1, 1, 1, 1);
            updateValueDisplay();
            aCallback();
            return;
        }
        mOrner = aStatus.mPlayerNumber;
        mMass.color = aStatus.playerColor;
        updateValueDisplay();
        aCallback();
    }
    public void changeIncreaseLevel(int aLevel,Action aCallback) {
        MySoundPlayer.playSe("increase", false);
        mIncreaseLevel = aLevel;
        updateValueDisplay();
        mBuildingRenderer.sprite = Resources.Load<Sprite>("sprites/feild/mass/building" + aLevel.ToString());
        mBuilding.scale2D = new Vector2(0, 0);
        mBuilding.scaleTo(new Vector2(0.2f, 1.3f), 0.2f, () => {
            mBuilding.scaleTo(new Vector2(1.3f, 0.6f), 0.2f, () => {
                mBuilding.scaleTo(new Vector2(1, 1), 0.2f, () => {
                    aCallback();
                });
            });
        });
    }
}
