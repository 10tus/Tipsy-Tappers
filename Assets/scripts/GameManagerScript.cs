using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public Gradient gradient;
    private GameOverHandler overHandler;
    SceneManagerScript scene;
    private bool over = false;
    private float duration = 5f;
    //[SerializeField]
    //private int ctr = 0;
    //private PlayerSystem playerSystem;
    private AdsController _ads;
    Leaderboards leaderboards;
    ScoresHandler scores;

    private void Awake() {
        ServiceLocator.Register<GameManagerScript>(this);
    }

    void Start()
    {
        scores = ServiceLocator.Resolve<ScoresHandler>();
        _ads = ServiceLocator.Resolve<AdsController>();
        scene = ServiceLocator.Resolve<SceneManagerScript>();
        leaderboards = ServiceLocator.Resolve<Leaderboards>();
        //playerSystem = PlayerSystem.instance;
        if(GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        overHandler = ServiceLocator.Resolve<GameOverHandler>();


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
        try //added catch method so dat game would not crash when game has no internet to connect playfab
        {
            if(!scores.GetHighScore())
                leaderboards.SubmitScoreToPlayfab();
        }
        catch{}
        
        over = true; 
        overHandler.GameOverPanel();
        
        

    }

    public void ShowLeaderboard()
    {
        scene.MenuScene();
    }

    public void ShowRevive()
    {
        overHandler.ShowRevivePanel();
    }

    //relocate dis shit when doing store
    private void ChangeColorBg()
    {
        
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = gradient.Evaluate(t);
        
    }
}
