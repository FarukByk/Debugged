using UnityEngine;

public class GameBoy : MonoBehaviour
{
    public bool OnEditMode = false;

    public MeshRenderer[] meshRenderers;
    public Transform Camera;
    public Transform EditModePosition, PlayModePosition;
    public PlayerCont player;
    public CharCont editor;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnEditMode = !OnEditMode;
        }

        if (OnEditMode)
        {
            EditMode();
            editor.stopAction = false;
            player.stopAction = true;
        }
        else
        {
            PlayMode();
            if (!editor.stopAction)
            {        
                editor.StopAction();
            }
            editor.stopAction = true;
            player.stopAction = false;
        }
    }


    void EditMode()
    {
        foreach (MeshRenderer MR in meshRenderers)
        {
            MR.enabled = false;
        }

        Camera.position = Vector3.Lerp(Camera.position, EditModePosition.position, Time.deltaTime * 10);
        Camera.rotation = Quaternion.Lerp(Camera.rotation, EditModePosition.rotation, Time.deltaTime * 10);



    }

    void PlayMode()
    {
        foreach (MeshRenderer MR in meshRenderers)
        {
            MR.enabled = true;
        }

        Camera.position = Vector3.Lerp(Camera.position, PlayModePosition.position, Time.deltaTime * 10);
        Camera.rotation = Quaternion.Lerp(Camera.rotation, PlayModePosition.rotation, Time.deltaTime * 10);



    }
}
