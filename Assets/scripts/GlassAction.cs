using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using System.Linq;


public class GlassAction : MonoBehaviour
{
    [SerializeField]
    public Sprite _deathDrink;
    [SerializeField]
    public Sprite[] _regularDrinks;

    public GameObject[] glassObjects;
<<<<<<< HEAD
    Queue<Glass> glassesQueue;
    public Glass currentGlass => glassesQueue.Peek();

    public GameObject poisonCloud;
    ConcurrentQueue<GameObject> poisonPool;
    ConcurrentQueue<GameObject> activePoisons;
=======
    internal Queue<Glass> glassesQueue;
    public Glass currentGlass => glassesQueue.Peek();

    public GameObject poisonCloud;
    internal ConcurrentQueue<GameObject> poisonPool;
    internal ConcurrentQueue<GameObject> activePoisons;
>>>>>>> develop

    int _queueLimit = 5;

    void Awake()
    {
        ServiceLocator.Register<GlassAction>(this);
    }

    void Start(){
        glassesQueue = new Queue<Glass>();
        poisonPool = new ConcurrentQueue<GameObject>();
        activePoisons = new ConcurrentQueue<GameObject>();

        while(glassesQueue.Count < _queueLimit){
            AddGlass();
        }

        UpdateGlass();
        //Helper.DebugGlass();
    }

    void AddGlass()
    {
        glassesQueue.Enqueue(GlassFactory.GenerateGlass());       
    }

    public void ReplaceGlass()
    {
        //if current glass is poison glass, return current active poison instance to poison pool
        if(glassesQueue.Dequeue().GetType() == typeof(PoisonGlass)){
            GameObject _poison;
            activePoisons.TryDequeue(out _poison);

            //Hide poison offscreen and disable, then store to poison pool
<<<<<<< HEAD
            _poison.FixPosition();
=======
            FixPosition(_poison);
>>>>>>> develop
            _poison.SetActive(false);
            poisonPool.Enqueue(_poison);
        };

        AddGlass();
        UpdateGlass();
<<<<<<< HEAD
        // Helper.DebugGlass();
=======
        //Helper.DebugGlass();
>>>>>>> develop
        // Helper.DebugPoisonInstances();
    }

    void UpdateGlass(){
        //temporary poison queue for updating
        ConcurrentQueue<GameObject> _poisonQueue = new ConcurrentQueue<GameObject>();

        foreach ((Glass glass, int i) in glassesQueue.Select((value, i) => (value, i)))
        {
            glassObjects[i].GetComponent<SpriteRenderer>().sprite
                = (glass.glassValue == 0) 
                    ? _deathDrink //Set death drink sprite, poison drinks have 0 glassValue
                    : _regularDrinks[glass.glassValue % _regularDrinks.Length]; //Prevent array out-of-bounds using modulo with drinks length

            if(glass.GetType() == typeof(PoisonGlass)){
                GameObject _poison;

                //try getting poison from active poison instances if it exists,
                //else if no active poison instances left, try getting from poison pool. 
                //if fails, instantiate a new poison gameobject if all poison instances count < 5
                if(activePoisons.TryDequeue(out _poison)) { }
                else if (poisonPool.TryDequeue(out _poison)) { }
                else if ((poisonPool.Count + activePoisons.Count) < 5)
<<<<<<< HEAD
                    _poison = Instantiate(poisonCloud, glassObjects[i].transform.position + new Vector3(0.1f, 0.3f, 0), Quaternion.identity) as GameObject;
=======
                    _poison = Instantiate(poisonCloud) as GameObject;
>>>>>>> develop


                //activate poison instance
                _poison.SetActive(true);
                //set poison gameobject parent
<<<<<<< HEAD
                _poison.FixPosition(glassObjects[i]);
=======
                FixPosition(_poison, glassObjects[i]);
>>>>>>> develop
                //add poison to updated poison queue
                _poisonQueue.Enqueue(_poison);
            }
        }

        //populate active poisons with updated poison queue
        activePoisons = new ConcurrentQueue<GameObject>(_poisonQueue);
    }

<<<<<<< HEAD
    
}

internal static class Helper{
    static GlassAction context = ServiceLocator.Resolve<GlassAction>();

    internal static void FixPosition(this GameObject poison, GameObject parent = null){
        //If parent is null, set parent to last glassObject element offscreen to hide poison
        if(parent is null){
            parent = context.glassObjects.Last();
=======
    void FixPosition(GameObject poison, GameObject parent = null){
        //If parent is null, set parent to last glassObject element offscreen to hide poison
        if(parent is null){
            parent = glassObjects.Last();
>>>>>>> develop
        }

        //Set poison Parent and Position
        poison.transform.parent = parent.transform;
        poison.transform.position = parent.transform.position + new Vector3(0.1f, 0.3f, 0);

        //Set Rotation
        poison.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

<<<<<<< HEAD
    // Add internal modifier to glassesQueue, poisonPool, and/or activePoisons if you want to use this method
    // internal static void DebugGlass(){
    //     string debug = "";
    //     foreach((Glass glass, int i) in context.glassesQueue.Select((value, i) => (value, i))){
    //         debug += $"[{i}]{glass}-{glass.glassValue} ";
    //     }
    //     Debug.Log(debug);
    // }

    
    // internal static void DebugPoisonInstances(){
    //     Debug.Log($"Poison pool count: {context.poisonPool.Count}");
    //     Debug.Log($"Active poison count: {context.activePoisons.Count}");
    //     Debug.Log($"Total poison instances count: {context.poisonPool.Count + context.activePoisons.Count}");
    // }
=======
    
>>>>>>> develop
}

