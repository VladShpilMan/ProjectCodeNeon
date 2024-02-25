using System.Collections;
using System.Collections.Generic;
using __ProjectCodeNeon.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectCodeNeon
{
    public class CardsPull : MonoBehaviour
    {
        public List<GameObject> cards;
        public Sprite[] cardSprites;

        public void SetCard(int id, int abilityId, int amount)
        {
            cards[id].transform.GetChild(4).GetComponent<Image>().sprite = cardSprites[abilityId];
            
            if(amount > 0)
                cards[id].transform.GetChild(5).GetComponent<Text>().text = "x"+amount.ToString();
            else
                cards[id].transform.GetChild(5).GetComponent<Text>().text = "x\u221e";
        }

        public void InitializePull(List<(Implant, int)> implants, int currentImplant)
        {
            for(int i = 0; i < cards.Count; i++)
                cards[i].SetActive(false);
            
            var count = 0;
            if (cards.Count < implants.Count)
                count = cards.Count;
            else
                count = implants.Count;
            
            
            List<(Implant, int)> adjustedImplants = new List<(Implant, int)>();
            adjustedImplants.AddRange(implants.GetRange(currentImplant, implants.Count - currentImplant));
            adjustedImplants.AddRange(implants.GetRange(0, currentImplant));

            // Отображение имплантов на картах
            for(int i = 0; i < adjustedImplants.Count; i++)
            {
                SetCard(i, adjustedImplants[i].Item1.Id, adjustedImplants[i].Item2);
                cards[i].SetActive(true);
            }
        }
    }
}
