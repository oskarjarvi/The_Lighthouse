using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, InteractableItemBase
{
    private Animator animator;
    public string animationBool;
    [SerializeField]
    private string _prompt;
    [SerializeField]
    private string _failPrompt;

    private bool isAnimationPaused = false;

    public Rigidbody rb => null;

    public bool hasInteracted = false;
    public string InteractionPrompt => _prompt;

    public bool Interacted => hasInteracted;

    private GameObject playerObject;

    private PopupSystem ppSystem;

    private AudioSource _audioSource;

    public AudioClip _audioClip;

    public float delay = 1f;

    public Camera _deathCamera;
    public Camera currentCamera;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            ppSystem = playerObject.GetComponent<PopupSystem>();

        }
    }

    public void Interact()
    {
        if (!isAnimationPaused)
        {

            animator.SetBool(animationBool, true);
            //Invoke("PlaySound", delay);

        }
        else
        {
            ppSystem.PopUp(_failPrompt);
        }


        hasInteracted = true;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorBlocker"))
        {
            _audioSource.Stop();

            if (playerObject != null)
            {
                ppSystem.PopUp(_failPrompt);
            }

            // Pause the animation by setting the speed to 0
            if (!isAnimationPaused)
            {
                animator.speed = 0f;
                isAnimationPaused = true;
                hasInteracted = false;
                animator.SetBool(animationBool, false);

            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorBlocker"))
        {

            if (isAnimationPaused)
            {
                _audioSource.Play();
                animator.speed = 1f;
                isAnimationPaused = false;
            }
        }
    }
    private void PlaySound()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
    private void StopSound()
    {
        _audioSource.Stop();
    }
    private void TriggerLastCamera()
    {

        _deathCamera.gameObject.SetActive(true);

        currentCamera.enabled= false;

        if (playerObject != null)
        {
            Destroy(playerObject);
        }
        _deathCamera.GetComponent<Animator>().SetTrigger("startEndAnim");
    }

}
