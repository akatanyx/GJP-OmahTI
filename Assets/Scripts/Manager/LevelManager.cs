using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Bounds levelSize;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(levelSize.center, levelSize.size);
    }

}
