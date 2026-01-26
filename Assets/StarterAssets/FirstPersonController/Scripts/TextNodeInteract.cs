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

    private bool isPlayerInRange;
    private bool hasBeenRead;
    private float nextAllowedPressTime = 0f;
    [SerializeField] private float pressCooldown = 0.15f;

    private void Awake()
    {
        if (promptObj != null)
        {
            promptObj.SetActive(false);
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

        if (nodeUI == null) return;

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

        if(promptObj != null) promptObj.SetActive(false);
        if(particleToHide != null) particleToHide.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = true;

        if(nodeUI != null && nodeUI.IsOpen) return;

        if(promptObj != null) promptObj.SetActive(true);
        if (promptTypewriter != null) promptTypewriter.Type(promptMessage);
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
