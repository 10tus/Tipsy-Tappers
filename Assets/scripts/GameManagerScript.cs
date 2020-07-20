using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public Gradient gradient;
    private GameOverHandler menu;
    private bool over = false;
    private float duration = 5f;

    void Awake(){
        ServiceLocator.Register<GameManagerScript>(this);
    }

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        menu = ServiceLocator.Resolve<GameOverHandler>();


    }

    // Update is called once per frame
    void Update()
    {
        //will not change bg color if game over
        if(!over)
        {
            ChangeColorBg();
        }

    }

    public void GameOver()
    {
        over = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerTap>().enabled = false;
        menu.GameOverPanel();
    }

    private void ChangeColorBg()
    {
        
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = gradient.Evaluate(t);
        
    }
}
