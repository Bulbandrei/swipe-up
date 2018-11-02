using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    #region Constants
    const int SPAWN_AMOUNT = 5;
    #endregion

    public static Spawner Instance;

    [SerializeField]
    GameObject RingPrefab;

    #region Vars
    int objsOnScreen;
    #endregion

    #region Properties
    public int ObjsOnScreen
    {
        get
        {
            return objsOnScreen;
        }

        set
        {
            objsOnScreen = value;
            if (objsOnScreen == 0)
            {
                SpawnObjs();
            }
        }
    }
    #endregion

    #region Initialization
    private void Awake()
    {
        Instance = this;
    }
    void Start () {
        SpawnObjs();
	}
    #endregion
	
	void SpawnObjs()
    {
        GravityController.NoGravity();
        for (int i = 0; i < SPAWN_AMOUNT; i++)
        {
            Instantiate(RingPrefab, new Vector3(Random.Range(-5.0f, 5.0f), transform.position.y, transform.position.z), Quaternion.identity);
            ObjsOnScreen++;
        }
        TouchForce.Instance.MayTouch = true;
    }

    #region Events
    public void ObjDestroyed(int points)
    {
        ObjsOnScreen--;
    }
    #endregion
}
