using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShip : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    public float moveTime = 0.0f;

    bool moving = false;

    float timer = 0.0f;
    int dir = 1;

    private void Start()
    {
        startPos = transform.position;
    }
    public void SetShipUp(Vector3 dir, float dist, float time)
    {
        startPos = transform.position;
        
        endPos = startPos + dir * dist;

        moveTime = time;

        timer = 0.0f;
    }
    public void MoveShip(bool value)
    {
        moving = value;
    }
    private void FixedUpdate()
    {
        if(!moving)
        {
            return;
        }

        transform.position = Vector3.Lerp(startPos, endPos, timer);

        timer += 1.0f/moveTime * dir * Time.fixedDeltaTime;

        if(timer > 1.0f)
        {
            dir = -1;
            return;
        }

        if (timer < 0.0f)
        {
            dir = 1;
        }

    }
}
