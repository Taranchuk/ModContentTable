using RimWorld;
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using System.Linq;

namespace ModContentTable
{
    public static class DebugTable
    {
        public static Dictionary<string, ModData> modsWithDefs = new Dictionary<string, ModData>();

        [DebugOutput]
        public static void ModContent()
        {
            CreateModContentData();
            foreach (var modData in modsWithDefs.Values)
            {
                modData.CreateContentData();
            }
            var ordered = modsWithDefs.OrderByDescending(x => x.Value.foundDefs.Sum(y => y.Value)).ToList();
            var list = new List<TableDataGetter<KeyValuePair<string, ModData>>>();
            list.Add(new TableDataGetter<KeyValuePair<string, ModData>>("Mod name", (KeyValuePair<string, ModData> kvp) => kvp.Key));
            list.Add(new TableDataGetter<KeyValuePair<string, ModData>>("Total defs", (KeyValuePair<string, ModData> kvp) => kvp.Value.defCount));
            list.Add(new TableDataGetter<KeyValuePair<string, ModData>>("Is C# mod", (KeyValuePair<string, ModData> kvp) => kvp.Value.isCSharpMod));
            foreach (var key in ModData.categoryValidators.Keys)
            {
                list.Add(new TableDataGetter<KeyValuePair<string, ModData>>(key.CapitalizeFirst(), (KeyValuePair<string, ModData> kvp) =>
                    kvp.Value.foundDefs.ContainsKey(key) ? kvp.Value.foundDefs[key] : 0));
            }
            DebugTables.MakeTablesDialog(ordered, list.ToArray());
        }

        public static void ModContentReport()
        {
            CreateModContentData();
            string mess = "Mod content analyzer:\n";
            foreach (var mod in modsWithDefs)
            {
                mod.Value.CreateContentData();
            }
            foreach (var modData in modsWithDefs.OrderByDescending(x => x.Value.foundDefs.Sum(y => y.Value)))
            {
                if (modData.Value.foundDefs.Sum(x => x.Value) > 0)
                {
                    mess += modData.Key + " " + modData.Value.contentData + "\n";
                }
            }
            foreach (var modData in modsWithDefs.OrderByDescending(x => x.Value.defCount))
            {
                if (modData.Value.foundDefs.Sum(x => x.Value) <= 0)
                {
                    mess += modData.Key + " (" + modData.Value.defCount + ")\n";
                }
            }
            Log.Warning(mess);
        }

