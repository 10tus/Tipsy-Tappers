using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< HEAD
    void Awake()
    {
=======
    
    private void Awake() {
        
>>>>>>> develop
        DontDestroyOnLoad(gameObject);
        ServiceLocator.Register<SceneManagerScript>(this);
    }
    
    public void GameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
}
