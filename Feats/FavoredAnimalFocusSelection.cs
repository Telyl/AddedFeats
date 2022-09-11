using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using AddedFeats.Feats.AnimalFocus;

namespace AddedFeats.Feats
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class FavoredAnimalFocusSelection
    {
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

            var selection = FeatureSelectionConfigurator.New("FavoredAnimalFocusSelection", "fe2d7d81-6631-4bff-8a35-b56c9811d7eb")
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddToAllFeatures(focbull)
                .AddToAllFeatures(focbear)
                .AddToAllFeatures(foctiger)
                .AddToAllFeatures(focfalcon)
                .AddToAllFeatures(focstag)
                .AddToAllFeatures(focmouse)
                .AddToAllFeatures(focowl)
                .AddToAllFeatures(focmonkey)
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

