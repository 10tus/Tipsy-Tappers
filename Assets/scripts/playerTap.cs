using UnityEngine;
using System.Collections;

public class PlayerTap : MonoBehaviour
{
    private Timer timer;
    private GameOverHandler overHandler;
    PlayerActions player;
    //private PlayerSystem playerSystem;
    private bool flag;
    Animator armAnim;

    private void Start() 
    {   
        overHandler = ServiceLocator.Resolve<GameOverHandler>();
        timer = ServiceLocator.Resolve<Timer>();
        player = ServiceLocator.Resolve<PlayerActions>();

        if(GameObject.FindGameObjectWithTag("arm") != null)
        {
            armAnim = GameObject.FindGameObjectWithTag("arm").GetComponent<Animator>();
        }
        //glass = GlassAction.instance;
        

    }

    void Update()
    {
        
        TouchAction();
        if(player.flagTap)
        {
            timer.StartTimer();
        }
        
    }

    private void TouchAction()
    {
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);
            Vector3 tch = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Ended)
            {
                DoAction(tch.x,tch.y); //get y-position of screen so player only taps at bottom side
                //print("Loc" + tch.x);


            } 

        }         
    }

    private void DoAction(float posX,float posY)
    {
        //player taps at right side

        if(posX > 0 && posY < -1.5f )
        {
            overHandler.instruction.SetActive(false);
            player.Drink();
            StartCoroutine(ToggleAnim(armAnim,"Drink",true,0.01f));
            player.flagTap = true;    
        }
        //player taps at left side
        else if (posX<0 && posY < -1.5f )
        {
            overHandler.instruction.SetActive(false);
            player.Throw();
            StartCoroutine(ToggleAnim(armAnim,"Throw",true,0.01f));
            player.flagTap = true;  
        }

    }

    private IEnumerator ToggleAnim(Animator animator,string param,bool val,float seconds)
    {
        animator.SetBool(param,val);
        yield return new WaitForSeconds(seconds);
        animator.SetBool(param,!val);
        
    }

   

}
