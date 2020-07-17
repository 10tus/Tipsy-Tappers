using System.Collections.Generic;
using UnityEngine;

public class GlassAction : MonoBehaviour
{
    static Sprite[] _drinks;

    public GameObject[] glassObjects;
    public Queue<Glass> glassesQueue;

    int _queueLimit = 5;

    void Start()
    {
        _drinks = new Sprite[] { 
            Resources.Load<Sprite>("deathdrink"),
            Resources.Load<Sprite>("drink"), 
            Resources.Load<Sprite>("drink2"), 
            Resources.Load<Sprite>("drink3") 
        };

        glassesQueue = new Queue<Glass>();

        while(glassesQueue.Count < _queueLimit){
            AddGlass();
        }
    }

    void AddGlass()
    {
        glassesQueue.Enqueue(GlassFactory.GenerateGlass());       
    }

    public void ReplaceGlass()
    {
        glassesQueue.Dequeue();
        AddGlass();
        UpdateGlass();
    }

    void UpdateGlass(){
        Glass[] _glassesArray = glassesQueue.ToArray();
        for (int i = 0; i < glassObjects.Length; i++)
        {
            glassObjects[i].GetComponent<SpriteRenderer>().sprite = _drinks[_glassesArray[i].glassValue % _glassesArray.Length];
        }
    }
    
}
