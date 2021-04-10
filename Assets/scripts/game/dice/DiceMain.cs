using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceMain : MonoBehaviour {
    public int[] mDice1Numbers { get { return new int[3] { 1, 3, 5 }; } }
    public int[] mDice2Numbers { get { return new int[4] { 0, 2, 4, 6 }; } }
    public int[] mDice3Numbers { get { return new int[2] { 0, 1 }; } }
    Arg mArg;
    [SerializeField]
    public MyBehaviour mDices;
    public DiceBox mDice1;
    public DiceBox mDice2;
    public DiceBox mDice3;
    void Start() {
        mArg = MySceneManager.getArg("dice");
        setDice(() => { mArg.get<Action<DiceMain>>("setted")(this); });
    }
    //diceを表示する
    public void setDice(Action aCallback) {
        mDices.moveBy(new Vector3(0, -7, 0), 0.4f, aCallback);
    }
    public void open() {
        if (!mDice1.mIsOpen) {
            open1();
            return;
        }
        if (!mDice2.mIsOpen) {
            open2();
            return;
        }
        if (!mDice3.mIsOpen) {
            open3();
            return;
        }
    }
    public void open1() {
        if (mDice1.mIsOpen) return;
        int[] tNumbers = mDice1Numbers;
        mDice1.open(tNumbers[UnityEngine.Random.Range(0, tNumbers.Length)]);
        checkAllOpen();
    }
    public void open2() {
        if (mDice2.mIsOpen) return;
        int[] tNumbers = mDice2Numbers;
        mDice2.open(tNumbers[UnityEngine.Random.Range(0, tNumbers.Length)]);
        checkAllOpen();
    }
    public void open3() {
        if (mDice3.mIsOpen) return;
        int[] tNumbers = mDice3Numbers;
        mDice3.open(tNumbers[UnityEngine.Random.Range(0, tNumbers.Length)]);
        checkAllOpen();
    }
    //diceの合計値を取得
    public int getTotalNumber() {
        return mDice1.number + mDice2.number + mDice3.number;
    }
    public void checkAllOpen() {
        if (!(mDice1.mIsOpen & mDice2.mIsOpen & mDice3.mIsOpen)) return;
        MyBehaviour.setTimeoutToIns(1, () => { mArg.get<Action<int>>("opened")(getTotalNumber()); });
    }
}
