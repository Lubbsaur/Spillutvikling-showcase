using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportTarget;               
    public ScreenFader screenFader;                
    public DungeonMusicController musicController; 
    public bool enteringDungeon = true;            

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.LockMovement();
            }

            StartCoroutine(screenFader.FadeOutIn(() =>
            {
                NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.ResetPath();
                    agent.isStopped = true;

                    agent.Warp(teleportTarget.position);

                    agent.isStopped = false;
                }
                else
                {
                    other.transform.position = teleportTarget.position;
                }

                if (movement != null)
                {
                    movement.UnlockMovement();
                }

                if (musicController != null)
                {
                    if (enteringDungeon)
                        musicController.EnterDungeon();
                    else
                        musicController.ExitDungeon();
                }
            }));
        }
    }
}








