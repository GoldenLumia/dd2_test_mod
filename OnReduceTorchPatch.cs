using Assets.Code.Game;
using Assets.Code.Inputs;
using Assets.Code.Item;
using Assets.Code.Library;
using Assets.Code.Utils;
using HarmonyLib;

namespace DD2_TestMod.Patches
{
    [HarmonyPatch(typeof(Cheats))]
    [HarmonyPatch("OnReduceTorch")]
    internal class OnReduceTorchPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(string action, InputActionDelegateValues values)
        {
            var areCheatsUnblocked = AccessTools.Method(typeof(Cheats), "AreCheatsUnblocked");
            bool cheatsUnblocked = (bool)areCheatsUnblocked.Invoke(null, null);

            if (values.m_performed && cheatsUnblocked && !TextBasedEditorPrefs.GetBool(TextBasedEditorPrefsBaseType.DRIVING_DISABLE_HOTKEYS))
            {
                ItemDefinition libraryElement = SingletonMonoBehaviour<Library<string, ItemDefinition>>.Instance.GetLibraryElement("valley_baubles");
                Singleton<GameTypeMgr>.Instance.PlayerInventory.AddItems(libraryElement, 10, false);
            }
            return false;
        }
    }
}