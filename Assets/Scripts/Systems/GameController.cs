using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Init();
        }
    }
    private void Init()
    {
        ObjectPoolManager.OutSideInit();
    }
    private void Start()
    {
        CannonManager.SetAllState(false);

        CannonManager.SetCannonState(CannonPositions.FirstLeft, true);
    }
}
