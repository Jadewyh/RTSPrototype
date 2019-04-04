using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{

  private Animator animator;
  private NavMeshAgent agent;

  void Awake()
  {
    animator = GetComponent<Animator>();
    agent = GetComponent<NavMeshAgent>();
  }

}
