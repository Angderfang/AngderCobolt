﻿using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features
{
    public class AMoveEnemy : AMove
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::MoveEnemy::Normal";
            if (dir > 0)
            {
                name = ModEntry.Instance.Localizations.Localize(["action", "MoveEnemy", "nameR"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "MoveEnemy", "description", "Positive"], new { number = dir});
            }
            else
            {
                name = ModEntry.Instance.Localizations.Localize(["action", "MoveEnemy", "nameL"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "MoveEnemy", "description", "Negative"], new { number = Math.Abs(dir) });
            }
            //Spr iconset = GetIcon(s);

                   //dir > 0 ? new TTGlossary(Modentry.MoveEnemyRightGlossary!.Head, dir) : new TTGlossary(Modentry.MoveEnemyLeftGlossary!.Head, -dir)

            List<Tooltip> tooltips = [
            new GlossaryTooltip(key)
            {
                
                    Icon = dir > 0 ? ModEntry.Instance.MoveenemyRight.Sprite :ModEntry.Instance.MoveenemyLeft.Sprite,
                    TitleColor = Colors.action,
                    Title = name,
                    Description = description
            }

            ];
            return tooltips;
            }

    }
}
