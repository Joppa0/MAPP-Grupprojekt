using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // Grundl�ggande kraft i y-led
    [SerializeField] private float jumpPrecision = 2f; // Justering av kraft f�r att "landa" p� r�tt punkt

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // H�gerklick
        {
            JumpToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void JumpToPoint(Vector2 targetPoint)
    {
        // Nollst�ller nuvarande hastigheter f�r att s�kerst�lla ett renare "hopp"
        rb.velocity = Vector2.zero;

        // Ber�knar skillnaden i position mellan nuvarande position och m�lpositionen
        Vector2 jumpVector = targetPoint - (Vector2)transform.position;

        // Justerar hoppriktningen och kraften baserat p� avst�nd och inst�llda reglage
        float jumpX = jumpVector.x * jumpPrecision;
        float jumpY = jumpForce + Mathf.Abs(jumpVector.y) * jumpPrecision; // Anv�nder absolutv�rde f�r y f�r att s�kerst�lla positiv kraft

        // Anv�nder kraften f�r att "hoppa" mot m�let
        rb.AddForce(new Vector2(jumpX, jumpY), ForceMode2D.Impulse);
    }
}
