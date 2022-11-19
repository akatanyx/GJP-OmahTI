using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region BatasLevel

    [HideInInspector]
    public float xMin;
    [HideInInspector]
    public float xMax;
    [HideInInspector]
    public float yMin;
    [HideInInspector]
    public float yMax;

    #endregion


    [SerializeField] protected GameObject player;
    protected LevelManager levelManager;

    void Start()
    {
        Initialization();
    }
    
    
    protected virtual void Initialization()
    {
        player = FindObjectOfType<Character>().gameObject;
        levelManager = FindObjectOfType<LevelManager>();
        xMin = levelManager.levelSize.min.x;
        xMax = levelManager.levelSize.max.x;
        yMin = levelManager.levelSize.min.y;
        yMax = levelManager.levelSize.max.y;
    }
}
