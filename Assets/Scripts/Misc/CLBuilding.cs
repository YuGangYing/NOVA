using UnityEngine;
using System.Collections;

public class CLBuilding : CLActor<CLBuilding>
{
    public GameObject StandModel;
    public GameObject RuinsModel;
    public int PlayerID;
    public GameObject ExplosionPrefab;

    void Start()
    {
        RuinsModel.SetActive(false);
    }

    void OnHealthDie()
    {
        StandModel.SetActive(false);
        RuinsModel.SetActive(true);
        CLGameObject.Instantiate(ExplosionPrefab, transform);
        CLGame.Instance.Action<CLGameAccount>();
    }
}