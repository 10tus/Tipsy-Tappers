using UnityEngine;

public class playerTap : TipsyUtils
{
    private bool flag;
    Animator armAnim;

    Timer timer;
    PlayerActions player;
    GameOverHandler menu;

    void Start() {
        menu = GetComponent<GameOverHandler>();
        if(GameObject.FindGameObjectWithTag("arm") != null)
        {
            armAnim = GameObject.FindGameObjectWithTag("arm").GetComponent<Animator>();
        }
        player = GetComponent<PlayerActions>();
        timer = GetComponent<Timer>();
        
    }

    void Update()
    {
        TouchAction();
        if(flag)
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
        Debug.Log("ha");
        if(posX > 0 && posY < -1.5f)
        {
            Debug.Log("he");
            // menu.instruction.SetActive(false);
            Debug.Log("he");
            player.Drink();
            Debug.Log("he");
            StartCoroutine(ToggleAnim(armAnim,"Drink",true,0.01f));
            Debug.Log("he");
            flag = true;    
            Debug.Log("he");      
        }
        //player taps at left side
        else if (posX<0 && posY < -1.5f)
        {
            Debug.Log("ho");
            // menu.instruction.SetActive(false);
            Debug.Log("ho");
            player.Throw();
            Debug.Log("ho");
            StartCoroutine(ToggleAnim(armAnim,"Throw",true,0.01f));
            Debug.Log("ho");
            flag = true;
            Debug.Log("ho");
        }

    }

}
