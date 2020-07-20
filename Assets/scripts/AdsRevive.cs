using UnityEngine;

public class AdsRevive : Ads
{
    //PlayerSystem playerSystem;
    GameOverHandler _over;
    GameObject revivePanel;

    private void Awake() 
    {
        _over = ServiceLocator.Resolve<GameOverHandler>();
        revivePanel = gameObject; //dis is referencing to d gameobject attach to dis script
    }

    public override void Reward()
    {
        Debug.Log("hello from ads revive bitch");
        _over.ReviveAccepted();

    }

    public override void InitialSetup()
    {
        revivePanel.SetActive(false);

    }

}
