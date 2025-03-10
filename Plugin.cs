using DD2_TestMod.Patches;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace DD2_TestMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class TutorialModBase : BaseUnityPlugin
    {
        private const string modGUID = "Goldenlumia.DDBaubles";
        private const string modName = "Baubles Replace Torch Reduce Cheat";
        private const string modVersion = "1.0.0.2";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static TutorialModBase Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            
            mls.LogInfo("GOLD: MOD LOADED");

            harmony.PatchAll(typeof(TutorialModBase));
            harmony.PatchAll(typeof(OnReduceTorchPatch));
        }
    }
}