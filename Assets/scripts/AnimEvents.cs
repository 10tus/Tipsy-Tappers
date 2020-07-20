using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    Animator animP;
    public GameObject playerSprite;
    // Start is called before the first frame update
<<<<<<< HEAD

    SoundManagerScript sound;

    void Start(){
        sound = ServiceLocator.Resolve<SoundManagerScript>();
    }

=======
    SoundManagerScript sound;
    private void Awake() 
    {
        sound = ServiceLocator.Resolve<SoundManagerScript>();
    }
>>>>>>> develop
    public void ShowElec()
    {
        animP = playerSprite.GetComponent<Animator>();
        animP.SetBool("doElec",true);
        sound.Play("Thunder");
    }

    public void EndElec()
    {
        //animP.SetBool("doElec", false);
        playerSprite.SetActive(false);
    }
}
