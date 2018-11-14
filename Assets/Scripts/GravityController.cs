using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityController : MonoBehaviour {

    public static GravityController Instance;

    Vector3 currentGravity = Vector3.zero;
    public Vector3 CurrentGravity
    {
        get
        {
            return currentGravity;
        }
        set
        {
            currentGravity = value;
            Physics.gravity = currentGravity;
        }
    }

    bool gyroActive = true;
    public bool GyroActive
    {
        get
        {
            return gyroActive;
        }
        set
        {
            GyroActive = value;
            if (SystemInfo.supportsGyroscope)
            {
                if (GyroActive)
                    StartCoroutine(gyroRoutine);
                else
                    StopCoroutine(gyroRoutine);
            }
        }
    }

    IEnumerator gyroRoutine;

    private void Awake()
    {
        Instance = this;
        gyroRoutine = gyroGravity();
    }

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            StartCoroutine(gyroRoutine);
        }
    }

    /// <summary>
    /// I'm using an enumerator because update sets the gravity too fast, which is not necessary.
    /// </summary>
    /// <returns></returns>
    IEnumerator gyroGravity()
    {
        while (true)
        {
            if (currentGravity != Vector3.zero)
                Physics.gravity = Input.gyro.gravity * 4.9f;

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void NoGravity()
    {
        CurrentGravity = Vector3.zero;
    }

    public void NormalGravity()
    {
        CurrentGravity = new Vector3(0,-9.8f,0);
    }

    public void WaterGravity()
    {
        CurrentGravity = new Vector3(0, -4.9f, 0);
    }

    public void SetGravity(Vector3 gravity)
    {
        CurrentGravity = gravity;
    }
}
