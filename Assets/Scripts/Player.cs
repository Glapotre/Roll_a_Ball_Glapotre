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
    // This will be appended to the name of the created entities and increment when each is created.

    public float swingAngle = 30f;

    private int HitValue = 0;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScenarioData _scenario;
    [SerializeField] private GameObject _wallPrefab;
    //[SerializeField] private float _lazerSpeed = 10f;

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
            Walls();

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            UpdateScore();
            Walls();
        }
    }

    private void Walls()
    {




        HitValue++;
        Debug.Log("Number hits is: " + HitValue);
    

        int IndexPrefab = HitValue;
        if (HitValue > 7)
        {
            IndexPrefab = 0;
        }





        GameObject currentEntity = Instantiate(_wallPrefab, _scenario.FirstWalls[IndexPrefab], Quaternion.identity);

        transform.position += Vector3.right * Time.deltaTime;

        Debug.Log("Prefab number is: " + _scenario.FirstWalls[IndexPrefab]);

    }







    private void UpdateScore()
    {


        ScoreValue++;
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        _scoreText.text = PlayerPrefs.GetString("Score");

      





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