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
                mousePos.z = 30.0f; // Distance between the camera and the objects
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                AddForce(mousePos);
            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var deltaPos = Input.GetTouch(0).deltaPosition;
                deltaPos.z = 10.0f;
                AddForce(Input.GetTouch(0).deltaPosition);
            }
#endif
        }
    }

    void AddForce(Vector3 forcePosition)
    {
        Debug.Log(forcePosition);
        MayTouch = false;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Spawnable");
        foreach (var item in objects)
        {
            item.GetComponent<Rigidbody>().AddExplosionForce(forceAmount, forcePosition, forceRadius);
        }
        GravityController.NormalGravity();
    }
}
