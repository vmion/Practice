using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Char_Controller
{    
    void Start()
    {
        
    }    
    void Update()
    {             
        if(Input.GetKeyDown(KeyCode.Z))
        {
            board.SetActive(false);
        } 
    }
}
