using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;


public class PathAgent : MonoBehaviour
{
    int currentIndex = 0;
    public float speed = 1;
    Animator animator;
    float t = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = UnitManager.walkablePositions[currentIndex];
        animator.SetBool("isWalking", true);
    }

    void Update()
    {
        
        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(UnitManager.walkablePositions[currentIndex], UnitManager.walkablePositions[currentIndex+1], t);
        if(t > 1)
        {
            if (UnitManager.walkablePositions[currentIndex+2] != null)
            currentIndex++;
            t = 0;
            //Spawn next enemy in line
        }

    }

}