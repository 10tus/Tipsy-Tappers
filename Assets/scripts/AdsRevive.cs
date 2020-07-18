using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsRevive : Ads
{
    PlayerSystem playerSystem;

    private void Awake() {
        playerSystem = PlayerSystem.instance;
    }

    public override void Reward()
    {
        Debug.Log("fcken bitch");
        playerSystem._over.ReviveAccepted();

    }

}
