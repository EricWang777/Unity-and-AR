using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public Material hightlightMaterial;
    private Material defaultMaterial;


    public GameObject panel;
    private bool isSelected;

    private Transform _selection;
    
    

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        panel.SetActive(isSelected);
    }

    // Update is called once per frame
    void Update()
    {
        panel.SetActive(isSelected);

        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        if (Input.touchCount > 0)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag("Selectable"))
                {
                    isSelected = true;
                    
                    var selectionRenderer = selection.GetComponent<Renderer>();

                    if (selectionRenderer != null)
                    {
                        defaultMaterial = selectionRenderer.material;
                        selectionRenderer.material = hightlightMaterial;
                    }

                    _selection = selection;
                }
                else if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    isSelected = true;
                }
                else
                {

                    isSelected = false;

                }

            }
        }
        
    }
}
