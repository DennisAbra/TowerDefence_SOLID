using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;


public class PathAgent : MonoBehaviour
{
    int currentIndex = 0;
     float speed = 10;
    Animator animator;
    float t = 0;

    public float Speed { get; private set; }

    void OnEnable()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        if (UnitManager.walkablePositions != null)
            transform.position = UnitManager.walkablePositions[currentIndex];
        animator.SetBool("isWalking", true);
        Speed = speed;
        t = 0;
        currentIndex = 0;
    }

    void Update()
    {
        if (transform.position == UnitManager.walkablePositions[UnitManager.walkablePositions.Count - 1])
            gameObject.SetActive(false);

        t += Time.deltaTime * speed;

        if(UnitManager.walkablePositions.Count > currentIndex + 1)
        {
            transform.position = Vector3.Lerp(UnitManager.walkablePositions[currentIndex], UnitManager.walkablePositions[currentIndex + 1], t);

            if (t > 1 && UnitManager.walkablePositions[currentIndex + 1] != null)
            {
                currentIndex++;
                t = 0;
                //Spawn next enemy in line
                //Send out event that tells another unit to activate
            }
        }
    }

}