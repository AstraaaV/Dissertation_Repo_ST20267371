using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownCamera : MonoBehaviour
{
    public GameObject mainCameraObject;
    public GameObject topDownCameraObject;
    
    void Start()
    {
        mainCameraObject.SetActive(true);
        topDownCameraObject.SetActive(false);
    }

    void Update()
    {
       if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
       {
            bool isMainCameraActive = mainCameraObject.activeSelf;

            mainCameraObject.SetActive(!isMainCameraActive);
            topDownCameraObject.SetActive(isMainCameraActive);
        }

    }
}
