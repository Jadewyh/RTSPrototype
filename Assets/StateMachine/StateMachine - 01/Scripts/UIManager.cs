using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UIManager : ScriptableObject
{
  [SerializeField]
  private GameObject healthBarPrefab;

  public HealthBar AddHealthBar(Unit unit)
  {
    var hb = Instantiate(healthBarPrefab).GetComponent<HealthBar>();
    hb.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
    return hb;
  }

}
