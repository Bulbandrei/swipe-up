using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchForce : MonoBehaviour {

    #region Vars
    #endregion
    #region Properties
    #endregion

    public static TouchForce Instance;

    [SerializeField]
    float forceAmount = 1250.0f;

    [SerializeField]
    float forceRadius = 10.0f;

    [SerializeField]
    GameObject touchEffect;

    AudioSource audioSource;

    #region Initialization
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 24.0f; // Distance between the camera and the objects - I put 1 point less so the rings will turn around randomly
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            AddForce(mousePos);
            AudioController.Instance.PlayAudio(AudioType.Force);
        }
    }

    void AddForce(Vector3 forcePosition)
    {
        // Finds every spawnable and add effect to it
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Spawnable");
        foreach (var item in objects)
        {
            if (forcePosition.y > item.transform.position.y)
                continue;

            var delta = item.transform.position - forcePosition;
            var force = delta + Vector3.up;
            force.z = 0;
            
            float forceMod = 1 - (force.magnitude / forceRadius);
            force = force * Mathf.Clamp01(forceMod);
            force.z = 0.0f;
            item.GetComponent<Rigidbody>().AddForce(force * forceAmount);
            item.GetComponent<Rigidbody>().AddTorque(Vector3.right * forceAmount * 100);
            //item.GetComponent<Rigidbody>().AddTorque(Vector3.up * 100000, ForceMode.Acceleration);
            //item.GetComponent<Rigidbody>().AddTorque(Vector3.right * 100000, ForceMode.Acceleration);

            item.GetComponent<SpawnablePositionController>().IsActive = true;
        }
        // Set Gravity back to normal
        GravityController.WaterGravity();
        // Spawn effect
        Instantiate(touchEffect, forcePosition, Quaternion.identity);
    }
}
