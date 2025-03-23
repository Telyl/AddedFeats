using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;

namespace AddedFeats.NewFeatures.FavoredAnimalFocus
{
    internal class FavoredAnimalFocusSelection
    {
        private static readonly string FeatName = "FavoredAnimalFocusSelection";
        internal const string DisplayName = "FavoredAnimalFocus.Name";
        private static readonly string Description = "FavoredAnimalFocus.Description";

        public static void Configure()
        {
            var focbear = AnimalFocusBear.Configure();
            var focbull = AnimalFocusBull.Configure();
            var focfalcon = AnimalFocusFalcon.Configure();
            var focmonkey = AnimalFocusMonkey.Configure();
            var focmouse = AnimalFocusMouse.Configure();
            var focowl = AnimalFocusOwl.Configure();
            var focstag = AnimalFocusStag.Configure();
            var foctiger = AnimalFocusTiger.Configure();

            var selection = FeatureSelectionConfigurator.New(FeatName, Guids.FavoredAnimalFocusSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddToAllFeatures(focbear, focbull, focfalcon, focmonkey, focmouse, focowl, focstag, foctiger)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .Configure();

            var basicFeatSelectionGuid = "247a4068-296e-8be4-2890-143f451b4b45";
            FeatureSelectionConfigurator.For(basicFeatSelectionGuid)
                .AddToAllFeatures(selection)
                .Configure();
        }
    }
}
