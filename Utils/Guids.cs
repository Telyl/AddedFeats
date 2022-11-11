using AddedFeats.Feats;
using AddedFeats.NewSpells;
using AddedFeats.Homebrew;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace AddedFeats.Utils
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class Guids
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Guids));


        //** Feats **//
        #region Features
        // Favored Animal Selection Feat
        internal const string FavoredAnimalFocusSelection = "fe2d7d81-6631-4bff-8a35-b56c9811d7eb";
        // Favored Animal Focus Tiger
        internal const string FavoredAnimalFocusTiger = "85120543-d4b6-403e-ac84-3acf07fa2d52";
        internal const string FavoredAnimalFocusTigerPet = "c246781a-4e26-460d-ab32-673307ece6f0";
        internal const string FavoredAnimalFocusTigerBuff = "cdde9ed1-46cf-4457-8b8e-af3d7b0f3055";
        // Favored Animal Focus Stag
        internal const string FavoredAnimalFocusStag = "66c9bcda-64bf-45ab-9412-477c28bf61ce";
        internal const string FavoredAnimalFocusStagPet = "e94f30bf-2072-42fc-bb81-9e8f009b788f";
        internal const string FavoredAnimalFocusStagBuff = "0b56ec52-cc94-49eb-a65a-37cd3af3b061";
        // Favored Animal Focus Owl
        internal const string FavoredAnimalFocusOwl = "4cfe9a7f-74d8-46d2-bc47-aafd383b30d6";
        internal const string FavoredAnimalFocusOwlPet = "b6096f3e-e95a-4700-b7fa-695a3f25f103";
        internal const string FavoredAnimalFocusOwlBuff = "725a9c22-a69f-41ee-9aed-cddd2b2af560";
        // Favored Animal Focus Mouse
        internal const string FavoredAnimalFocusMouse = "8ad4b17f-fc7d-41b3-9021-fe70108484a1";
        internal const string FavoredAnimalFocusMousePet = "373677ff-532d-49d0-bee3-eb6513fef7c7";
        internal const string FavoredAnimalFocusMouseBuff = "322ddf08-5a72-41c1-a020-cf0f871967ec";
        // Favored Animal Focus Monkey
        internal const string FavoredAnimalFocusMonkey = "fa0ba92e-6304-494f-be38-8e4a4a840e16";
        internal const string FavoredAnimalFocusMonkeyPet = "b462262d-73ee-4f85-a321-6c4a63de44cf";
        internal const string FavoredAnimalFocusMonkeyBuff = "f5273925-3e0f-4b91-9bdc-26d39a674825";
        // Favored Animal Focus Falcon
        internal const string FavoredAnimalFocusFalcon = "acfeb870-ad3f-4036-874f-4ec277944290";
        internal const string FavoredAnimalFocusFalconPet = "8f297ce8-6233-4ef6-a578-0988ded5dc0b";
        internal const string FavoredAnimalFocusFalconBuff = "b101cef2-6e25-46ac-945c-bf9f1fcdb722";
        // Favored Animal Focus Bull
        internal const string FavoredAnimalFocusBull = "60b339a3-33e6-4fce-0c9c-e51403ef5ada";
        internal const string FavoredAnimalFocusBullPet = "60c339a3-33e3-4fce-8c8c-e51403ef5ada";
        internal const string FavoredAnimalFocusBullBuff = "16c88e20-b823-44b9-b15f-e69fc4c6a556";
        // Favored Animal Focus Bear
        internal const string FavoredAnimalFocusBear = "bca79523-3d18-4ff6-ae9d-a14ace7ff516";
        internal const string FavoredAnimalFocusBearPet = "80a1a04e-ed27-43f9-8ee0-783625d155c6";
        internal const string FavoredAnimalFocusBearBuff = "ef3f2bfe-6ab0-4259-ae1e-5a6aa14cb4a2";

        // Forceful Charge
        internal const string ForcefulCharge = "b7874cf6-0a94-4685-a295-f2467b6c5428";
        internal const string ForcefulChargeAbility= "d8ec6b3f-c687-493a-b5d9-2b31715b5fd2";
        internal const string ForcefulChargeBuff = "508b7605-623c-468e-af65-7bac9cc36af7";
        internal const string ForcefulChargeEffect = "19041d0c-3ae9-40aa-ae0f-addc3aefc7e2";
        // Improved Forceful Charge
        internal const string ImprovedForcefulCharge = "c6374cf6-0b94-4386-a281-f1167b6c5428";
        internal const string ImprovedForcefulChargeEffect = "5ccb7545-623c-468e-bbbb-7b32ccc36af7";

        // Planar Focus
        internal const string PlanarFocus = "3d64ed44-42f3-4a8f-b7ae-a927a45af820";
        // Planar Focus Air
        internal const string PlanarFocusAir = "31c9d157-fb70-4997-a0b3-56f9026d1c40";
        internal const string PlanarFocusAirBuff = "31d9d157-fb70-4997-a0b3-56f9026d1c40";
        internal const string PlanarFocusAirAnimalBuffEffect = "e55cc808-4cf2-4272-88f5-bda65d0417fc";
        internal const string PlanarFocusAirEffect = "06c88e20-b823-44b9-b15f-e69fc4c6a556";
        internal const string PlanarFocusAirAnimalBuff = "56722f70-03e5-4272-9aa3-115382ae869f";
        // Planar Focus Chaotic
        internal const string PlanarFocusChaotic = "4828c37e-3d59-4358-a042-b9fa4fb7bb72";
        internal const string PlanarFocusChaoticBuff = "db903e19-7586-45a5-ac05-3cb8b410f039";
        internal const string PlanarFocusChaoticAnimalBuffEffect = "502cba69-f94a-4399-b37f-3577ad8018a2";
        internal const string PlanarFocusChaoticEffect = "b5741066-de62-447c-8ba5-e265aa4e1292";
        internal const string PlanarFocusChaoticAnimalBuff = "fd904798-952b-45aa-b594-0841156bdc7a";
        // Planar Focus Cold
        internal const string PlanarFocusCold = "cc239df8-dfbf-49d2-86ca-ea462717ab4d";
        internal const string PlanarFocusColdBuff = "f47e182b-f8d3-4477-b78e-075fc8205892";
        internal const string PlanarFocusColdAnimalBuffEffect = "775bb39b-8d48-40d9-bed7-55f2411575dd";
        internal const string PlanarFocusColdEffect = "818f4cdb-2939-4824-90cc-118269fe5203";
        internal const string PlanarFocusColdAnimalBuff = "958b8209-4ece-4233-b592-87c3997fc073";
        // Planar Focus Earth
        internal const string PlanarFocusEarth = "548da62f-f01a-420c-9528-e8bee75a7d34";
        internal const string PlanarFocusEarthBuff = "7119f97b-110a-4001-b770-8b5eb08799e5";
        internal const string PlanarFocusEarthAnimalBuffEffect = "9d3fbbc5-0609-4fe3-9cd2-53991c10620d";
        internal const string PlanarFocusEarthEffect = "6ebaa1dc-a6d2-48f9-81fa-80eb1d822b6e";
        internal const string PlanarFocusEarthAnimalBuff = "b0f6b4b8-3dc7-4197-95ad-b242db6651bd";
        // Planar Focus Evil
        internal const string PlanarFocusEvil = "db542378-f53b-4d5c-ada2-461cea97a6b3";
        internal const string PlanarFocusEvilBuff = "2d294f73-f191-4309-b103-7a3eecc505b0";
        internal const string PlanarFocusEvilAnimalBuffEffect = "b81875c8-313b-473f-a16c-be34c011b579";
        internal const string PlanarFocusEvilEffect = "a54b8dd2-d0cc-44d3-bd6e-068274031a3b";
        internal const string PlanarFocusEvilAnimalBuff = "856c95f9-05a9-4dd6-89b3-6f2ebd51eef2";
        // Planar Focus Fire
        internal const string PlanarFocusFire = "33628596-101f-4c2d-bcf8-e6814ddda153";
        internal const string PlanarFocusFireBuff = "456b7bc9-600b-4ac6-8a57-3f5d6061753d";
        internal const string PlanarFocusFireAnimalBuffEffect = "d2864f28-e5f9-4421-ad0f-c64f63124906";
        internal const string PlanarFocusFireEffect = "9899c88c-2e31-425b-946e-829e8666387e";
        internal const string PlanarFocusFireAnimalBuff = "595d616f-73ac-4da7-aa3b-402eeb53ee79";
        // Planar Focus Good
        internal const string PlanarFocusGood = "dc063444-4fdf-45e2-95dd-6c656be7eea0";
        internal const string PlanarFocusGoodBuff = "dd28cb24-248f-4db5-b633-8d4d5e9fcd97";
        internal const string PlanarFocusGoodAnimalBuffEffect = "0b264ada-ab69-4e83-b146-51a2cb8d5365";
        internal const string PlanarFocusGoodEffect = "ab623544-9f58-4065-a969-7b080b9745fe";
        internal const string PlanarFocusGoodAnimalBuff = "16500163-506d-4043-9ed9-05fd0ec902dd";
        // Planar Focus Lawful
        internal const string PlanarFocusLawful = "9db5c2d0-1910-4da6-ac3c-a6c57d64fd7c";
        internal const string PlanarFocusLawfulBuff = "d8cef07b-85f8-416b-80e9-da08e98f14f3";
        internal const string PlanarFocusLawfulAnimalBuffEffect = "92b8a842-5176-4d3d-9fb9-7a3a9100d80d";
        internal const string PlanarFocusLawfulEffect = "0963ac0b-d3a5-4609-9755-1c85208309a9";
        internal const string PlanarFocusLawfulAnimalBuff = "984434d3-991b-4f80-a522-3ffc949f2906";
        // Planar Focus Shadow
        internal const string PlanarFocusShadow = "4389de8f-407c-43fe-88fe-3acbd3de4d79";
        internal const string PlanarFocusShadowBuff = "caa05043-9cea-48e3-94ea-01e223abfdfb";
        internal const string PlanarFocusShadowAnimalBuffEffect = "0c40850d-e56d-4cbe-98af-536287e3d742";
        internal const string PlanarFocusShadowEffect = "b1269d4f-1ae6-4d8e-8257-03fa1fc86fdb";
        internal const string PlanarFocusShadowAnimalBuff = "275b8ffc-475a-41a7-aeab-f41379cb2c73";
        // Planar Focus Water
        internal const string PlanarFocusWater = "b1b7951d-6c39-4b4b-9848-907436cacb1f";
        internal const string PlanarFocusWaterBuff = "b47bab02-098a-4b50-a244-dd4e62711658";
        internal const string PlanarFocusWaterAnimalBuffEffect = "6c912f1a-16e6-4870-b0d8-a3cb881bbfaa";
        internal const string PlanarFocusWaterEffect = "4ca8f08f-124d-4cd6-a04c-8164192f0d87";
        internal const string PlanarFocusWaterAnimalBuff = "736fd667-b68a-4e65-bee2-78cd55f3c27f";

        //Improved Natural Armor
        internal const string ImprovedNaturalArmor = "5f91ffea-81a3-4743-894b-e76e39c9567c";
        //Improved Natural Attack
        internal const string ImprovedNaturalAttack = "f020ce8c-09df-4ec3-a10f-92d08f69dc94";

        //EVOLUTIONS
        internal const string ImprovedDamage = "d79bb9d8-f021-462f-b9af-33744de95e20";
        internal const string ImprovedDamagePet = "c79bb9d8-f021-462f-b9af-33744de95e20";

        //***********//
        internal static readonly (string guid, string displayName)[] Features =
          new (string, string)[]
          {
              (PlanarFocus, Feats.PlanarFocus.DisplayName),
              (ForcefulCharge, Feats.ForcefulCharge.DisplayName),
              (FavoredAnimalFocusSelection, Feats.FavoredAnimalFocusSelection.DisplayName),
              (ImprovedNaturalArmor, Feats.ImprovedNaturalArmor.DisplayName),
              (ImprovedNaturalAttack, Feats.ImprovedNaturalAttack.DisplayName),
              (ImprovedDamage, Feats.ImprovedDamage.DisplayName),
          };
        #endregion

        #region Spells
        // Strong Jaw (Spell)
        internal const string StrongJawSpell = "7cd44ce1-3575-4ceb-b81b-dc71d66415c0";
        internal const string StrongJawSpellBuff = "6cd44ce1-3575-4ceb-b81b-dc71d66415c0";
        internal static readonly (string guid, string displayName)[] Spells =
          new (string, string)[]
          {
              (StrongJawSpell, StrongJaw.DisplayName),
          };
        #endregion

        #region Homebrew
        internal const string MythicAnimalFocus = "f15fc8d6-b2cd-495d-8977-bf99e6176d1d";
        internal static readonly (string guid, string displayName)[] Homebrews =
          new (string, string)[]
          {
              (MythicAnimalFocus, Homebrew.MythicAnimalFocus.DisplayName),
          };
        #endregion

    }
}

