using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGame : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorTextureOnEnemy;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
    private void OnMouseOver()
    {
        Debug.Log(gameObject.name);
        if(gameObject.CompareTag("Enemy"))
        {
            Cursor.SetCursor(cursorTextureOnEnemy, hotspot, cursorMode);
        }
    }

}
