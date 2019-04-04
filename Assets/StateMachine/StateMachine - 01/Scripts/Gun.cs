using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit), typeof(AudioSource))]
public class Gun : MonoBehaviour
{

  //VFX
  [SerializeField]
  private MuzzleFlashEffect muzzleFlash;
  [SerializeField]
  private float muzzleFlashSize = 1f;
  [SerializeField]
  private float muzzleFlashIntensity = 1f;

  //Audio
  private AudioSource audioSource;
  [SerializeField]
  private AudioClip gunShotClip;
  [SerializeField]
  [Range(0, 1)]
  private float gunShotVolume = 1f;

  //Gameplay
  private Unit parentUnit;
  [SerializeField]
  private Transform fireTransform;
  [SerializeField]
  private int damage = 1;
  public float attackSpeed = 1.0f;
  public float attackRange = 15f;

  private void Awake()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.volume = gunShotVolume;
    parentUnit = GetComponent<Unit>();
  }

  private void Start()
  {
    muzzleFlash.SetParticleDuration(attackSpeed);
  }

  public void Fire(float flashDuration)
  {
    if (parentUnit.CurrentTarget == null) return;

    muzzleFlash.transform.position = fireTransform.position;
    muzzleFlash.transform.rotation = fireTransform.rotation;
    muzzleFlash.Flash(flashDuration, muzzleFlashIntensity, muzzleFlashSize);

    audioSource.Stop();
    audioSource.PlayOneShot(gunShotClip, gunShotVolume);

    parentUnit.CurrentTarget.TakeDamage(damage);

  }

}
