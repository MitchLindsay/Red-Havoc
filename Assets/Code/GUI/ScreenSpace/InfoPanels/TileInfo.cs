﻿using Assets.Code.GUI.WorldSpace;
using Assets.Code.TileMaps.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class TileInfo : InfoPanelController
    {
        public Text TileName;
        public Text HealthRegenBonus;
        public Text DefenseBonus;
        public Text MovementBonus;

        public Image TileGraphic;
        public Image HealthRegenBonusIcon;
        public Image DefenseBonusIcon;
        public Image MovementBonusIcon;

        void OnEnable()
        {
            MouseCursor.OnMouseOverTile += SetInfo;
        }

        void OnDestroy()
        {
            MouseCursor.OnMouseOverTile -= SetInfo;
        }

        public override void SetInfo(GameObject tileGameObject)
        {
            if (InfoPanel != null)
            {
                if (tileGameObject != null)
                {
                    Tile tile = tileGameObject.GetComponent<Tile>();
                    Sprite sprite = tileGameObject.GetComponent<SpriteRenderer>().sprite;

                    if (tile != null && sprite != null)
                    {
                        ShowInfo();
                        SetTextElement(TileName, tile.EntityName);

                        SetStatBonusElement(HealthRegenBonus, HealthRegenBonusIcon, tile.HealthRegenBonus);
                        SetStatBonusElement(DefenseBonus, DefenseBonusIcon, tile.DefenseBonus);
                        SetStatBonusElement(MovementBonus, MovementBonusIcon, tile.MovementBonus);

                        SetImageElement(TileGraphic, sprite);
                    }
                    else
                        HideInfo();
                }
                else
                    HideInfo();
            }
        }

        private void SetStatBonusElement(Text textElement, Image imageElement, int statBonusValue)
        {
            string statBonusText = "";

            if (statBonusValue > 0)
            {
                statBonusText = "+" + statBonusValue.ToString();

                SetElementColor(textElement, ColorStatIncrease);
                SetElementColor(imageElement, ColorStatIncrease);
            }
            else if (statBonusValue < 0)
            {
                statBonusText = statBonusValue.ToString();

                SetElementColor(textElement, ColorStatDecrease);
                SetElementColor(imageElement, ColorStatDecrease);
            }
            else
            {
                statBonusText = "0";

                SetElementColor(textElement, ColorStatNeutral);
                SetElementColor(imageElement, ColorStatNeutral);
            }

            SetTextElement(textElement, statBonusText);
        }
    }
}