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
    private bool hasBeenUsed;

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
        if (hasBeenUsed) return;

        if (isPlayerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (nodeUI != null && nodeUI.IsOpen) return;

            nodeUI.Open(nodeText);

            hasBeenUsed = true;

            if (promptObj != null) promptObj.SetActive(false);

            if (particleToHide != null) particleToHide.SetActive(false);

            if (triggerCollider != null) triggerCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenUsed) return;
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = true;

        if (promptObj != null) promptObj.SetActive(true);

        if (promptTypewriter != null)
        {
            promptTypewriter.Type(promptMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasBeenUsed) return;
        if (!other.CompareTag("Player")) return;

        isPlayerInRange = false;

        if(promptTypewriter != null) promptTypewriter.StopTyping();

        if (promptObj != null) promptObj.SetActive(false);

        if(nodeUI != null && nodeUI.IsOpen)
        {
            nodeUI.Close();
        }
    }
}
