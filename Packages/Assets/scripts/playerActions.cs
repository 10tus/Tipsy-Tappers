using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playerActions : MonoBehaviour
{

    private int ctr = 0,forbidVal = 1;
    private Timer timer;
    private GameManagerScript gameManager;
    private ScoresHandler scoresHandler;
    [SerializeField]
    private Slider drunkBar;
    private PlayerSystem playerSystem;

    [SerializeField] //SerializeField to make it visible in inspector in unity but not accessible by other class
    private int DrunkLimit;
    public Animator manAnim,dionAnim;
    public GameObject cover;


    private void Start() 
    {
        playerSystem = PlayerSystem.instance;
        gameManager = GameManagerScript.instance;
        scoresHandler = playerSystem._scoresHandler;
        timer = playerSystem._timer;
    }

    private void Drink(int drinkVal)
    {

        if(drinkVal != forbidVal)
        {
            timer.AddTime();
            scoresHandler.UpdateScore();
            ctr++;
            UpdateValue();
            

        }
        else       
            playDeadAnim(1);

    }

    public void Consequences(int val, int type)
    {
        
        if(type == 0)
        {
            Drink(val);
            timer.rateOfDecreaseChange(scoresHandler.currScore);

        }
        else if(type == 1)
            Throw(val);
    }

    private void Throw(int throwVal)
    {
        if(throwVal == forbidVal)
        {
            timer.AddTime();
            scoresHandler.UpdateScore();
        }
        else
            playDeadAnim(0);
    }

    private void UpdateValue()
    {

        drunkBar.value = ctr;
        if(ctr == DrunkLimit)
            StartCoroutine(Drunk());


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

        if(playerSystem._over.revivedOnce)
            gameManager.GameOver();     
        else
            gameManager.ShowRevive(); 
            
    }

    

    
}
