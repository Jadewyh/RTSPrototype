using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer), typeof(SpriteRenderer))]
public class Objective : MonoBehaviour
{

  [SerializeField]
  private Vector3 offset;
  [SerializeField]
  private Vector3 rotation;

  private LineRenderer line;
  private SpriteRenderer sprite;

  private void Awake()
  {
    line = GetComponent<LineRenderer>();
    sprite = GetComponent<SpriteRenderer>();
  }

  private void LateUpdate()
  {
    transform.position = transform.parent.GetComponent<NavMeshAgent>().destination + offset;
    transform.rotation = Quaternion.Euler(rotation);

    line.SetPosition(0, transform.parent.position);
    line.SetPosition(1, transform.position);
  }

}
