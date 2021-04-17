using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    //params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    //Cached reference
    Level level;
    //state variables
    [SerializeField] int timesHit; //For Debug purposes
    private void Start()
    {
        CountBreakableBlocks();
    }
    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
            FindObjectOfType<GameSession>().AddToScore();
        }
        else
            ShowNextHitSprites();
    }

    private void ShowNextHitSprites()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        Debug.LogError("Srite is missing from array" + gameObject.name);
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX , transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
