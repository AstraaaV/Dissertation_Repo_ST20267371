using UnityEngine;
using UnityEngine.InputSystem;
public class TextNodeInteract : MonoBehaviour
{
    [Header("Story Content")]
    [TextArea(3, 8)]
    [SerializeField] private string nodeText;

    [Header("UI")]
    [SerializeField] private NodeUI nodeUI;
    [SerializeField] private GameObject promptObj;
    [SerializeField] private TypewriterTMP promptTypewriter;

    [Header("Hide FX After Use")]
    [SerializeField] private GameObject particleToHide;
    [SerializeField] private Collider triggerCollider;

    [Header("Prompt Settings")]
    [SerializeField] private string promptMessage = "Press 'E' to interact";

    [Header("Environmental Changes")]
    [SerializeField] private GameObject[] environmentObjects;

    [Header("Input Settings")]
    [SerializeField] private float pressCooldown = 0.15f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource ambientAudio;

    [SerializeField] private string nodeName = "Node";

    [Header("VFX Settings")]
    [SerializeField] private ParticleSystem nodeParticles;

    private bool isPlayerInRange;
    private bool hasBeenRead;
    private bool hasBeenActivated = false;
    private float nextAllowedPressTime = 0f;

    private void Awake()
    {
        if (promptObj != null)
        {
            promptObj.SetActive(false);
        }

        if(nodeParticles != null)
        {
            nodeParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayerInRange) return;
        if (Keyboard.current == null) return;
        if (!Keyboard.current.eKey.wasPressedThisFrame) return;
        if (Time.time < nextAllowedPressTime) return;

        nextAllowedPressTime = Time.time + pressCooldown;

        if (nodeUI.IsOpen)
        {
            nodeUI.Close();

            if(!hasBeenRead)
            {
                if(promptObj != null) promptObj.SetActive(true);
                if (promptTypewriter != null) promptTypewriter.Type(promptMessage);
            }

            return;
        }

        nodeUI.Open(nodeText);
        hasBeenRead = true;

        if (!hasBeenActivated)
        {
            hasBeenActivated = true;

            Debug.Log(gameObject.name + " pressed E.");

            ActivateEnvironment();

            Debug.Log(nodeName + " activated.");

            if (CompletionCounter.Instance != null)
            {
                CompletionCounter.Instance.RegisterNodeActivation();
            }

            if (particleToHide != null)
            {
                particleToHide.SetActive(false);
            }

            //if (triggerCollider != null)
            //{
            //    triggerCollider.enabled = false;
            //}

            if (ambientAudio != null)
            {
                Debug.Log(nodeName + " ambient audio played.");

                ambientAudio.Play();
            }

            if(nodeParticles != null)
            {
                nodeParticles.Play();
            }
        }

        if (promptObj != null)
        {
            promptObj.SetActive(false);
        }
    }

    private void ActivateEnvironment()
    {
        foreach(GameObject obj in environmentObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log(gameObject.name + " detected player.");

        isPlayerInRange = true;

        if(nodeUI != null && nodeUI.IsOpen) return;

        if(promptObj != null) promptObj.SetActive(true);
        if (promptTypewriter != null)
        {
            promptTypewriter.StopTyping();
            promptTypewriter.Type(promptMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = false;

        if (promptTypewriter != null) promptTypewriter.StopTyping();
        if (promptObj != null) promptObj.SetActive(false);

        if (nodeUI != null && nodeUI.IsOpen)
        {
            nodeUI.Close();
        }
    }
}
