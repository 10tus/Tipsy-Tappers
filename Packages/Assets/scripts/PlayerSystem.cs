using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public GlassAction _glass;
    public playerActions _player;
    public Timer _timer;
    public GameOverHandler _over;
    public ScoresHandler _scoresHandler;

    #region SingletonInstance
    public static PlayerSystem instance;

     private void Awake() 
     {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }
     }

     #endregion

    public IEnumerator ToggleAnim(Animator animator,string param,bool val,float seconds)
    {
        animator.SetBool(param,val);
        yield return new WaitForSeconds(seconds);
        animator.SetBool(param,!val);
        
    }
    

}

