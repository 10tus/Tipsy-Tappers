using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Animator manAnim, dionAnim;

    DrunkHandler drunkHandler;
    GameManagerScript gameManager;
    ScoresHandler scoresHandler;
    Timer timer;
    GlassAction glassAction;

    void Awake(){
        ServiceLocator.Register<PlayerActions>(this);
    }

    void Start() 
    {
        gameManager = ServiceLocator.Resolve<GameManagerScript>();
        scoresHandler = ServiceLocator.Resolve<ScoresHandler>();
        timer = ServiceLocator.Resolve<Timer>();

        glassAction = ServiceLocator.Resolve<GlassAction>();
        drunkHandler = ServiceLocator.Resolve<DrunkHandler>();
    }

    public void Drink()
    {
        if(glassAction.glassesQueue.Peek().Drink())
        {
            glassAction.ReplaceGlass();
            timer.AddTime();
            timer.rateOfDecreaseChange(scoresHandler.currScore);
            scoresHandler.UpdateScore();
            drunkHandler.IncrementDrunkLevel();
        }
        else       
            playDeadAnim(1);
    }

    public void Throw()
    {
        if(glassAction.glassesQueue.Peek().Throw())
        {
            glassAction.ReplaceGlass();
            timer.AddTime();
            scoresHandler.UpdateScore();
        }
        else
            playDeadAnim(0);
    }

    void playDeadAnim(int type)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerTap>().enabled = false;
        if(type == 1) //death by flying
        {
            StartCoroutine(SetAnim(manAnim,"dead", true));
        }
        else //death by electrocution
        {
            StartCoroutine(SetAnim(dionAnim,"dionShow",true));
        }
        //manAnim.SetBool("dead", false);
    }

    IEnumerator SetAnim(Animator name,string param,bool val)
    {
        name.SetBool(param,val);
        yield return new WaitForSeconds(2.5f);
        name.SetBool(param,!val);
        gameManager.GameOver();
    }
}
