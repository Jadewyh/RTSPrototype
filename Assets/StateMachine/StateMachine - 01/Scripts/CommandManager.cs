using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SelectionRect))]
public class CommandManager : MonoBehaviour
{

  private List<Unit> allUnits;
  private List<Unit> alliedUnits;
  private List<Unit> enemyUnits;
  private List<Unit> selectedUnits;

  private SelectionRect selectionRect;

  private AudioSource audioSource;

  [SerializeField]
  private float voiceOverVolume = 1f;
  [SerializeField]
  private List<AudioClip> attackVoiceOvers;
  [SerializeField]
  private List<AudioClip> moveVoiceOvers;

  void Awake()
  {
    selectionRect = GetComponent<SelectionRect>();

    selectedUnits = new List<Unit>();

    allUnits = new List<Unit>();
    allUnits.AddRange(FindObjectsOfType<Unit>());

    alliedUnits = new List<Unit>();
    alliedUnits.AddRange(FindObjectsOfType<Unit>()
      .Where(unit => unit.GetAlliance == Unit.Alliance.PLAYER)
      .ToArray()
    );

    enemyUnits = new List<Unit>();
    enemyUnits.AddRange(FindObjectsOfType<Unit>()
      .Where(unit => unit.GetAlliance == Unit.Alliance.COMPUTER)
      .ToArray()
    );

    audioSource = GetComponent<AudioSource>();

  }

  void Update()
  {

    //===================================================================
    // Selection
    //===================================================================
    //Release the mouse button
    if (Input.GetMouseButtonUp(0))
    {
      if (!Input.GetKey(KeyCode.LeftShift))
      {
        ClearSelectedUnits();
      }

      //clicked once
      //if (Time.time - clickTime <= delay)
      //{
      //  RaycastHit hit;
      //  if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
      //  {
      //    Unit unit = hit.collider.gameObject.GetComponent<Unit>();
      //    if (unit != null)
      //    {
      //      AddUnit(unit);
      //    }
      //  }
      //}

      ////click was held
      //else
      //{
      for (int i = 0; i < alliedUnits.Count; i++)
      {
        Unit currentUnit = alliedUnits[i];
        if (selectionRect.Contains(currentUnit))
        {
          AddUnit(currentUnit);
        }
      }
      //}
    }

    //===================================================================
    // Orders
    //===================================================================
    //right click
    if (Input.GetMouseButtonDown(1))
    {
      if (selectedUnits.Count != 0 && selectedUnits[0].GetAlliance == Unit.Alliance.PLAYER)
      {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
        {
          Unit currentUnit = hit.collider.GetComponent<Unit>();
          if (currentUnit != null)
          {
            if (currentUnit.GetAlliance == Unit.Alliance.COMPUTER)
            {
              IssueAttackOrder(currentUnit);

            }
            else
            {
              IssueMoveOrder(currentUnit.transform.position);
            }
          }
          else
          {
            IssueMoveOrder(hit.point);
          }
        }
      }
    }
  }

  private void IssueMoveOrder(Vector3 destination)
  {
    if (Random.Range(0, 4) <= 1)
    {
      AudioClip voiceOver = moveVoiceOvers[Random.Range(0, moveVoiceOvers.Count)];
      audioSource.PlayOneShot(voiceOver, voiceOverVolume);
    }
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      //selectedUnits[i].IssueCommand(new Command(CommandType.MOVE, destination));
      selectedUnits[i].Move(destination);
    }
  }

  private void IssueAttackOrder(Unit target)
  {
    if (Random.Range(0, 4) <= 1)
    {
      AudioClip voiceOver = attackVoiceOvers[Random.Range(0, attackVoiceOvers.Count)];
      audioSource.PlayOneShot(voiceOver, voiceOverVolume);
    }
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      //selectedUnits[i].IssueCommand(new Command(CommandType.ATTACK, target));
      selectedUnits[i].Attack(target);
    }
  }
  
  /// <summary>
  /// 
  /// </summary>
  /// <param name="unit"></param>
  private void AddUnit(Unit unit)
  {
    selectedUnits.Add(unit);
    unit.SelectUnit();
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearSelectedUnits()
  {
    for (int i = 0; i < selectedUnits.Count; ++i)
    {
      selectedUnits[i].DeselectUnit();
    }
    selectedUnits.Clear();
  }

}
