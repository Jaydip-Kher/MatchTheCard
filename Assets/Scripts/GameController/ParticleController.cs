using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        StopParticle();
    }
    private void OnEnable()
    {
        ActionController.Instance.onLevelComplete += StartParticle;
        ActionController.Instance.onLevelReset += StopParticle;
    }
    private void OnDisable()
    {
        if (ActionController.Instance == null) return;

        ActionController.Instance.onLevelComplete -= StartParticle;
        ActionController.Instance.onLevelReset -= StopParticle;
    }
    public void StartParticle()
    {
        particle.Play();
    }
    public void StopParticle()
    {
        particle.Stop();
    }
}