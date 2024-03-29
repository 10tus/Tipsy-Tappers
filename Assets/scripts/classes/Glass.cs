using UnityEngine;

[System.Serializable]
public abstract class Glass {
    public int glassValue;

    public Glass(int value){
        glassValue = value;
    }

    public abstract bool Drink();
    public abstract bool Throw();
}