using UnityEngine;
using System.Collections;

[AddComponentMenu("Nova/Mech/Equip")]
public class CLMechEquip : MonoBehaviour
{
    public CLMechLeg Leg;
    public CLMechBody Body;
    public CLMechWeapon WeaponL;
    public CLMechWeapon WeaponR;

    public void Assemble()   
    {
        // assenmble
        Leg.transform.parent = transform;
        Body.transform.parent = Leg.BodyLink;
        WeaponL.transform.parent = Body.WeaponLinkL;
        WeaponR.transform.parent = Body.WeaponLinkR;
        // reset
        Leg.transform.localPosition = Vector3.zero;
        Body.transform.localPosition = Vector3.zero;
        WeaponL.transform.localPosition = Vector3.zero;
        WeaponR.transform.localPosition = Vector3.zero;
    }

    public void Disassemble()
    {
        Leg.transform.parent = null;
        Body.transform.parent = null;
        WeaponL.transform.parent = null;
        WeaponR.transform.parent = null;
    }

    public void SetLeg(GameObject newEquipmentPrefab)
    {
        SetEquipment<CLMechLeg>(ref Leg, CLGameObject.Instantiate(newEquipmentPrefab));
    }

    public void SetBody(GameObject newEquipmentPrefab)
    {
        SetEquipment<CLMechBody>(ref Body, CLGameObject.Instantiate(newEquipmentPrefab));
    }

    public void SetWeaponL(GameObject newEquipmentPrefab)
    {
        SetEquipment<CLMechWeapon>(ref WeaponL, CLGameObject.Instantiate(newEquipmentPrefab));
    }

    public void SetWeaponR(GameObject newEquipmentPrefab)
    {
        SetEquipment<CLMechWeapon>(ref WeaponR, CLGameObject.Instantiate(newEquipmentPrefab));
    }

    public void SetEquipment<T>(ref T oldEquipment, GameObject newEquipment) where T : MonoBehaviour
    {
        if (oldEquipment != newEquipment)
        {
            Disassemble();
            Destroy(oldEquipment.gameObject);
            oldEquipment = newEquipment.GetComponent<T>();
            Assemble();
        }
    }
}