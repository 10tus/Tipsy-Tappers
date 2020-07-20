using UnityEngine;

public class playerTap : MonoBehaviour
{
    private Timer timer;
    private GlassAction glass;
    private GameOverHandler overHandler;
    private PlayerSystem playerSystem;
    private bool flag;
    Animator armAnim;

    private void Start() 
    {
        playerSystem = PlayerSystem.instance;
        overHandler = playerSystem._over;
        timer = playerSystem._timer;
        glass = playerSystem._glass;

        if(GameObject.FindGameObjectWithTag("arm") != null)
        {
            armAnim = GameObject.FindGameObjectWithTag("arm").GetComponent<Animator>();
        }
        //glass = GlassAction.instance;
        

    }

    void Update()
    {
        
        TouchAction();
        if(flag)
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

        if(posX > 0 && posY < -1.5f)
        {
            overHandler.instruction.SetActive(false);
            glass.RemoveGlass(0);
            StartCoroutine(playerSystem.ToggleAnim(armAnim,"Drink",true,0.01f));
            flag = true;          
        }
        //player taps at left side
        else if (posX<0 && posY < -1.5f)
        {
            overHandler.instruction.SetActive(false);
            glass.RemoveGlass(1);
            StartCoroutine(playerSystem.ToggleAnim(armAnim,"Throw",true,0.01f));
            flag = true;
            
        }

    }

   

}
