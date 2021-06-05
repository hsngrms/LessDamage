using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace Less_Damage
    {
    public class LDSubModule : MBSubModuleBase
        {
        protected override void OnSubModuleLoad()
            {
            base.OnSubModuleLoad();
            new Harmony("hsngrms.less.damage.updated").PatchAll();
            }
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
            {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("Less Damage original (1/1) enabled"));
            }
        }

    [HarmonyPatch(typeof(Mission), "GetDamageMultiplierOfCombatDifficulty")]
    internal class GetDamageMultiplierOfCombatDifficultyModification
        {
        public static void Postfix(Agent affectedAgent, ref float __result)
            {
            try
                {
                if (affectedAgent.IsMainAgent)        // damage received by player, not inflicted by player, one fortieth
                    __result = 0.025f;
                if (affectedAgent.IsMount)        // damage received by mounts, one hundredth
                    __result = 0.01f;
                else
                    __result = 1f;        // default value 1f means full damage, so 0.1f means one tenth (1/10) damage
                }
            catch
                {
                }
            }
        }

    }