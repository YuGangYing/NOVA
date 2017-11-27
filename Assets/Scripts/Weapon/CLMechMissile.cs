using UnityEngine;
using System.Collections;

public class CLMechMissile : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed;
    public GameObject SmokeTailorEffect;
    public GameObject HitEffectPrefab;
    private CLMoveTo mMove;
    private CLRotateTo mRotate;
    private GameObject mTarget;
    private CLMechWeapon mWeapon;

    void Awake()
    {
        mMove = CLGameObject.AddComponent<CLMoveTo>(gameObject);
        mRotate = CLGameObject.AddComponent<CLRotateTo>(gameObject);
    }

    void Update()
    {
        if (mTarget.GetComponent<CLHealth>().IsAlive())
        {
            mMove.Speed = MoveSpeed;
            mRotate.Speed = RotateSpeed;
            Vector3 position;
            CLMech mech = mTarget.GetComponent<CLMech>();
            if (mech != null)
            {
                position = mech.Equip.Leg.BodyLink.transform.position;
            }
            else
            {
                position = mTarget.transform.position;
            }
            mMove.MoveToTarget(position);
            mRotate.RotateTo(position - transform.position);
            if (mMove.ReachTarget())
            {
                mTarget.GetComponent<CLHealth>().Damage(mWeapon.ATK);
                Destroy();
            }
        }
        else
        {
            Destroy();
        }
    }

    public void OnShot(CLMechWeapon weapon, GameObject target)
    {
        mTarget = target;
        mWeapon = weapon;
    }

    void Destroy()
    {
        SmokeTailorEffect.transform.parent = null;
        SmokeTailorEffect.gameObject.AddComponent<CLAutoDestroy>().LifeTime = 2f;
        SmokeTailorEffect.GetComponent<ParticleSystem>().emissionRate = 0;
        CLGameObject.Instantiate(HitEffectPrefab, transform);
        Destroy(gameObject);
    }
}