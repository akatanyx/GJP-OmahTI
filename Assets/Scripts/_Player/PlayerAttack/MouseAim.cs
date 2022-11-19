using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;
    [SerializeField] private GameObject parentObject;
    private CharacterFlip parent;
    public float CurrentAimAngle { get; set; }
    public float CurrentAimAngleAbsolute { get; set; }
    Camera mainCamera;
    GameObject reticle;

    private Vector3 direction, mousePosition, reticlePosition;
    private Vector3 currentAim = Vector3.zero, currentAimAbsolute = Vector3.zero;
    private Quaternion initialRotation, lookRotation;


    void Start()
    {
        Cursor.visible = false;
        mainCamera = Camera.main;
        initialRotation = transform.rotation;
        reticle = Instantiate(reticlePrefab);
        reticle.transform.parent = parentObject.transform;
        parent = GetComponentInParent<CharacterFlip>();
    }
    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        MoveReticle();
        RotateWeapon();
        //FlipToAim();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log(CurrentAimAngle);
        //}
    }

    void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        direction = mainCamera.ScreenToWorldPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;

        currentAimAbsolute = direction - transform.position;
        if (!parent.isFlipped)
        {
            currentAim = direction - transform.position;
        }
        else
        {
            currentAim = transform.position - direction;
        }


    }
    private void RotateWeapon()
    {
        if (currentAim != Vector3.zero && direction != Vector3.zero)
        {
            // Get Angle
            CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
            CurrentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg;

            CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);

            lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = lookRotation;
        }
        else
        {
            CurrentAimAngle = 0f;
            transform.rotation = initialRotation;
        }
    }
    void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity;
        reticle.transform.position = reticlePosition;
    }
}
