using System.Collections;
using UnityEngine;


public class TipsyUtils:MonoBehaviour
{
    
    public IEnumerator ToggleAnim(Animator animator,string param,bool val,float seconds)
    {
        animator.SetBool(param,val);
        yield return new WaitForSeconds(seconds);
        animator.SetBool(param,!val);
        
    }

    public void ToggleActiveGameObject(GameObject obj, bool val)
    {
        obj.SetActive(val);
    }


    public void SetAnimatorBoolParameter(Animator animator, string name, bool val)
    {
        animator.SetBool(name,val);

    }

}
