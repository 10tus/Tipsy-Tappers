using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GlassAction : MonoBehaviour
{
    [SerializeField]
    public Sprite[] _drinks;

    public GameObject[] glassObjects;
    public Queue<Glass> glassesQueue;

    public GameObject poisonCloud;
    List<GameObject> poisonInstances;
    List<Vector2> poisonCoordinates;

    int _queueLimit = 5;

    void Awake()
    {
        ServiceLocator.Register<GlassAction>(this);
    }

    void Start(){
        glassesQueue = new Queue<Glass>();
        poisonInstances = new List<GameObject>();
        poisonCoordinates = new List<Vector2>();

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

        //Destroy all poison gameobject instances in play area
        if(poisonInstances.Count != 0)
        {
            foreach (GameObject p in poisonInstances)
            {
                Destroy(p);   
            }
            

        }

        //clear poisoncontainer and coordinates everytime player removes glass to reset play area
        poisonInstances.Clear();
        poisonCoordinates.Clear();

        AddGlass();
        UpdateGlass();
        //DebugGlass();
    }

    void UpdateGlass(){
        foreach ((Glass glass, int i) in glassesQueue.Select((value, i) => (value, i)))
        {
            glassObjects[i].GetComponent<SpriteRenderer>().sprite = _drinks[glass.glassValue % _drinks.Length];

            if(glass.glassValue == 0){
                GameObject _poison = Instantiate(poisonCloud, glassObjects[i].transform.position + new Vector3(0.1f, 0.3f, 0), Quaternion.identity) as GameObject;

                //checks if there is already poison on play area to prevent multiple instantiation of poison gameobject
                if(!poisonCoordinates.Contains(_poison.transform.position))
                {
                    //add position of instantiated gameobject to coordinates
                    poisonCoordinates.Add(_poison.transform.position);
                    //set poison gameobject to parent
                    _poison.transform.SetParent(glassObjects[i].transform);
                    //reset rotation of poison as it is the same as parent
                    _poison.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    //add poison to container
                    poisonInstances.Add(_poison);
                }
                else
                {
                    Destroy(_poison);
                }
            }
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