        private static void CreateModContentData()
        {
            if (modsWithDefs.Count == 0)
            {
                foreach (Type item in typeof(Def).AllSubclasses())
                {
                    foreach (var def in GenDefDatabase.GetAllDefsInDatabaseForDef(item))
                    {
                        if (def.modContentPack != null && !def.modContentPack.IsOfficialMod)
                        {
                            var name = def?.modContentPack?.Name;
                            if (name != null)
                            {
                                if (!modsWithDefs.TryGetValue(name, out var modData))
                                {
                                    modsWithDefs[name] = modData = new ModData();
                                }
                                modData.RegisterDef(def);
                            }
                        }
                    }
                }

                foreach (ModContentPack mod in LoadedModManager.RunningModsListForReading)
                {
                    if (!mod.IsOfficialMod)
                    {
                        if (!modsWithDefs.TryGetValue(mod.Name, out var modData))
                        {
                            modsWithDefs[mod.Name] = modData = new ModData();
                        }
                        modData.isCSharpMod = mod.assemblies?.loadedAssemblies?.Count > 0;
                    }
                }
            }
        }
    }
    public class ModData
    {
        public bool isCSharpMod;
        public int defCount;
        private HashSet<Def> defs = new HashSet<Def>();
        private HashSet<Def> tmpDefs = new HashSet<Def>();
        public Dictionary<string, int> foundDefs = new Dictionary<string, int>();
        public string contentData;
        public static Dictionary<string, Func<Def, bool>> categoryValidators = new Dictionary<string, Func<Def, bool>>();
        static ModData()
        {
            categoryValidators.Add("incidents", x => x is IncidentDef);
            categoryValidators.Add("quests", x => x is QuestScriptDef def && def.IsRootAny);
            categoryValidators.Add("game conditions", x => x is GameConditionDef);
            categoryValidators.Add("weathers", x => x is WeatherDef);
            categoryValidators.Add("factions", x => x is FactionDef);
            categoryValidators.Add("humanlikes", x => x is ThingDef def && def.race != null && def.race.Humanlike);
            categoryValidators.Add("animals", x => x is ThingDef def && def.race != null && def.race.Animal);
            categoryValidators.Add("mechanoids", x => x is ThingDef def && def.race != null && def.race.IsMechanoid);
            categoryValidators.Add("insects", x => x is ThingDef def && def.race != null && def.race.Insect);
            categoryValidators.Add("turrets", x => x is ThingDef def && def.building?.turretGunDef != null);
            categoryValidators.Add("workbenches", x => x is ThingDef def && def.IsWorkTable);
            categoryValidators.Add("buildings", x => x is ThingDef def && def.IsBuildingArtificial);
            categoryValidators.Add("works", x => x is WorkGiverDef);
            categoryValidators.Add("floors", x => x is TerrainDef def && def.layerable && def.designationCategory != null);
            categoryValidators.Add("plants", x => x is ThingDef def && def.plant != null);
            categoryValidators.Add("armors", x => IsArmor(x));
            categoryValidators.Add("apparels", x => x is ThingDef def && def.IsApparel);
            categoryValidators.Add("melee weapons", x => x is ThingDef def && def.IsMeleeWeapon && !def.destroyOnDrop);
            categoryValidators.Add("range weapons", x => x is ThingDef def && def.IsRangedWeapon && !def.destroyOnDrop);
            categoryValidators.Add("resources", x => x is ThingDef def && def.IsStuff);
            categoryValidators.Add("drugs", x => x is ThingDef def && def.IsDrug);
            categoryValidators.Add("food", x => x is ThingDef def && def.IsIngestible);
            categoryValidators.Add("recipes", x => x is RecipeDef);
            categoryValidators.Add("research projects", x => x is ResearchProjectDef);
            categoryValidators.Add("precepts", x => x is PreceptDef);
            categoryValidators.Add("gatherings", x => x is GatheringDef);
            categoryValidators.Add("interactions", x => x is InteractionDef);
            categoryValidators.Add("joygivers", x => x is JoyGiverDef);
            categoryValidators.Add("thoughts", x => x is ThoughtDef);
            categoryValidators.Add("traits", x => x is TraitDef);
            categoryValidators.Add("abilities", x => x is AbilityDef);
            categoryValidators.Add("body parts", x => x is HediffDef def && typeof(Hediff_AddedPart).IsAssignableFrom(def.hediffClass));
            categoryValidators.Add("implants", x => x is HediffDef def && typeof(Hediff_Implant).IsAssignableFrom(def.hediffClass));
            categoryValidators.Add("bad hediffs", x => x is HediffDef def && (def.isBad || def.makesSickThought
            || def.lethalSeverity > 0 || def.HasComp(typeof(HediffComp_Immunizable))));
        }
        private static bool IsArmor(Def x)
        {
            if (x is ThingDef def)
            {
                if (def.thingCategories != null)
                {
                    if (def.thingCategories.Contains(ThingCategoryDefOf.ArmorHeadgear)
                     || def.thingCategories.Contains(ThingCategoryDefOf.ApparelArmor))
                    {
                        return true;
                    }
                }
                if (def.apparel?.defaultOutfitTags != null)
                {
                    return def.apparel.defaultOutfitTags.Contains("Soldier");
                }
            }
            return false;
        }
        public void RegisterDef(Def def)
        {
            if (def is ThingDef thingDef && (thingDef.IsFrame || thingDef.IsBlueprint || thingDef.IsCorpse
                || thingDef.IsMeat || thingDef.IsLeather))
            {
                return;
            }
            defCount++;
            defs.Add(def);
        }

        public void CreateContentData()
        {
            tmpDefs = defs.ToHashSet();
            List<string> output = new List<string>();
            foreach (var data in categoryValidators)
            {
                OutputAmount(output, data.Key, data.Value);
            }
            contentData = "(Found defs: " + foundDefs.Sum(x => x.Value) + ") - " + string.Join(", ", output);

        }

        private void OutputAmount(List<string> output, string category, Func<Def, bool> validator)
        {
            var filteredDefs = tmpDefs.Where(validator);
            if (filteredDefs.Any())
            {
                var count = filteredDefs.Count();
                output.Add(category + ": " + count);
                foundDefs[category] = count;
                //output.Add(category + ": (" + string.Join(", ", filteredDefs.Select(x => x.label ?? x.defName)) + ") " + filteredDefs.Count());
            }
            tmpDefs.RemoveWhere(x => validator(x));
        }
    }

}
