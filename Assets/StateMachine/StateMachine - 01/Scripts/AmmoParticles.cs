using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoParticles : MonoBehaviour
{
  [SerializeField]
  private GameObject shellPrefab;
  [SerializeField]
  private int maxShellNum;
  [SerializeField]
  private float timeTilReturn;

  public Transform spawnPoint;

  private List<GameObject> shells;

  // Start is called before the first frame update
  void Awake()
  {
    shells = new List<GameObject>();
    for(int i = 0; i < maxShellNum; ++i)
    {
      var shell = Instantiate(shellPrefab);
      shell.SetActive(false);
      shells.Add(shell);
    }
  }

  public void EjectShell()
  {
    GameObject ejected = null;
    for(int i =0; i < maxShellNum; ++i)
    {
      if(shells[i].activeInHierarchy == false)
      {
        ejected = shells[i];
        break;
      }
    }
    if (ejected == null) ejected = shells[0];

    ejected.SetActive(true);
    ejected.transform.position = spawnPoint.position;
    ejected.GetComponent<Rigidbody>().velocity = Vector3.zero;
    StartCoroutine(returnShell(ejected));
  }

  private IEnumerator returnShell(GameObject shell)
  {
    yield return new WaitForSeconds(timeTilReturn);
    shell.SetActive(false);
  }


}
