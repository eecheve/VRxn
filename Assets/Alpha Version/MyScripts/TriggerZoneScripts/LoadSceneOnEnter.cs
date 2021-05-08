using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnEnter : MonoBehaviour
{
    [SerializeField] private int sceneNumber = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Should load scene " + sceneNumber.ToString());
            LoadScene(sceneNumber);
        }
    }

    private void LoadScene(int number)
    {
        SceneManager.LoadScene(number, LoadSceneMode.Single);
    }
}
