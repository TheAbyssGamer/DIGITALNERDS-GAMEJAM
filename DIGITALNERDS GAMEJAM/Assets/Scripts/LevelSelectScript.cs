﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public void LoadLevel(int index){
        SceneManager.LoadScene(index);
    }
}
