using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls pc;
    public float speed = 1;
    public Vector2 movementInput;
    public int currentSpeedId = 0;

    public int currentPlayerId = 1;
    public GameObject playerOne;
    public GameObject playerTwo;

    // Start is called before the first frame update
    void Awake()
    {
        pc = new PlayerControls();

        pc.Player.Move.started += ctx => Move(ctx.ReadValue<Vector2>());
        pc.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        pc.Player.Move.canceled += ctx => Move(ctx.ReadValue<Vector2>());

        pc.Player.ChangeSpeed.performed += ctx => ChangeSpeed();

        pc.Player.ChangeColor.performed += ctx => ChangeColor();

        pc.Player.SwitchChar1.performed += ctx => ChangePlayer(1);
        pc.Player.SwitchChar2.performed += ctx => ChangePlayer(2);
    }

    private void Start()
    {
        playerTwo.SetActive(false);
    }

    private void OnEnable()
    {
        pc.Enable();
    }

    private void OnDisnable()
    {
        pc.Disable();
    }

    public void Move(Vector2 input)
    {
        movementInput = input;
    }

    public void ChangeColor()
    {
        if(currentPlayerId == 1)
        {
            foreach (SpriteRenderer i in playerOne.GetComponentsInChildren<SpriteRenderer>())
            {
                i.color = Random.ColorHSV();
            }
        }
        else
        {
            foreach (SpriteRenderer i in playerTwo.GetComponentsInChildren<SpriteRenderer>())
            {
                i.color = Random.ColorHSV();
            }
        }
    }

    public void ChangePlayer(int id)
    {
        currentPlayerId = id;

        if (currentPlayerId == 1)
        {
            playerTwo.SetActive(false);
            playerOne.SetActive(true);
        }
        else
        {
            playerTwo.SetActive(true);
            playerOne.SetActive(false);
        }
    }

    public void ChangeSpeed()
    {
        currentSpeedId += 1;
    }

    private void Update()
    {
        transform.Translate((Vector2.one * movementInput) * (speed * Mathf.Pow(2,(currentSpeedId % 3) + 1)) * Time.deltaTime);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}
