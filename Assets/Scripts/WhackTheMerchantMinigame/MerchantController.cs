using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite m_merchantSprite;

    [Header("GameManager")]
    [SerializeField] private MerchantGameManager m_gameManager;

    [SerializeField] private Vector2 m_startPosition = new Vector2(0f, -2.56f); //-2.56 because the mole/merchant sprite is 256x256
    [SerializeField] private Vector2 m_endPosition = Vector2.zero;
    [SerializeField] private float m_showDuration = 0.5f; //how long it takes to show the mole/merchant
    [SerializeField] private float m_duration = 1f;       //how long the mole/merchant stays visible for


    private SpriteRenderer m_spriteRenderer;
    private BoxCollider2D m_boxCollider2D;
    private Vector2 m_boxOffset;
    private Vector2 m_boxSize;
    private Vector2 m_boxOffsetHidden;
    private Vector2 m_boxSizeHidden;

    // Mole Parameters 
    private bool m_hittable = true;
    public enum MerchantType { Standard }; //TODO: Add more types if we ever need them
    private MerchantType m_merchantType;
    private int m_lives;
    private int m_merchantIndex = 0;

	private void Awake()
	{
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();

        m_boxOffset = m_boxCollider2D.offset;
        m_boxSize = m_boxCollider2D.size;
        m_boxOffsetHidden = new Vector2(m_boxOffset.x, -m_startPosition.y / 2f);
        m_boxSizeHidden = new Vector2(m_boxSize.x, 0f);
	}

	public void Activate(int level)
	{
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(m_startPosition, m_endPosition));
	}
    
    public void SetIndex(int index)
	{
        m_merchantIndex = index;
	}

    private void CreateNext()
	{
        m_merchantType = MerchantType.Standard;
        m_spriteRenderer.sprite = m_merchantSprite;
        m_lives = 1;
        m_hittable = true;
	}

	private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        // Make sure we start at the start.
        transform.localPosition = start;

        // Show the mole/merchant.
        float elapsed = 0f;
        while (elapsed < m_showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / m_showDuration);
            m_boxCollider2D.offset = Vector2.Lerp(m_boxOffsetHidden, m_boxOffset, elapsed / m_showDuration);
            m_boxCollider2D.size = Vector2.Lerp(m_boxSizeHidden, m_boxSize, elapsed / m_showDuration);
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure we're exactly at the end.
        transform.localPosition = end;
        m_boxCollider2D.offset = m_boxOffset;
        m_boxCollider2D.size = m_boxSize;

        // Wait for duration to pass.
        yield return new WaitForSeconds(m_duration);

        // Hide the mole/merchant.
        elapsed = 0f;
        while (elapsed < m_showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / m_showDuration);
            m_boxCollider2D.offset = Vector2.Lerp(m_boxOffset, m_boxOffsetHidden, elapsed / m_showDuration);
            m_boxCollider2D.size = Vector2.Lerp(m_boxSize, m_boxSizeHidden, elapsed / m_showDuration);
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Make sure we're exactly back at the start position.
        transform.localPosition = start;
        m_boxCollider2D.offset = m_boxOffsetHidden;
        m_boxCollider2D.size = m_boxSizeHidden;

        // If we got to the end and it's still hittable then we missed it.
        if (m_hittable)
        {
            m_hittable = false;
            m_gameManager.Missed(m_merchantIndex);
        }
    }

    private void OnMouseDown()
	{
        if (m_hittable)
		{

            switch (m_merchantType)
            {
                case MerchantType.Standard:
                    m_gameManager.AddScore(m_merchantIndex);
                    //Stop the animation
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    //We don't want to be able to keep tapping on this
                    m_hittable = false;
                    break;
                default:
                    break;
            }
           
        }
	}

    private IEnumerator QuickHide()
	{
        yield return new WaitForSeconds(0.25f);
        // Whilst we were waiting we may have spawned again here, so just
        // check that hasn't happened before hiding it. This will stop it
        // flickering in that case.
        if (!m_hittable)
		{
            Hide();
		}
	}

    public void Hide()
	{
        transform.localPosition = m_startPosition;
        m_boxCollider2D.offset = m_boxOffsetHidden;
        m_boxCollider2D.size = m_boxSizeHidden;
	}

    private void SetLevel(int level)
	{
        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 1f);
        m_duration = Random.Range(durationMin, durationMax);
    }

    public void StopGame()
	{
        m_hittable = false;
        StopAllCoroutines();
	}
}
