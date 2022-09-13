using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;

namespace AddedFeats.Utils
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class BasicFunctions
    {
        public static BlueprintFeature CreateBasicFeat(string featname, string guid, string displayname, string description)
        {
            return FeatureConfigurator.New(featname, guid)
                .SetDisplayName(displayname)
                .SetDescription(description)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .Configure();
        }
    }
}

