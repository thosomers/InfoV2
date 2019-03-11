using UnityEngine;

public abstract class GameScene : MonoBehaviour
{
    public Camera SceneCamera;
    public GameObject Scene;
    public Canvas gameCanvas;
	
    private Camera lastCam;
	
    public void Show()
    {
        Scene.SetActive(true);
        foreach (var cam in Camera.allCameras)
        {
            if (cam != SceneCamera)
            {
                cam.enabled = false;
                lastCam = cam;
            }
        }

        SceneCamera.enabled = true;
        gameCanvas.enabled = true;
    }
	
    public void Hide()
    {
        lastCam.enabled = true;
        Scene.SetActive(false);
        gameCanvas.enabled = false;
    }
	
}