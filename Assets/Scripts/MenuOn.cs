using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()//Turns Menu active when scene is loadedS
    {
        MenuUIHandler.Instance.Menu.SetActive(true);

    }
}
