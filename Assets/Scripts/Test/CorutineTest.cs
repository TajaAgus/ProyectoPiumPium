using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutineTest : MonoBehaviour
{
    [SerializeField] private Renderer myModel;

    private void Start()
    {
        myModel.material.color = Color.red;
    }
    private void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            ChangeColor();
        }
        if (Input.GetKeyDown("u"))
        {
            StartCoroutine(Fade());
        }
    }
    private void ChangeColor()
    {
        Color c = myModel.material.color;

        if (myModel.material.color == Color.red)
        {
            myModel.material.color = Color.green;
        }
        else if (myModel.material.color == Color.green)
        {
            myModel.material.color = Color.red;
        }
    }
    IEnumerator Fade()
    {
        Color c = myModel.material.color;
        if (c.a == 1f)
        {
            for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
            {
                c.a = alpha;
                myModel.material.color = c;
                yield return new WaitForSeconds(.1f); ;
            }
        }
        else
        {
            for (float alpha = c.a; alpha <= 1; alpha += 0.1f)
            {
                c.a = alpha;
                myModel.material.color = c;
                yield return new WaitForSeconds(.1f); ;
            }
        }
    }
}
