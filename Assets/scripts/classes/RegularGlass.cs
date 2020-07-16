using UnityEngine;

public class RegularGlass : Glass {
    Sprite[] _drinksSprites;

    RegularGlass(int value) : base(){
        glassValue = value;
        gameObject.GetComponent<SpriteRenderer>().sprite = _drinksSprites[glassValue % _drinksSprites.Length];
    }

    public override bool Drink(){
        return true;
    }
    public override bool Throw(){
        return false;
    }
}