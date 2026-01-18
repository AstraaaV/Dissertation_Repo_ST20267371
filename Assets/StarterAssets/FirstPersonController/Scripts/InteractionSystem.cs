using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private GameObject activatedVisual;
    private bool activated;

    public bool Activated => activated;

    public void Activate()
    {
        if (activated) return;
        activated = true;

        if(activatedVisual != null)
        {
            activatedVisual.SetActive(true);
        }

        Debug.Log($"{name} activated.");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
