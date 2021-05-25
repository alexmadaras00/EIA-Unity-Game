using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHelp : MonoBehaviour
{
    public GameObject helpMenu;
    public GameObject helpText;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            if (helpMenu.activeSelf)
            {
                helpMenu.SetActive(false);
                helpText.SetActive(false);
            }

            else
            {
                helpMenu.SetActive(true);
                helpText.SetActive(true);
            }
        }
    }
}
