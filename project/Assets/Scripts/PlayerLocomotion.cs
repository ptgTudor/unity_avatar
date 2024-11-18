using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Movement Speeds")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Displayed Message")]
    public TMP_Text displayedText;
    private string collectedLetters;

    [Header("Media Variables")]
    public Button congratsButton;
    public Button failedButton;
    public GameObject scorePanel;
    public RawImage video_11;
    public GameObject video_12;
    public RawImage video_21;
    public GameObject video_22;
    public AudioSource collected;
    public AudioSource congrats;
    public AudioSource failed;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        displayedText.text = "You have collected: ";
        collectedLetters = "";

        // set the buttons as inactive at the start of the game
        congratsButton.gameObject.SetActive(false);
        failedButton.gameObject.SetActive(false);

        video_11.gameObject.SetActive(false);
        video_12.gameObject.SetActive(false);
        video_21.gameObject.SetActive(false);
        video_22.gameObject.SetActive(false);

        collected.gameObject.SetActive(false);
        congrats.gameObject.SetActive(false);
        failed.gameObject.SetActive(false);
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableLetter"))
        {
            scorePanel.SetActive(true);

            // deactivate collected letter
            other.gameObject.SetActive(false);

            // assign the displayedText and collectedLetters to the UI text elements
            collectedLetters = collectedLetters + other.gameObject.GetComponent<TextMeshPro>().text;
            displayedText.text = "You have collected: " + collectedLetters;
            collected.gameObject.SetActive(true);
            collected.Play();
            Debug.Log("You have collected the letter:" + collectedLetters);
        }

        Debug.Log("Letters: " + collectedLetters.Length);

        if (collectedLetters.Equals("ETTI"))
        {
            congratsButton.gameObject.SetActive(true);
            video_11.gameObject.SetActive(true);
            video_12.gameObject.SetActive(true);
            congratsButton.GetComponentInChildren<Text>().text = "Congratulations!";
            congrats.gameObject.SetActive(true);
            congrats.Play();
            // code below to stop the scene runtime
            // Time.timeScale = 0;
        }

        else
        {
            if (collectedLetters.Length >= 4)
            {
                failedButton.gameObject.SetActive(true);
                video_21.gameObject.SetActive(true);
                video_22.gameObject.SetActive(true);
                failedButton.GetComponentInChildren<Text>().text = "Try again!";
                failed.gameObject.SetActive(true);
                failed.Play();
                // code below to stop the scene runtime
                // Time.timeScale = 0;
            }
        }
    }
}
