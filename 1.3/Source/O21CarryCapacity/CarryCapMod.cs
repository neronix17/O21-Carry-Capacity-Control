using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21CarryCapacity
{
    public class CarryCapMod : Mod
    {
        public static CarryCapSettings settings;
        public static CarryCapMod mod;

        public CarryCapMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<CarryCapSettings>();
            mod = this;
        }

        public override string SettingsCategory()
        {
            return "Carry Capacity Control";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("Capacity Multiplier");
            listingStandard.GapLine();
            listingStandard.Label("This multiplies the base carry capacity of a pawn.");
            listingStandard.Label("Default: 0.50, Min-Max: 0.05 - 5.00, Current: " + settings.capacityMultiplier.ToString("0.00"));
            settings.capacityMultiplier = listingStandard.Slider(settings.capacityMultiplier, 0.05f, 5.00f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }

    public class CarryCapSettings : ModSettings
    {
        public float capacityMultiplier = 0.5f;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref capacityMultiplier, "capacityMultiplier", 0.5f);
        }
    }
}
