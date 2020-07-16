using UnityEngine;

public class playerTap : TipsyUtils
{
    // Update is called once per frame
    //float tapStart, tapEnd,tapTime = 0.02f;
    private Timer timer;
    private GlassAction glass;
    private GameOverHandler menu;
    private bool flag;
    Animator armAnim;

    private void Start() {
        menu = GameOverHandler.instance;
        if(GameObject.FindGameObjectWithTag("arm") != null)
        {
            armAnim = GameObject.FindGameObjectWithTag("arm").GetComponent<Animator>();
        }
        glass = GlassAction.instance;
        timer = Timer.instance;
        
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
            menu.instruction.SetActive(false);
            glass.RemoveGlass(0);
            StartCoroutine(ToggleAnim(armAnim,"Drink",true,0.01f));
            flag = true;          
        }
        //player taps at left side
        else if (posX<0 && posY < -1.5f)
        {
            menu.instruction.SetActive(false);
            glass.RemoveGlass(1);
            StartCoroutine(ToggleAnim(armAnim,"Throw",true,0.01f));
            flag = true;
            
        }

    }

}
