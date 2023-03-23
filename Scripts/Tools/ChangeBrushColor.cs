using UnityEngine;

public class ChangeBrushColor : MonoBehaviour
{
    [SerializeField]
    private Coloring _coloringScript;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _coloringScript = FindObjectOfType<Coloring>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Marker")
        {
            //Renderer _brushRenderer = other.gameObject.GetComponent<Renderer>();
            //_brushRenderer.material.color = _renderer.material.color;
            //_coloringScript._brushRenderer = _renderer;

            _coloringScript.ChangeBrushColor(_renderer.material.color);
        }
    }
}