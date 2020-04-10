using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;


public class PathAgent : MonoBehaviour, IMover, IReuseable
{
    int currentIndex = 0;
    Animator animator;
    float t = 0;

    public float Speed { get; set; }

    public void Reset()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        if (UnitManager.walkablePositions != null)
            transform.position = UnitManager.walkablePositions[currentIndex];
        animator.SetBool("isWalking", true);
        t = 0;
        currentIndex = 0;
    }

    public void Move()
    {
        if (transform.position == UnitManager.walkablePositions[UnitManager.walkablePositions.Count - 1])
            gameObject.SetActive(false);

        t += Time.deltaTime * Speed;

        if (UnitManager.walkablePositions.Count > currentIndex + 1)
        {
            transform.position = Vector3.Lerp(UnitManager.walkablePositions[currentIndex], UnitManager.walkablePositions[currentIndex + 1], t);

            if (t > 1 && UnitManager.walkablePositions[currentIndex + 1] != null)
            {
                currentIndex++;
                t = 0;
            }
        }
    }

}