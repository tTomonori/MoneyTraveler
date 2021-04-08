using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMass : GameMass {
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
        }
    }
}
