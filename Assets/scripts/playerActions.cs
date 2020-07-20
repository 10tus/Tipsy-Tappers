using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{

    public Animator manAnim, dionAnim;
    public bool flagTap;

    DrunkHandler drunkHandler;
    GameManagerScript gameManager;
    ScoresHandler scoresHandler;
    Timer timer;
    GlassAction glassAction;
    GameOverHandler _over;

    void Awake()
    {
        ServiceLocator.Register<PlayerActions>(this);
    }

    private void Start() 
    {
        //playerSystem = PlayerSystem.instance;
        gameManager = ServiceLocator.Resolve<GameManagerScript>();
        scoresHandler = ServiceLocator.Resolve<ScoresHandler>();
        timer = ServiceLocator.Resolve<Timer>();
        glassAction = ServiceLocator.Resolve<GlassAction>();
        drunkHandler = ServiceLocator.Resolve<DrunkHandler>();
        _over = ServiceLocator.Resolve<GameOverHandler>();
    }

    public void Drink()
    {
        if(glassAction.currentGlass.Drink())
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
        if(glassAction.currentGlass.Throw())
        {
            glassAction.ReplaceGlass();
            timer.AddTime();
            scoresHandler.UpdateScore();
        }
        else
            playDeadAnim(0);
    }

    /*
    private void UpdateValue()
    {

        drunkBar.value = ctr;
        if(ctr == DrunkLimit)
            StartCoroutine(drunkHandler.Drunk());


    }

    
    
    IEnumerator Drunk()
    {
        Animator clouds = cover.GetComponent<Animator>();

        manAnim.SetBool("Drunk", true);
        clouds.SetBool("doCover", true);
        yield return new WaitForSeconds(3f);
        ctr = 0;
        manAnim.SetBool("Drunk", false);
        clouds.SetBool("doCover", false);

    }*/
    void playDeadAnim(int type)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTap>().enabled = false;
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

        if(_over.revivedOnce)
            gameManager.GameOver();     
        else
            gameManager.ShowRevive(); 
            
    }

    

    
}
