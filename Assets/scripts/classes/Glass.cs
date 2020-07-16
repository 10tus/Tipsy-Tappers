using UnityEngine;

[System.Serializable]
public abstract class Glass {
    public GameObject gameObject;
    public int glassValue;

    Glass(){
        gameObject = new GameObject("Glass");
    }

    public abstract bool Drink();
    public abstract bool Throw();
}