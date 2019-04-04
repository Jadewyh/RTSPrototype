using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light), typeof(ParticleSystem))]
public class MuzzleFlashEffect : MonoBehaviour
{

  //new keyword is used to eliminate warning from deprecated gameObject.light
  private new Light light;

  [SerializeField]
  private Gun gun;

  [SerializeField]
  private ParticleSystem bulletShells;
  [SerializeField]
  private ParticleSystem tracerRounds;
  private ParticleSystem muzzleFlash;

  private void Awake()
  {
    light = GetComponent<Light>();
    light.intensity = 0;
    muzzleFlash = GetComponent<ParticleSystem>();
    muzzleFlash.Stop();
    var main = muzzleFlash.main;
    main.playOnAwake = false;
    main.loop = false;
  }

  public void SetParticleDuration(float duration)
  {
    ParticleSystem.MainModule main = muzzleFlash.main;
    main.duration = duration;
    main = bulletShells.main;
    main.duration = duration;
    main = tracerRounds.main;
    main.duration = duration;
  }

  public void Flash(float fadeOutTime, float intensity = 2f, float size = 1)
  {
    light.intensity = intensity;
    muzzleFlash.Stop();
    muzzleFlash.transform.localScale = Vector3.one * size;
    muzzleFlash.Play();
    StartCoroutine(FadeOut(fadeOutTime));
  }

  private IEnumerator FadeOut(float fadeOutTime)
  {
    yield return new WaitForSeconds(fadeOutTime);
    light.intensity = 0;
  }

}
