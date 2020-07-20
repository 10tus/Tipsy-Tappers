using UnityEngine;
using System.Collections;

public class PlayerTap : MonoBehaviour
{
<<<<<<< HEAD
    private bool flag;
    Animator armAnim;

    Timer timer;
    PlayerActions player;
    GameOverHandler menu;

    void Start() {
        menu = ServiceLocator.Resolve<GameOverHandler>();
=======
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

>>>>>>> develop
        if(GameObject.FindGameObjectWithTag("arm") != null)
        {
            armAnim = GameObject.FindGameObjectWithTag("arm").GetComponent<Animator>();
        }
<<<<<<< HEAD
        player = ServiceLocator.Resolve<PlayerActions>();
        timer = ServiceLocator.Resolve<Timer>();
=======
        //glass = GlassAction.instance;
        

>>>>>>> develop
    }

    void Update()
    {
        TouchAction();
<<<<<<< HEAD
        if(flag)
=======
        if(player.flagTap)
        {
>>>>>>> develop
            timer.StartTimer();
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
<<<<<<< HEAD
        if(posX > 0 && posY < -1.5f)
        {
            menu.instruction.SetActive(false);
            player.Drink();
            StartCoroutine(ToggleAnim(armAnim,"Drink",true,0.01f));
            flag = true;    
=======

        if(posX > 0 && posY < -1.5f )
        {
            overHandler.instruction.SetActive(false);
            player.Drink();
            StartCoroutine(ToggleAnim(armAnim,"Drink",true,0.01f));
            player.flagTap = true;    
>>>>>>> develop
        }
        //player taps at left side
        else if (posX<0 && posY < -1.5f )
        {
<<<<<<< HEAD
            menu.instruction.SetActive(false);
            player.Throw();
            StartCoroutine(ToggleAnim(armAnim,"Throw",true,0.01f));
            flag = true;
=======
            overHandler.instruction.SetActive(false);
            player.Throw();
            StartCoroutine(ToggleAnim(armAnim,"Throw",true,0.01f));
            player.flagTap = true;  
>>>>>>> develop
        }

    }

    private IEnumerator ToggleAnim(Animator animator,string param,bool val,float seconds)
    {
        animator.SetBool(param,val);
        yield return new WaitForSeconds(seconds);
        animator.SetBool(param,!val);
        
    }

   

}
