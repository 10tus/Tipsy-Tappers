using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public Gradient gradient;
    private GameOverHandler menu;
    private bool over = false;
    private float duration = 5f;

    #region SingletonInstance
    public static GameManagerScript instance;

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
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        menu = GameOverHandler.instance;


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
        menu.GameOverPanel();

    }

    private void ChangeColorBg()
    {
        
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = gradient.Evaluate(t);
        
    }
}
