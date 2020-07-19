using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GlassAction : MonoBehaviour
{
    [SerializeField]
    public Sprite[] _drinks;

    public GameObject[] glassObjects;
    public Queue<Glass> glassesQueue;

    int _queueLimit = 5;

    void Awake()
    {
        ServiceLocator.Register<GlassAction>(this);
    }

    void Start(){
        glassesQueue = new Queue<Glass>();

        while(glassesQueue.Count < _queueLimit){
            AddGlass();
        }

        UpdateGlass();
        //DebugGlass();
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
        //DebugGlass();
    }

    void UpdateGlass(){
        foreach ((Glass glass, int i) in glassesQueue.Select((value, i) => (value, i)))
        {
            glassObjects[i].GetComponent<SpriteRenderer>().sprite = _drinks[glass.glassValue % _drinks.Length];
        }
    }
    
    void DebugGlass(){
        string debug = "";
        foreach((Glass glass, int i) in glassesQueue.Select((value, i) => (value, i))){
            debug += $"[{i}]{glass}-{glass.glassValue} ";
        }
        Debug.Log(debug);
    }
}
