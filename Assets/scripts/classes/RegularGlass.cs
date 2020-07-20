using UnityEngine;

public class RegularGlass : Glass {
    public RegularGlass(int value) : base(value){}

    public override bool Drink(){
        return true;
    }
    public override bool Throw(){
        return false;
    }
}