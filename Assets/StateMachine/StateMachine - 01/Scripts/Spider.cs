using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Spider : Unit
{

  private NavMeshAgent agent;
  private Animator animator;

  [SerializeField]
  private GameObject deathEffect;

  protected override void Awake()
  {
    base.Awake();
    selectionIcon.SetActive(false);
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
    deathEffect.SetActive(false);
  }

  override public void SelectUnit()
  {
    selectionIcon.SetActive(true);
  }

  override public void DeselectUnit()
  {
    selectionIcon.SetActive(false);
  }

  public override void Move(Vector3 destination)
  {
    throw new System.NotImplementedException();
  }

  public override void Attack(Unit target)
  {
    throw new System.NotImplementedException();
  }

  protected override void Die()
  {
    deathEffect.SetActive(true);
    animator.SetBool("dead", true);
    base.Die();
  }



}
