using UnityEngine;
using System.Collections;

public class CLMechBullet : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed;
    private CLMoveRotateTo mMoveRotateTo;
    private Vector3 mDestitaion;

    void Awake()
    {
        mMoveRotateTo = CLGameObject.AddComponent<CLMoveRotateTo>(gameObject);
    }

    void Update()
    {
        mMoveRotateTo.Move.Speed = MoveSpeed;
        mMoveRotateTo.Rotate.Speed = RotateSpeed;
        mMoveRotateTo.MoveRotateToTarget(mDestitaion);
        if (mMoveRotateTo.ReachTarget())
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnShot(GameObject target)
    {
        mDestitaion = target.transform.position;
    }
}