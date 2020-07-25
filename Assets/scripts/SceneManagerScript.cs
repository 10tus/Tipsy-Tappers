using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void Awake() {
        ServiceLocator.Register<SceneManagerScript>(this);  
        
    }
    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
