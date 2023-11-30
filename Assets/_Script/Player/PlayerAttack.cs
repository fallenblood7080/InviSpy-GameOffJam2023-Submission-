using Extension;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackDur;

    private Animator animator;


    private PlayerMovement playerMovement;

    private bool canAttack;
    private WaitForSeconds attackTime;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        attackTime = new(attackDur);
    }
    
    private void Update()
    {
        canAttack = !playerMovement.IsMoving &&
                     Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange, enemyLayer) &&
                     hit.collider.CompareTag("Enemy");

        if (canAttack)
        {
            "Press Left Mouse Button to Attack".Log();
            if (InputManager.GetInstance.IsInteractPressed)
            {
                playerMovement.Stand();
                animator!.SetTrigger("Stab");
            }
        }

    }
}
