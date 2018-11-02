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

#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                AddForce(Input.GetTouch(0).deltaPosition);
            }
#endif
        }
	}

    void AddForce(Vector3 forcePosition)
    {
        MayTouch = false;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Spawnable");
        foreach (var item in objects)
        {
            item.GetComponent<Rigidbody>().AddExplosionForce(10.0f, forcePosition, 1.0f);
        }
    }
}
