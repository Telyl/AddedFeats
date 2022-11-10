using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;

/* Taken directly from TabletopTweaks-Core. Did not want to have a TTT-Core dependency.*/
/* Credit goes to Vek17 */
namespace AddedFeats.Utils
{
    public interface IBlueprintCacheInitHandler : IGlobalSubscriber
    {
        void BeforeBlueprintCachePatches();
        void BeforeBlueprintCacheInit();
        void AfterBlueprintCacheInit();
        void AfterBlueprintCachePatches();

        [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
        internal static class BlueprintsCache_Init_EventHandler
        {

            static void Prefix()
            {
                EventBus.RaiseEvent<IBlueprintCacheInitHandler>(h => h.BeforeBlueprintCacheInit());
            }

            static void Postfix()
            {
                EventBus.RaiseEvent<IBlueprintCacheInitHandler>(h => h.AfterBlueprintCacheInit());
            }
        }
        [HarmonyPatch(typeof(StartGameLoader), nameof(StartGameLoader.LoadPackTOC))]
        internal class StartGameLoader_LoadPackTOC_EventHandler
        {
            static void Prefix()
            {
                EventBus.RaiseEvent<IBlueprintCacheInitHandler>(h => h.BeforeBlueprintCachePatches());
            }

            static void Postfix()
            {
                EventBus.RaiseEvent<IBlueprintCacheInitHandler>(h => h.AfterBlueprintCachePatches());
            }
        }
    }
}