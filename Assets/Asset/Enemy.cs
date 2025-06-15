using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Animator animator;
    public bool isMoving = true;
    public bool isAttacking = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveTowardsPlayer();
        }
        // agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        // animator.SetBool("Walk", true);
        this.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public void MoveTowardsPlayer()
    {
        if (agent != null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
            animator.SetBool("Walk", true);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle player collision logic here
            isAttacking = true;
            if (isAttacking)
            {
                OnAttack();
            }

            // StartCoroutine(ResetAttack());
        }
        // else
        // {
        //     StartCoroutine(ResetMovement());
        // }
    }

    public void OnAttack()
    {
        agent.SetDestination(transform.position); // Stop moving towards the player
        Debug.Log("Attacking Player!");
        animator.SetTrigger("Attack");
        isMoving = false;
        animator.SetBool("Walk", false);
        if (isAttacking)
        {
            StartCoroutine(ResetAttack());
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
            Debug.Log("Player has left the trigger area.");

            // isMoving = true;
            // animator.SetBool("Walk", true);
            StartCoroutine(ResetMovement());
        }
    }

    private IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(1f);
        if (!isAttacking)
        {
            isMoving = true;
            animator.SetBool("Walk", true);
            MoveTowardsPlayer();
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2f);
        if (isAttacking)
        {
            OnAttack();
            // Reset stopping distance to allow movement towards player
        }

    }

}
