using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AttachToObject(Transform obj)
    {
        transform.parent = obj;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
