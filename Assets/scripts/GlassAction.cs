using System.Collections.Generic;
using UnityEngine;

public class GlassAction : MonoBehaviour
{
    public Queue<Glass> glassesQueue;
    int _queueLimit = 5;

    void Start()
    {
        glassesQueue = new Queue<Glass>();

        while(glassesQueue.Count < _queueLimit){
            AddGlass();
        }
    }

    private void AddGlass()
    {
        glassesQueue.Enqueue(GlassFactory.GenerateGlass());       
    }

    public void ReplaceGlass()
    {
        Destroy(glassesQueue.Dequeue());
        AddGlass();
    }
    
}
