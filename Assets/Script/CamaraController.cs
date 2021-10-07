using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamaraController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Lv1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Lv2()
    {
        SceneManager.LoadScene("SampleScene2");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
