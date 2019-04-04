using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

abstract public class Unit : MonoBehaviour
{

  [SerializeField]
  protected GameObject objective;
  [SerializeField]
  protected GameObject selectionIcon;
  [SerializeField]
  protected UIManager uiManager;

  private static int unitCount = 0;

  public enum Alliance
  {
    PLAYER,
    COMPUTER
  }

  [SerializeField]
  protected Alliance alliance = Alliance.COMPUTER;
  public Alliance GetAlliance
  {
    get { return alliance; }
  }

  //fields
  [SerializeField]
  protected int health = 1;

  public int Health
  {
    get { return health; }
  }

  [SerializeField]
  protected float maxHealth = 1f;

  public float MaxHealth
  {
    get { return maxHealth; }
  }

  protected bool alive = true;
  public bool IsAlive
  {
    get { return alive; }
  }

  //[SerializeField]
  //protected float attackRange = 1f;
  //[SerializeField]
  //protected float attackSpeed = 1f;
  [SerializeField]
  protected float deathFadeTime = 1f;
  [SerializeField]
  protected float deathFadeDelay = 1f;

  //abstract methods
  public abstract void SelectUnit();
  public abstract void DeselectUnit();
  public abstract void Move(Vector3 destination);
  public abstract void Attack(Unit target);

  //the unit this unit is attacking
  protected Unit currentTarget;
  public Unit CurrentTarget
  {
    get { return currentTarget; }
  }

  protected int unitID;
  public int UnitID
  {
    get { return unitID; }
  }

  //Health UI
  protected HealthBar healthBar;
  protected Slider healthSlider;
  private RectTransform healthTransform;
  [SerializeField]
  private Vector3 healthOffset;

  protected virtual void Awake()
  {
    unitID = unitCount++;
    healthBar = uiManager.AddHealthBar(this);//UIManager.Instance.AddHealthBar(this);
    healthSlider = healthBar.GetComponent<Slider>();
    healthSlider.maxValue = maxHealth;
    healthSlider.value = health;
    healthTransform = healthSlider.GetComponent<RectTransform>();
    //objective = new GameObject();
    //commands = new Queue<Command>();
  }

  protected virtual void Update()
  {
    //Debug.Log(currentCommand);
    //if(currentCommand == null && commands.Count > 0)
    //{
    //  Command command = commands.Dequeue();
    //  switch(command.type)
    //  {
    //    case CommandType.ATTACK:
    //      currentCommand = StartCoroutine(Attack(command));
    //      break;
    //    case CommandType.MOVE:
    //      currentCommand = StartCoroutine(Move(command));
    //      break;
    //  }
    //}
  }

  protected virtual void LateUpdate()
  {
    healthTransform.position = Camera.main.WorldToScreenPoint(transform.position + healthOffset);
  }

  //public void IssueCommand(Command command)
  //{
  //  commands.Enqueue(command);
  //}

  public virtual void TakeDamage(int damage)
  {
    health -= damage;
    if (health <= 0)
    {
      Die();
    }
    healthBar.UpdateHealth(health);
  }

  protected virtual IEnumerator FadeThenDestroy()
  {
    float deathTime = Time.time;
    yield return new WaitForSeconds(deathFadeDelay);
    while(Time.time < deathFadeTime + deathTime)
    {
      transform.Translate(Vector3.down * Time.deltaTime);
      yield return new WaitForEndOfFrame();
    }
    Destroy(gameObject);
    Destroy(healthBar.gameObject);
  }

  protected virtual void Die()
  {
    alive = false;
    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    healthBar.gameObject.SetActive(false);
    selectionIcon.SetActive(false);
    StartCoroutine(FadeThenDestroy());
  }

  private void OnValidate()
  {
    if (maxHealth < health) maxHealth = health;
  }

}

