using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsRevive : Ads
{
    PlayerSystem playerSystem;
    GameObject revivePanel;

    private void Awake() {
        playerSystem = PlayerSystem.instance;
        revivePanel = gameObject; //dis is referencing to d gameobject attach to dis script
    }

    public override void Reward()
    {
        Debug.Log("hello from ads revive bitch");
        playerSystem._over.ReviveAccepted();

    }

    public override void InitialSetup()
    {
        revivePanel.SetActive(false);

    }

}
