using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchForce : MonoBehaviour {

    #region Vars
    bool mayTouch;
    #endregion
    #region Properties
    public bool MayTouch
    {
        get
        {
            return mayTouch;
        }

        set
        {
            mayTouch = value;
        }
    }
    #endregion

    public static TouchForce Instance;

    [SerializeField]
    float forceAmount = 1000.0f;

    [SerializeField]
    float forceRadius = 100.0f;

    [SerializeField]
    GameObject touchEffect;

    #region Initialization
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Update () {
		if (MayTouch)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
                mousePos.z = 24.0f; // Distance between the camera and the objects - I put 1 point less so the rings will turn around randomly
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                AddForce(mousePos);
            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 deltaPos = Input.GetTouch(0).deltaPosition;
                deltaPos.z = 24.0f;
                deltaPos = Camera.main.ScreenToWorldPoint(deltaPos);
                AddForce(deltaPos);
            }
#endif
        }
    }

    void AddForce(Vector3 forcePosition)
    {
        MayTouch = false;
        // Finds every spawnable and add effect to it
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Spawnable");
        foreach (var item in objects)
        {
            item.GetComponent<Rigidbody>().AddExplosionForce(forceAmount, forcePosition, forceRadius);
            item.GetComponent<SpawnablePositionController>().IsActive = true;
        }
        // Set Gravity back to normal
        GravityController.NormalGravity();
        // Spawn effect
        Instantiate(touchEffect, forcePosition, Quaternion.identity);
    }
}
