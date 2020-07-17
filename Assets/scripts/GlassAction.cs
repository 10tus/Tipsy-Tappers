using System.Collections.Generic;
using UnityEngine;

public class GlassAction : MonoBehaviour
{
   
    private List<int> shotGlassValue;
    
    public GameObject[] model;
    public Sprite[] col;
    private PlayerSystem playerSystem;
    private List<GameObject> poisonContainer;
    private List<Vector2> coordinates;
    private playerActions actions;

    void Start()
    {
        playerSystem = PlayerSystem.instance;
        actions = playerSystem._player;
        shotGlassValue = new List<int>();
        coordinates = new List<Vector2>();
        poisonContainer = new List<GameObject>();
        for (int i = 0; i < 5;i++)
        {
            shotGlassValue.Add(Randomizer());
            UpdateGlass();

        }
    }

    int Randomizer()
    {       
        return Random.Range(0, 5);
    }
    // Update is called once per frame
    public void RemoveGlass(int type)
    {
        actions.Consequences(shotGlassValue[0],type);
        
        //Destroy all poison gameobject instances in play area
        if(poisonContainer.Count != 0)
        {
            foreach (GameObject p in poisonContainer)
            {
                Destroy(p);   
            }
            

        }

        //clear poisoncontainer and coordinates everytime player removes glass to reset play area
        poisonContainer.Clear();
        coordinates.Clear();

        //remove glass at hand of player
        shotGlassValue.RemoveAt(0);
        //add another glass means create new randomized value from 0-5 then add it from list of shotglass values
        AddGlass();

        //change all sprites of all shotglass model (I referenced it from model[])
        UpdateGlass();

    }

    private void UpdateGlass()
    {
        for (int i = 0; i < shotGlassValue.Count; i++)
        {
            //for every shot glass model replace the sprites with sprites coming from col[] that uses index from shotglassvalue
            model[i].GetComponent<SpriteRenderer>().sprite = col[shotGlassValue[i]];

            //if poison shot is at play area show poison animation thru gameobject
            if(shotGlassValue[i] == 1)
            {
                //instantiate poison gameobject, adjust vector3 offset to change position of poison
                GameObject go = Instantiate(model[5],model[i].transform.position+new Vector3(0.1f,0.3f,0),Quaternion.identity) as GameObject;
                
                //checks if there is already poison on play area to prevent multiple instantiation of poison gameobject
                if(!coordinates.Contains(go.transform.position))
                {
                    //add position of instantiated gameobject to coordinates
                    coordinates.Add(go.transform.position);
                    //set poison gameobject to parent
                    go.transform.SetParent(model[i].transform);
                    //reset rotation of poison as it is the same as parent
                    go.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    //add poison to container
                    poisonContainer.Add(go);
                }
                else
                {
                    Destroy(go);
                }

            }

        }
    }
    private void AddGlass()
    {
        if(shotGlassValue.Count < 6)
        {
            shotGlassValue.Add(Randomizer());           
        }
    }
}
