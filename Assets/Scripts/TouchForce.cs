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
    float forceAmount = 1250.0f;

    [SerializeField]
    float forceRadius = 10.0f;

    [SerializeField]
    GameObject touchEffect;

    #region Initialization
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Update () {
		//if (MayTouch)
  //      {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
                mousePos.z = 24.0f; // Distance between the camera and the objects - I put 1 point less so the rings will turn around randomly
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                AddForce(mousePos);
            }
        //}
    }

    void AddForce(Vector3 forcePosition)
    {
        MayTouch = false;
        // Finds every spawnable and add effect to it
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Spawnable");
        foreach (var item in objects)
        {
            forcePosition.z = item.transform.position.z + 0.15f;
            item.GetComponent<Rigidbody>().AddExplosionForce(forceAmount, forcePosition, forceRadius);
            item.GetComponent<SpawnablePositionController>().IsActive = true;
        }
        // Set Gravity back to normal
        GravityController.WaterGravity();
        // Spawn effect
        Instantiate(touchEffect, forcePosition, Quaternion.identity);
    }
}
