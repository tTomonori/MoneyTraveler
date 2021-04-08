using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus {
    public CharaAi mAi;
    public string mCharaFile;
    public string mCharaName;
    public int mMoney;
    public int mProperty;
    public int mAssets;
    public int mOrbit;
    public int mRank;
    public int mPlayerNumber;
    public int mCurrentMassNumber;
    public PlayerComa mComa;

    public Color playerColor { get {
            if (mPlayerNumber == 1) return new Color(1, 0.1f, 0.1f, 1);
            else if (mPlayerNumber == 2) return new Color(0.2f, 0.4f, 1, 1);
            else if (mPlayerNumber == 3) return new Color(1, 1, 0, 1);
            else if (mPlayerNumber == 4) return new Color(0, 1, 0, 1);
            return new Color(1, 1, 1, 1);
        }
    }
}
