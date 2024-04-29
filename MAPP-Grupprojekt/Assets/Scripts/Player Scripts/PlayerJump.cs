using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // Grundläggande kraft i y-led
    [SerializeField] private float jumpPrecision = 2f; // Justering av kraft för att "landa" på rätt punkt

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Högerklick
        {
            JumpToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void JumpToPoint(Vector2 targetPoint)
    {
        // Nollställer nuvarande hastigheter för att säkerställa ett renare "hopp"
        rb.velocity = Vector2.zero;

        // Beräknar skillnaden i position mellan nuvarande position och målpositionen
        Vector2 jumpVector = targetPoint - (Vector2)transform.position;

        // Justerar hoppriktningen och kraften baserat på avstånd och inställda reglage
        float jumpX = jumpVector.x * jumpPrecision;
        float jumpY = jumpForce + Mathf.Abs(jumpVector.y) * jumpPrecision; // Använder absolutvärde för y för att säkerställa positiv kraft

        // Använder kraften för att "hoppa" mot målet
        rb.AddForce(new Vector2(jumpX, jumpY), ForceMode2D.Impulse);
    }
}
