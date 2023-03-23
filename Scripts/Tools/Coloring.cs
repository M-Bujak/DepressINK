using UnityEngine;

public class Coloring : MonoBehaviour
{
    [SerializeField]
    private Renderer _brushRenderer;
    [SerializeField]
    private AudioClip[] _splatAudios;
    [SerializeField]
    private AudioSource _splatSource;
    [SerializeField]
    private ParticleSystem _splatParticles;
    
    private ParticleSystemRenderer _splatRenderer;

    private void Start()
    {
        _splatRenderer = _splatParticles.GetComponent<ParticleSystemRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Colorable")
        {
            Colorable colorable = other.GetComponent<Colorable>();
            colorable.ChangeColor(_brushRenderer.material.color, true);
            SplashPaint();
        }

        if (other.tag == "MultipleColorable")
        {
            MultipleObjectsToColor otherScript =
                other.gameObject.GetComponentInParent<MultipleObjectsToColor>();

            if (otherScript != null)
            {
                otherScript.ChangeColors(_brushRenderer.material.color);
                SplashPaint();
            }
        }
    }

    private void SplashPaint()
    {
        _splatRenderer.material.color = _brushRenderer.material.color;
        _splatParticles.Emit(5);
        PlayRandomSplat();
    }

    private void PlayRandomSplat()
    {
        int index = Random.Range(0, _splatAudios.Length);
        _splatSource.PlayOneShot(_splatAudios[index]);
    }

    public void ChangeBrushColor(Color color)
    {
        _brushRenderer.material.color = color;
    }
}

