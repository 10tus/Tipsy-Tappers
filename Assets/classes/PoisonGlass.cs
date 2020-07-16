using UnityEngine;

public class PoisonGlass : Glass {
    Sprite _poisonSprite;

    PoisonGlass() : base(){
        glassValue = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = _poisonSprite;
    }

    public override bool Drink(){
        return false;
    }
    public override bool Throw(){
        return true;
    }
}