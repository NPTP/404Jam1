using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public GameObject character;
    public PlayerController playerController;

    private Vector3 localRotation;
    private float cameraDistance = 10f;

    public float mouseSens = 4f;
    public float scrollSens = 2f;
    public float orbitDampening = 10f;
    public float scrollDampening = 6f;

    public bool cameraDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = character.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            cameraDisabled = !cameraDisabled;
        }

        if (!cameraDisabled)
        {
            // Rotation
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * mouseSens;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSens;

                // Prevent camera flip
                localRotation.y = Mathf.Clamp(localRotation.y, 0f, 90f);
            }

            // Zoom
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSens;
                scrollAmount *= this.cameraDistance * 0.3f;

                this.cameraDistance += scrollAmount * -1f;

                this.cameraDistance = Mathf.Clamp(this.cameraDistance, 1.5f, 100f);
            }
        }
    }

    // Called after Update()
    private void LateUpdate()
    {
        Quaternion rotationDelta = Quaternion.Euler(this.character.transform.rotation.eulerAngles - this.playerController.pastRotation.eulerAngles);

        Debug.Log(rotationDelta);

        Quaternion qt = Quaternion.Euler(localRotation.y, localRotation.x, 0);

        this.character.transform.rotation = Quaternion.Lerp(this.character.transform.rotation, qt, Time.deltaTime * orbitDampening);

        if (this.transform.localPosition.z != this.cameraDistance * -1f)
        {
            this.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this.transform.localPosition.z, this.cameraDistance * -1f, Time.deltaTime * scrollDampening));
        }
    }
}
