using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using System.Linq;


public class GlassAction : MonoBehaviour
{
    [SerializeField]
    public Sprite[] _drinks;

    public GameObject[] glassObjects;
    public Queue<Glass> glassesQueue;

    public GameObject poisonCloud;
    ConcurrentQueue<GameObject> poisonInstances;

    int _queueLimit = 5;

    void Awake()
    {
        ServiceLocator.Register<GlassAction>(this);
    }

    void Start(){
        glassesQueue = new Queue<Glass>();
        poisonInstances = new ConcurrentQueue<GameObject>();

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
        //if current glass is poison glass, remove and destroy firstmost poison instance from poison queue
        if(glassesQueue.Dequeue().GetType() == typeof(PoisonGlass)){
            GameObject _poison;
            poisonInstances.TryDequeue(out _poison);
            Destroy(_poison);
        };

        AddGlass();
        UpdateGlass();
        //DebugGlass();
    }

    void UpdateGlass(){

        //temporary poison queue for updating
        ConcurrentQueue<GameObject> _poisonQueue = new ConcurrentQueue<GameObject>();

        foreach ((Glass glass, int i) in glassesQueue.Select((value, i) => (value, i)))
        {
            glassObjects[i].GetComponent<SpriteRenderer>().sprite = _drinks[glass.glassValue % _drinks.Length];

            if(glass.glassValue == 0){
                GameObject _poison;

                //gets and remove the firstmost poison instance from original queue if it exists, else instantiate a new one
                if(!poisonInstances.TryDequeue(out _poison))
                    _poison = Instantiate(poisonCloud, glassObjects[i].transform.position + new Vector3(0.1f, 0.3f, 0), Quaternion.identity) as GameObject;

                //set poison gameobject parent
                _poison.transform.parent = glassObjects[i].transform;
                //update poison gameobject position to match parent
                _poison.transform.position = glassObjects[i].transform.position + new Vector3(0.1f, 0.3f, 0);
                //reset rotation of poison as it is the same as parent
                _poison.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                //add poison to updated poison queue
                _poisonQueue.Enqueue(_poison);
            }
        }

        //populate the original poison instances queue with the updated poison queue
        poisonInstances = new ConcurrentQueue<GameObject>(_poisonQueue);
    }
    
    void DebugGlass(){
        string debug = "";
        foreach((Glass glass, int i) in glassesQueue.Select((value, i) => (value, i))){
            debug += $"[{i}]{glass}-{glass.glassValue} ";
        }
        Debug.Log(debug);
    }
}
