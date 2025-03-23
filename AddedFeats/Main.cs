using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityModManagerNet;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;
using AddedFeats.NewArchetypes;
using AddedFeats.NewFeatures.AnimalFocus;
using AddedFeats.NewFeatures.FavoredAnimalFocus;
using AddedFeats.NewFeatures.PlanarFocusFeature;
using AddedFeats.NewSpells;
using AddedFeats.NewFeatures.Teamwork;
using AddedFeats.NewFeatures;
using AddedFeats.NewFeatures.AnimalCompanionSpecific;
using AddedFeats.NewFeatures.SpiritFocus;
using AddedFeats.NewHomebrew;

namespace AddedFeats;

#if DEBUG
[EnableReloading]
#endif
public static class Main {
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger log;

    public static bool Load(UnityModManager.ModEntry modEntry) {
        log = modEntry.Logger;
#if DEBUG
        modEntry.OnUnload = OnUnload;
#endif
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        return true;
    }

    public static void OnGUI(UnityModManager.ModEntry modEntry) {

    }

#if DEBUG
    public static bool OnUnload(UnityModManager.ModEntry modEntry) {
        HarmonyInstance.UnpatchAll(modEntry.Info.Id);
        return true;
    }
#endif
    [HarmonyPatch(typeof(BlueprintsCache))]
    public static class BlueprintsCaches_Patch {
        private static bool Initialized = false;

        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        public static void Init_Postfix() {
            try {
                if (Initialized) {
                    log.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;

                log.Log("Patching blueprints.");

                //Archetypes
                Stalker.Configure();
                
                //Feats
                FavoredAnimalFocusSelection.Configure();
                AnimalFocusBat.Configure();
                AnimalFocusFrog.Configure();
                AnimalFocusSnake.Configure();
                AnimalFocusWolf.Configure();
                PlanarFocusFeature.Configure();
                VampiricCompanion.Configure();
                ForcefulCharge.Configure();
                SpiritsGift.Configure();
                ImprovedNaturalAttack.Configure();

                //Spells
                StrongJaw.Configure();
                Atavism.Configure();

                //Teamwork
                ImprovedSpellShare.Configure();

                //Homebrew
                MythicAnimalFocus.Configure();
                

            } catch (Exception e) {
                log.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }
}
