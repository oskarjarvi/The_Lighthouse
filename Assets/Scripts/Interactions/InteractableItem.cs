using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableItem : MonoBehaviour, InteractableItemBase
{
    private Animator animator;
    public string animationBool;
    [SerializeField]
    private string _prompt;
    [SerializeField]
    private string _failPrompt;

    private bool isBlocked = false;

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
        // If the door hasn't collided with anything, do this
        if (!isBlocked)
        {
            _audioSource.Play();
            animator.speed = 1f;
            animator.SetBool(animationBool, true);
        }
        // If the door has collided with things and haven't gone into this statement before
        else if (isBlocked)
        {
            ppSystem.PopUp(_failPrompt);
        }
    }

    // Check if door collides with doorblocker
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorBlocker"))
        {
            _audioSource.Stop();

            animator.speed = 0f;
            isBlocked = true;
            // Reset hasInteracted flag when the door gets blocked
            hasInteracted = false;

            ppSystem.PopUp(_failPrompt);

        }
    }

    // Check if doorblocker is removed
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorBlocker"))
        {
            isBlocked = false;
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

        currentCamera.enabled = false;

        if (playerObject != null)
        {
            Destroy(playerObject);
        }


    }

    private void ResetGame()
    {
        SceneManager.LoadScene("Menu");
    }

}
