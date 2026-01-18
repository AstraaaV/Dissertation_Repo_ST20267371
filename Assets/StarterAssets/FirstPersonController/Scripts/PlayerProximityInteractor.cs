using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerProximityInteractor : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;

    private InteractionSystem currentNode;
    private SessionManager session;

    private void Awake()
    {
        session = FindObjectOfType<SessionManager>();
    }
    private void OnEnable()
    {
        if (interactAction == null) return;

        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        if (interactAction == null) return;

        interactAction.action.performed -= OnInteract;
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (currentNode == null) return;

        Debug.Log("Interact pressed (in range)");
        currentNode.Activate(session);
        currentNode = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out InteractionSystem node))
        {
            if (node.Activated) return;

            currentNode = node;
            Debug.Log($"In range of {node.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractionSystem node))
        {
            if (currentNode == node)
            {
                currentNode = null;
                Debug.Log($"Left range of {node.name}");
            }
        }
    }
}
