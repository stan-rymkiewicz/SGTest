using System;
using CodeBase.Core;
using UnityEngine;

namespace CodeBase.AceOfShadows
{
    public class AceOfShadowsModule : GameModuleController
    {
        CardDeck startDeck = new CardDeck();
        CardDeck endDeck = new CardDeck();
        
        //Game setup
        private GameObject _cardPefab;
        public int CardsCount = 144;
        public Vector3 CardsOffset = new Vector3(0, 0, 0);
        public Vector3 _startDeckPosition;

        public Vector3 _targetDeckPosition;
        public Vector3 _targetDecksOffset;
        public int numberOfStacks = 2;

    }


}