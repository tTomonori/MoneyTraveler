using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComa : MyBehaviour {
    [SerializeField]
    public SpriteRenderer mImg;
    public TextMesh mNumber;
    public void displayNumber(string aNum) {
        mNumber.text = aNum;
    }
}
