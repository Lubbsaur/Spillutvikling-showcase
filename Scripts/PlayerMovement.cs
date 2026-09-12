using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControl input;

    [SerializeField] private int maximumDistance;
    [SerializeField] private LayerMask clickableLayers;
    [SerializeField] private ParticleSystem clickEffect;

    private NavMeshAgent agent;
    private bool isWalking = false;

    private bool movementLocked = false;
    
    //Legger til event for interact og attack
    public event Action OnInteractAction;
    public event Action OnAttackAction;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        input = new PlayerControl();
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Player.ClickMove.performed += ctx => ClickToMove();
    }

    Vector3 hits;

    void ClickToMove()
    {
        if (movementLocked) return;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, maximumDistance, clickableLayers))
        {

            if (hit.collider.CompareTag("NPC")) return;

            hits = hit.point;
            agent.destination = hit.point;
            ClickRotate(hit.point);

            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    private void ClickRotate(Vector3 position)
    {
    
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        input.Player.Interact.performed += InteractOnPerformed;
        input.Player.Attack.performed += AttackOnPerformed;
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke();
    }

    private void AttackOnPerformed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke();
    }

    private void Update()
    {
        if (agent.hasPath)
        {
            Vector3 targetDirection = agent.steeringTarget - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 10 * Time.deltaTime, 1);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        isWalking = agent.hasPath;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void LockMovement()
    {
        movementLocked = true;
        agent.ResetPath();
        agent.isStopped = true;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
        agent.isStopped = false;
    }
}


