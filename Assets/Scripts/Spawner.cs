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
    List<GameObject> spawned = new List<GameObject>();
    #endregion

    #region Properties
    public int ObjsOnScreen
    {
        get
        {
            return spawned.Count;
        }
    }
    #endregion

    #region Initialization
    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        SpawnObjs();
	}

    void OnDisable()
    {
        DestroyObjs();
    }
    #endregion
	
	void SpawnObjs()
    {
        GravityController.NoGravity();
        for (int i = 0; i < SPAWN_AMOUNT; i++)
        {
            var ring = Instantiate(RingPrefab, new Vector3(Random.Range(-5.0f, 5.0f), transform.position.y, transform.position.z), Quaternion.identity);
            ring.transform.parent = transform;
            spawned.Add(ring);
        }
        TouchForce.Instance.MayTouch = true;
    }

    void DestroyObjs()
    {
        foreach(var ring in spawned)
        {
            if(ring != null)
            {
                Destroy(ring);
            }
        }
        spawned.Clear();
    }

    #region Events
    public void ObjDestroyed(int points, GameObject gameObject)
    {
        GameManager.Instance.CurrentScore += points;
        spawned.Remove(gameObject);
        if(spawned.Count == 0)
        {
            SpawnObjs();
        }
    }
    #endregion
}
