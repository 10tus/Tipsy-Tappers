using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrunkHandler : MonoBehaviour
{
    public GameObject cover;
    public Animator manAnim;
    private int _drunkLimit = 15;
    private int drunkLevel = 0;

    [SerializeField]
    private Slider drunkBar;
    
    void Awake(){
        ServiceLocator.Register<DrunkHandler>(this);
    }

    public void IncrementDrunkLevel(){
        drunkBar.value = ++drunkLevel;
        if(drunkLevel == _drunkLimit)
            StartCoroutine(Drunk());
    }

    IEnumerator Drunk()
    {
        Animator clouds = cover.GetComponent<Animator>();

        manAnim.SetBool("Drunk", true);
        clouds.SetBool("doCover", true);
        yield return new WaitForSeconds(3f);
        drunkLevel = 0;
        manAnim.SetBool("Drunk", false);
        clouds.SetBool("doCover", false);
    }

}
