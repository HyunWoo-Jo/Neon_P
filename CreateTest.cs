using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour
{
    public GameObject obj;
    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(obj);
            yield return new WaitForSeconds(1f);
        }
    }
}
