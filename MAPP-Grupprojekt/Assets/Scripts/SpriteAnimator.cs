using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 0.1f;
    private Image image;
    private int currentFrame;
    private float timer;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame];
        }
    }
}
