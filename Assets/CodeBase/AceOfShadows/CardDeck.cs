using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.AceOfShadows
{
    public class CardDeck
    {
        public int CardsCount = 144;
        public Vector3 CardsOffset = new Vector3(0, 0, 0);


        protected List<Transform> cards = new List<Transform>();
        
        private Vector3 _startPosition;

        public Transform GetNextCardTransform()
        {
            return cards != null && cards.Count > 0 ? cards[cards.Count - 1] : null;
        }

        public Vector3 GetNextCardPosition()
        {
            return  _startPosition + (cards != null ? cards.Count * CardsOffset : Vector3.zero);
        }

        public void Setup(GameObject cardPrefab, Vector3 startPosition, Vector3 cardOffset)
        {
            _startPosition = startPosition;
            for (int i = 0; i < CardsCount - 1; ++i)
            {
                GameObject card = GameObject.Instantiate(cardPrefab, startPosition + i * cardOffset, Quaternion.identity);
                
                cards.Add(card.transform);
            }
        }
    }

    public class MoveLogic
    {
        private CardDeck cardDeck1;
        private CardDeck cardDeck2;

        public void StartAction()
        {
            Transform card = cardDeck1.GetNextCardTransform();
            
            Vector3 startPosition = card.position;
            Vector3 endPosition = cardDeck2.GetNextCardPosition();
            
            //animate from to by time
        }
    }

    public class CardsAnimation
    {
        public float elapsedTime = 0.0f;
        public float duration = 0.0f;
        
        public Vector3 ProcessAnimation(Vector3 start, Vector3 end, float t)
        {
            return Vector3.Lerp(start, end, t);
        }
    }
}