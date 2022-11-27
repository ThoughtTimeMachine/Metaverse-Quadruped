using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool isOpen = false;
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;
    [SerializeField] AudioClip activationButton;
    private void OnTriggerEnter(Collider other)
    {
        if (!AnimationUtilityClass.AnimatorIsPlaying(animator))
        {
            OpenCloseDoor();
        }
    }

    private void OpenCloseDoor()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(activationButton);

        if (animator != null)
        {
            if (!isOpen)
            {
                animator.Play("OpenHatch", 0, 0f);
                audioSource.PlayOneShot(openDoor);
                isOpen = true;
            }
            else
            {
                if (!AnimationUtilityClass.AnimatorIsPlaying(animator))
                {
                    animator.Play("CloseHatch", 0, 0f);
                    audioSource.PlayOneShot(closeDoor);
                    isOpen = false;
                }
            }
        }
    }

}
