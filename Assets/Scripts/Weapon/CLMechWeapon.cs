using UnityEngine;
using System.Collections;

[AddComponentMenu("Nova/Mech/Weapon")]
public class CLMechWeapon : CLActor<CLMechWeapon>
{
    public CLMechWeaponType Type;
    public int PrefabID;
    public CLMechAmmoType AmmoType;
    public CLAttackRange AttackRange;
    public AudioClip FireClip;
    public Transform FirePoint;
    public GameObject FireEffectPrefab;
    public GameObject AmmoPrefab;
    public float ATK = 1;
    public float CoolTime;
    public float FireTime;
    public GameObject Target;
    public GameObject FireEffect;
    public GameObject User;

    void Awake()
    {
        mActor = this;
        AddAction<CLMechWeaponIdle>();
        AddAction<CLMechWeaponCool>();
        AddAction<CLMechWeaponFire>();
        Action<CLMechWeaponIdle>();
        if (FireEffectPrefab != null)
        {
            FireEffect = CLGameObject.Instantiate(FireEffectPrefab);
            CLTransform.Attach(FirePoint, FireEffect.transform);
            FireEffect.SetActive(false);
        }
    }

    void Start()
    {
       
    }

    public void OpenFire(GameObject target, GameObject user)
    {
        Target = target;
        User = user;
        Action<CLMechWeaponFire>();
    }

    public void StopFire()
    {
       Action<CLMechWeaponIdle>();
    }

    public void ShotAmmo()
    {
        //print(User.name + " ShotAmmo " + Target.name);
        if (AmmoType == CLMechAmmoType.Bullet)
        {
            ShotBullet();
        }
        else if (AmmoType == CLMechAmmoType.Missle)
        {
            ShotMissile();
        }
        else if (AmmoType == CLMechAmmoType.Laser)
        {
            ShotLaser();
        }
    }

    public void ShotBullet()
    {
        Target.GetComponent<CLHealth>().Damage(ATK);
    }

    public void ShotMissile()
    {
        if (AmmoPrefab != null)
        {
            GameObject ammo = CLGameObject.Instantiate(AmmoPrefab, FirePoint);
            ammo.GetComponent<CLMechMissile>().OnShot(this, Target);
        }
    }

    public void ShotLaser()
    {
        if (AmmoPrefab != null)
        {
            GameObject ammo = CLGameObject.Instantiate(AmmoPrefab, FirePoint);
            ammo.GetComponent<CLMechLaser>().OnShot(this, Target);
        }
    }

    public void ShowFireEffect()
    {
        if (FireEffect != null)
        {
            FireEffect.SetActive(true);
        }
    }

    public void HideFireEffect()
    {
        if (FireEffect != null)
        {
            FireEffect.SetActive(false);
        }
    }
}

public enum CLMechWeaponType
{
    LeftHand,
    RightHand,
    DoubleHand,
    Shoulder
}

public enum CLMechAmmoType
{
    Bullet,
    Laser,
    Missle
}