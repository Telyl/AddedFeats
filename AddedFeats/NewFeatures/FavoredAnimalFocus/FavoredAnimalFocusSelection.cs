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
            var focbear = FavoredAnimalFocusBear.Configure();
            var focbull = FavoredAnimalFocusBull.Configure();
            var focfalcon = FavoredAnimalFocusFalcon.Configure();
            var focmonkey = FavoredAnimalFocusMonkey.Configure();
            var focmouse = FavoredAnimalFocusMouse.Configure();
            var focowl = FavoredAnimalFocusOwl.Configure();
            var focstag = FavoredAnimalFocusStag.Configure();
            var foctiger = FavoredAnimalFocusTiger.Configure();
            //var focbat = FavoredAnimalFocusBat.Configure();
            //var focfrog = FavoredAnimalFocusFrog.Configure();
            //var focsnake = FavoredAnimalFocusSnake.Configure();
            //var focwolf = FavoredAnimalFocusWolf.Configure();

            var selection = FeatureSelectionConfigurator.New(FeatName, Guids.FavoredAnimalFocusSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddToAllFeatures(focbear, focbull, focfalcon, focmonkey, focmouse, 
                focowl, focstag, foctiger) //focbat, focfrog, focsnake, focwolf)
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
