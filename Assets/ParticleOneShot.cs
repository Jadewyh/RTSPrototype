
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleOneShot : MonoBehaviour
{
  private ParticleSystem ps;

  [SerializeField]
  private List<AudioClip> playSounds;
  [SerializeField]
  private float volume = 1f;

  protected void Awake()
  {
    ps = GetComponent<ParticleSystem>();
  }

  protected void Start()
  {
    Destroy(gameObject, ps.main.duration);
    AudioSource audio = GetComponent<AudioSource>();
    if(audio != null && playSounds != null)
    {
      for (int i = 0; i < playSounds.Count; ++i)
      {
        audio.PlayOneShot(playSounds[i], volume);
      }
    }
  }

}
