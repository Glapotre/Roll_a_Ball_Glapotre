using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue = 0;
    private float movementX;
    private float movementY;



    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScenarioData _scenario;
    [SerializeField] private GameObject _wallPrefab;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical"));
        }
    }


    void onMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Destroy(other.gameObject);
            UpdateScore();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            UpdateScore();
        }
    }


    private void UpdateScore()
    {


        ScoreValue++;
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        _scoreText.text = PlayerPrefs.GetString("Score");




        int IndexPrefab = ScoreValue;
        if (ScoreValue > 3)
        {
            IndexPrefab = 0;
        }

        Instantiate(_wallPrefab, _scenario.FirstWalls[IndexPrefab], Quaternion.identity);




        if (ScoreValue >= 8)
        {


            Debug.Log("Active Scene name is: " + SceneManager.GetActiveScene().name + "\nActive Scene index: " + SceneManager.GetActiveScene().buildIndex);
            int Index = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = Index + 1;
            int maxSceneIndex = SceneManager.sceneCount;


            if (Index == maxSceneIndex)
                Index = 0;
            else
                Index = nextSceneIndex;


            PlayerPrefs.SetInt("ScoreValue", ScoreValue);

            SceneManager.LoadScene(Index, LoadSceneMode.Single);

          



            Debug.Log("next Scene name is: " + nextSceneIndex);
            Debug.Log("number of Scene name is: " + SceneManager.sceneCount);

        }



    }

}