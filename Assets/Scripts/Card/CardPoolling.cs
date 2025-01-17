using System.Collections.Generic;
using UnityEngine;

public class CardPoolling : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform poolHolder;
    [SerializeField] private int initialPoolSize = 20;

    private readonly Queue<Card> pool = new Queue<Card>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Card newCard = Instantiate(cardPrefab, poolHolder);
            newCard.gameObject.SetActive(false);
            pool.Enqueue(newCard);
        }
    }

    /// <summary>
    /// Gets a card from the pool. If no cards are available,
    /// instantiates a new one (expandable pool).
    /// </summary>
    public Card GetCard()
    {
        if (pool.Count > 0)
        {
            Card card = pool.Dequeue();
            card.gameObject.SetActive(true);
            return card;
        }
        else
        {
            Card newCard = Instantiate(cardPrefab, poolHolder);
            return newCard;
        }
    }

    /// <summary>
    /// Returns a card back to the pool.
    /// </summary>
    public void ReturnCard(Card card)
    {
        card.gameObject.SetActive(false);
        card.transform.SetParent(poolHolder);
        pool.Enqueue(card);
    }
}
