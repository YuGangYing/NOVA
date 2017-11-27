using UnityEngine;
using System.Collections;

public class CLMechLaser : MonoBehaviour
{
    public LineRenderer LaserBody;
    public GameObject HitEffectPrefab;
    private GameObject mTarget;
    private CLMechWeapon mWeapon;
    GameObject mHitEffect;

    void Update()
    {
        if (mTarget.GetComponent<CLHealth>().IsAlive())
        {
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
            LaserBody.SetPosition(1, position);
            mHitEffect.transform.position = position;
            mTarget.GetComponent<CLHealth>().Damage(mWeapon.ATK * Time.deltaTime);
        }
        else
        {
            Destroy(mHitEffect);
            Destroy(gameObject);
        }
        if (!mWeapon.User.GetComponent<CLHealth>().IsAlive())
        {
            Destroy(mHitEffect);
            Destroy(gameObject);
        }
        LaserBody.SetPosition(0, mWeapon.FirePoint.position);
    }

    public void OnShot(CLMechWeapon weapon, GameObject target)
    {
        mTarget = target;
        mWeapon = weapon;
        gameObject.AddComponent<CLAutoDestroy>().LifeTime = weapon.FireTime;
        mHitEffect = CLGameObject.Instantiate(HitEffectPrefab);
        mHitEffect.AddComponent<CLAutoDestroy>().LifeTime = weapon.FireTime;
    }
}