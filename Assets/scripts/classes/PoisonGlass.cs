using UnityEngine;

public class PoisonGlass : Glass {
    public PoisonGlass(int value) : base(value){}

    public override bool Drink(){
        return false;
    }
    public override bool Throw(){
        return true;
    }
}