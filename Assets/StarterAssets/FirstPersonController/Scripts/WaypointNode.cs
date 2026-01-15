using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    [SerializeField] private GameObject activatedVisual;
    private bool isActivated = false;

    public void Activate(SessionManager sessionManager)
    {
        if (isActivated) return;

        isActivated = true;

        if (activatedVisual != null)
            activatedVisual.SetActive(true);

        sessionManager.RegisterActivation();
    }
}
