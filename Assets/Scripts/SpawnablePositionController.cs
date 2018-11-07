using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Z position of the GameObject based on rigidbody Y force
/// </summary>
public class SpawnablePositionController : MonoBehaviour {

    float minZPosition;

    [SerializeField]
    float maxZPosition = 10.0f;

    bool isActive;

    public bool IsActive
    {
        get
        {
            return isActive;
        }

        set
        {
            isActive = value;
            if (isActive)
            {
                StartCoroutine(LerpPosition());
                StartCoroutine(LerpRotation());
            }
        }
    }

    private void Awake()
    {
        minZPosition = transform.position.z;
    }

    void Start () {
		
	}

    IEnumerator LerpPosition()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 startPos = transform.position;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        while (rigidbody.velocity.y > 0)
            yield return null;
        //GetComponent<Renderer>().material.color = Color.red;
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        //while(startPos.z < maxZPosition)
        //{
        //    startPos.z = Mathf.Lerp(minZPosition, maxZPosition, )
        //}
    }

    IEnumerator LerpRotation()
    {
        Vector3 desiredRotation = new Vector3(90, 0, 0);
        while (IsActive)
        {
            //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, desiredRotation, Time.deltaTime / 2);
            yield return new WaitForFixedUpdate();
        }
    }
}
