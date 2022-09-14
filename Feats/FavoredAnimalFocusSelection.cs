using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using AddedFeats.Feats.AnimalFocus;
using AddedFeats.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace AddedFeats.Feats
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class FavoredAnimalFocusSelection
    {
        private static readonly string FeatName = "FavoredAnimalFocusSelection";
        private static readonly string DisplayName = "FavoredAnimalFocus.Name";
        private static readonly string Description = "FavoredAnimalFocus.Description";
        public static void Configure()
        {
            var focbull = FavoredAnimalFocusBull.Configure();
            var focbear = FavoredAnimalFocusBear.Configure();
            var foctiger = FavoredAnimalFocusTiger.Configure();
            var focfalcon = FavoredAnimalFocusFalcon.Configure();
            var focstag = FavoredAnimalFocusStag.Configure();
            var focmouse = FavoredAnimalFocusMouse.Configure();
            var focowl = FavoredAnimalFocusOwl.Configure();
            var focmonkey = FavoredAnimalFocusMonkey.Configure();

            var selection = FeatureSelectionConfigurator.New(FeatName, Guids.FavoredAnimalFocusSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddToAllFeatures(focbull, focbear, foctiger, focfalcon, focstag, focmouse, focowl, focmonkey)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .Configure();

            var basicFeatSelectionGuid = "247a4068-296e-8be4-2890-143f451b4b45";
            FeatureSelectionConfigurator.For(basicFeatSelectionGuid)
                .AddToAllFeatures(selection)
                .Configure();
        }

        public static bool SetGroup(BlueprintFeatureSelection selection, FeatureGroup[] featuregroups)
        {
            try
            {
                FeatureSelectionConfigurator.For(selection)
                .AddToGroups(featuregroups)
                .Configure();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

