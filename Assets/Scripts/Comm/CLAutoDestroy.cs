using UnityEngine;
using System.Collections;

public class CLAutoDestroy : MonoBehaviour
{
    public float LifeTime = 5f;
    private float mCurLifeTime;

    // Use this for initialization
    void Start()
    {
        mCurLifeTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        mCurLifeTime += Time.deltaTime;
        if (mCurLifeTime > LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
