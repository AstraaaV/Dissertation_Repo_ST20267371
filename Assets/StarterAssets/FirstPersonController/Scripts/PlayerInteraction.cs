using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private WaypointNode currentNode;
    private SessionManager sessionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNode == null) return;

        if(Input.GetKeyDown(interactKey))
        {
            currentNode.Activate(sessionManager);
            currentNode = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WaypointNode node))
        {
            currentNode = node;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out WaypointNode node))
        {
            if (currentNode == node)
                currentNode = null;
        }
    }
}
