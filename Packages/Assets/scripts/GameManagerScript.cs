using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public Gradient gradient;
    private GameOverHandler overHandler;
    private bool over = false;
    private float duration = 5f;
    [SerializeField]
    private int ctr = 0;
    private PlayerSystem playerSystem;
    private AdsController _ads;




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
        _ads = AdsController.instance;
        playerSystem = PlayerSystem.instance;
        if(GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        overHandler = playerSystem._over;


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
        overHandler.GameOverPanel();
        //if games count is 2 play no reward ads
        

    }

    public void ShowRevive()
    {
        overHandler.ShowRevivePanel();
    }

    private void ChangeColorBg()
    {
        
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = gradient.Evaluate(t);
        
    }
}
