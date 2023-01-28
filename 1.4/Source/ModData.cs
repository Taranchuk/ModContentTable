using RimWorld;
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using System.Linq;
using System.Collections;

namespace ModContentTable
{
    public static class DebugTable
    {
        public static Dictionary<string, ModData> modsWithDefs = new Dictionary<string, ModData>();

        [DebugOutput]
        public static void ModContentAmount()
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

        public static HashSet<string> modsToIgnore = new HashSet<string>
        {
            "OskarPotocki.VanillaFactionsExpanded.Core",
            "brrainz.harmony",
"taranchuk.fastergameloading",
"vova.mod",
"ludeon.rimworld",
"bs.performance",
"ludeon.rimworld.royalty",
"ludeon.rimworld.ideology",
"ludeon.rimworld.biotech",
"unlimitedhugs.hugslib",
"mlie.clothingsorter",
"owlchemist.cherrypicker",
"oskarpotocki.vanillafactionsexpanded.core",
"vanillaexpanded.vwe",
"oskarpotocki.vanillafactionsexpanded.medievalmodule",
"oskarpotocki.vanillafactionsexpanded.settlersmodule",
"vanillaexpanded.vfearchitect",
"krkr.locks2",
"agentblac.makepawnsprisoners",
"spoonshortage.adogsaidanimalprosthetics",
"georodin.cooldownindicator",
"activated.carbon",
"meltup.advancedchemfuelgenerator",
"kikohi.advancedparka",
"bart.atg",
"crewd.advancedtransportpods",
"nep.smallerturbines",
"mlie.allmemoriesfade",
"dubwise.dubsbadhygiene",
"rimfridge.kv.rw",
"owlchemist.toggleableoverlays",
"lwm.deepstorage",
"vanillaexpanded.vfea",
"3hstltd.usefulancientcrates",
"avilmask.commonsense",
"yoann.commonsenseopportunisticcleaningpatch",
"neronix17.tweaksgalore",
"vanillaexpanded.vbookse",
"vanillaexpanded.vcooke",
"vanillaexpanded.vcef",
"vanillaexpanded.vmemese",
"steve.betternurses",
"opa.allspecialistscanwork",
"nictrasavios.fabricorbrew",
"unlimitedhugs.allowtool",
"roolo.almostthere",
"vanillaexpanded.vfemedical",
"ifsck.ancientmedicallinking",
"scorpio.ancientscraftable",
"xrushha.animagear",
"xrushha.eldersfaction",
"vanillaexpanded.vfecore",
"kyoutaigo.animaextract",
"animafruit.velcroboy333",
"dimonsever000.animaobelisk.specific",
"dylan.animalgear",
"owlchemist.animalgear.equipment",
"puremj.mjrimmods.animalresourcelabel",
"jp.animalslikehay",
"revolus.animalsarefun",
"inglix.appareltaintedoncorpserot",
"xercaine.industrialage.artillery",
"iforgotmysocks.caravanadventures",
"mlie.morefactioninteraction",
"oskarpotocki.vfe.classical",
"vanillaexpanded.vwel",
"oskarpotocki.vfe.pirates",
"mlie.askbeforeenter",
"haecriver.outfitcopy",
"automatic.autolinks",
"scorpio.autotoolswitcher",
"razor2.3.anotherrimworldmod.autocutblight",
"jelly.autogate",
"ben.automaticnightowl",
"hol.babypaste",
"fluffy.backuppower",
"oskarpotocki.vanillaexpanded.ideologypatches",
"oskarpotocki.vanillaexpanded.royaltypatches",
"limetreesnake.systems",
"limetreesnake.furnishing",
"largemoron.ltsdbhwallpipes",
"vanillaexpanded.vaecaves",
"oskarpotocki.vfe.insectoid",
"scorpio.optimizationleathers",
"mlie.badleathercategory",
"simmin.animalsforage",
"mlie.animalsforagepatches",
"seohyeon.optimizationmeats",
"mlie.badmeatcategory",
"yoann.balancedfloorsmarketvalue",
"kaitorisenkou.ballgames",
"george.bandage.recolor",
"inertialmage.beautystatmod",
"bedrestforfoodpoisoning.1trickponyta",
"dizzyioeuy.bedding",
"thegoofyone.beerisalright",
"mlie.bestmix",
"legodude17.bba",
"machine.betterinfestations",
"mlie.betterjumppack",
"happybodi.bettermultianalyzers",
"mlie.betterprojectileorigin",
"aelanna.betterpyromania",
"steve.betterquestrewards",
"largemoron.betterscenarioresearch",
"vendan.legodude17.bsd.update",
"co.uk.epicguru.bettertranshumanists",
"owlchemist.bettervanillamasking",
"kikohi.bettergroundpenetratingscanner",
"creeper.betterinfocard",
"scorpio.beyondbiosculpting",
"daria40k.biglittlemodpatch",
"3hstltd.framework",
"syrchalis.processor.framework",
"3hstltd.berryexpanded",
"3hstltd.landmines",
"3hstltd.weapons.jam",
"inglix.fasterbiosculptingpod",
"waether.biosculptingplus",
"m00nl1ght.geologicallandforms",
"m00nl1ght.geologicallandforms.biometransitions",
"rimworld.kayesh.bionicjawsdontneednotongue",
"dark.cloning",
"shootex.contraception.mod",
"fluffy.blueprints",
"arandomkiwi.huntforme",
"owlchemist.boundarytraining",
"error277.bows.expanded",
"orion.hospitality",
"bs.mbifvte",
"neronix17.clutterstructures",
"neronix17.embrasures",
"calamabanana.breachableembrasures",
"mlie.brrandphew",
"insertkey.buildertweaks",
"8z.callforintel",
"bratwurstinator.callouts",
"syrus.caravanmoodbuff",
"kathanon.floatsubmenu",
"kathanon.categorizedbillmenus",
"mlie.catshuntforfun",
"mlie.centralizedclimatecontrol",
"vent.chemicalbarrels",
"owlchemist.chickennests",
"kb.chillout",
"owlchemist.cleanpathfinding",
"regrowth.botr.core",
"ih.clean.textures",
"aelanna.cleaningspeed",
"hatti.cleaningarea",
"mlie.clutterdoors",
"fluffy.colonymanager",
"jgh.cmr",
"kikohi.coloreddeepresources",
"edern.combatpsycasts",
"petetimessix.compacthediffs",
"stevezero.conditionmatters",
"victor.cons.psysens",
"kathanon.craftwithcolor",
"royalf.craftablerbarrels",
"xrushha.craftablebroadshieldcore",
"phomor.craftingqualityrebalanced",
"jelly.craftsmanship",
"iexist.crossbows",
"pillowesque.customplaystylepresets",
"mlie.customprisonerinteractions",
"kikohi.cybernetic",
"vanillaexpanded.vappe",
"mlie.cyberneticwarfare",
"georodin.deepdrillindicator",
"caesarv6.damageindicators",
"mlie.deepstorageplus",
"mlie.defaultingredientradius",
"chv.diagonalwalls2",
"pyrce.dire.raids",
"flangopink.disableautoselecttravelsupplies",
"brat.diversitydiscrimination",
"gguake.ai.dsfi",
"kb.prioritizeresearch",
"jamaicancastle.rf.fertilefields",
"nanoce.glasslights",
"regrowth.botr.aspenforest",
"vanillaexpanded.vplantse",
"docworld.lite",
"hlx.dontsettlethere",
"telardo.dragselect",
"dragonking.dragonsfertilefieldsregrowthpatches",
"eagle0600.dresspatients.1.4",
"mlie.drugpolicyfix",
"mlie.drugresponse",
"dubwise.dubsappareltweaks",
"sztorno.dubsbadhygienewtpatch",
"dubwise.dubsbreakmod",
"dubwise.dubsmintmenus",
"dubwise.dubsmintminimap",
"dubwise.dubsperformanceanalyzerbutchered",
"dubwise.dubsskylights",
"maaxar.dubsskylights.glasslights.patch",
"nilchei.dynamicdiplomacy",
"anomalous.dynamicpopulation",
"etrt.medicineproduction",
"mlie.eggincubator",
"ellit.glitterpaths",
"gondragon.electricgrid",
"carnysenpai.enableoversizedweapons",
"murmur.enemyselfpreservation",
"smartkar.enhancedvatlearning",
"mlie.enterhere",
"erin.body.texture",
"erin.decorations",
"bart.em",
"turnovus.submod.extendedbioengineering",
"princess.extracompstats",
"witek.fabricorscanrepairmechs",
"murmur.factionraidcooldown",
"mlie.farmingexpansion",
"humphrey.personal.fillthetables",
"vanillaexpanded.vweg",
"mosi.firefoamthings",
"warlocksforge.fishtraps",
"oskarpotocki.vfe.mechanoid",
"sugi.fixmechanoidresources",
"crocodil.foodpoisoningcures",
"mlie.foodpoisoningstackfix",
"garethp.ashanddust",
"mlie.fuelburning",
"miyamiya.functionalroom.latest",
"vanillaexpanded.vfepropsanddecor",
"mlie.functionalvanillaexpandedprops",
"gt.sam.furnace",
"tk421storm.gardens",
"uuugggg.gearupandgo",
"lowli.genebankplus",
"danielwedemeyer.generipper",
"tac.genetrader",
"neronix17.toolbox",
"leafzxg.thrumboextension",
"roolo.giddyupcore",
"roolo.giddyupbattlemounts",
"roolo.giddyupcaravan",
"roolo.giddyuprideandroll",
"thegoofyone.glowingambrosia",
"albion.goexplore",
"khorbos.gotosleep",
"telefonmast.graphicssettings",
"arkymn.agingvisuals",
"ancientgammoner.groundtargeter",
"name.krypt.rimworld.rwlayout.alpha2",
"name.krypt.rimworld.pawntablegrouped",
"arandomkiwi.guardsforme",
"mlie.harvestwhenbutchering",
"mlie.haulminedchunks",
"mlie.heatstatistics",
"dubwise.dubscentralheating",
"vanillaexpanded.helixiengas",
"cixwow.helixienboiler",
"vanillaexpanded.vfeproduction",
"cixwow.helixiengasproduction",
"uveren.hemogenextractor",
"mlie.hightechlaboratoryfacilities",
"bart.htlr",
"doctorstupid.prettyskin",
"mlie.huntersusemelee",
"mlie.huntingrestriction",
"mlie.hydroponicsgrowthsync",
"spacemoth.hygienerecycled",
"mlie.icantfixthat",
"kathanon.imoverhere",
"dame.ignorance",
"kathanon.impressionablechildren",
"mlie.incidentpersonstat",
"murmur.isid",
"haecriver.injuredcarry",
"turnovus.biotech.integratedgenes",
"turnovus.biotech.integratedxenotypes",
"atkana.interceptiontweaks",
"kota.inventorytab",
"invisiblefiresprinkler.dbh.capi",
"kikohi.jewelry",
"iforgotmysocks.justcopy",
"brrainz.justignoremepassing",
"mlie.justputitoverthere",
"dorely.emergencysurgery",
"dhultgren.keepbedownership",
"lwm.fuelfilter",
"troopersmith1.deathrattle",
"xavior.lifesupport",
"syrus.lightmap",
"brrainz.lineofsightfix",
"mlie.linkablesettings",
"vanillaexpanded.vfepower",
"4loris4.morelinkables",
"brat.linkpatch",
"sztorno.liquidtanks",
"sixdd.littlestorage2",
"mlie.livewiththepain",
"scorpio.makeanythingbuildable",
"scorpio.makeanythingcraftable",
"hol.stonebutcher",
"vanillaexpanded.vwems",
"detvisor.makeshiftweapons",
"zylle.mapdesigner",
"m00nl1ght.mappreview",
"supes.mapwideorbitaltradebeacon",
"pyrce.mass.graves",
"garethp.massreplant",
"leoltron.maxbuymaxsell",
"mlie.mechaniteaugmentation",
"horo.mechaniteforge.11",
"bart.mfr",
"chairheir.mechaniteplague",
"hydrologist.mechanitorcommandrelays",
"winggar.mechanoidnightvision",
"el.mow",
"imranfish.xmlextensions",
"teok25.mechanoidtweaks",
"fluffy.medicaltab",
"thepatchworkhoax.meleepsycasts",
"vanya.tools.bulkcarrier",
"mlie.mercerbackpacks",
"owlchemist.midsaversaver",
"uuugggg.miningpriority",
"taranchuk.modcontenttable",
"taranchuk.moderrorchecker",
"antaioz.modmedicinepatch",
"superniquito.modoptionssort",
"owlchemist.modulartablesandchairs",
"imranfish.moodmatters",
"giantspacehamster.moody",
"owlchemist.moonlight",
"atamandolinskaja.morecrashedshipparts",
"sineswiper.drugstats",
"mlie.suprememelee",
"avilmask.researchwhatever",
"petetimessix.researchreinvented",
"jaxe.rimhud",
"vanillaexpanded.skills",
"nmajask.moreexpertise",
"arquebus.moregenecomplexity",
"darkborderman.moretraitgenes",
"garryflowers.moreslaverystuff",
"jiopaba.fences",
"paradox.morevanillaturrets",
"arl85.animalsagefilter",
"morphsassorted.nocrushedbodies",
"mlie.mountainminer",
"nilchei.mountainouscavesystem",
"mud.tribalapparel",
"wit.namesgalore",
"netrve.dsgui",
"boundir.newgameplus",
"mlie.newlimbsneedstraining",
"unon.noburnmetal",
"doug.nojobauthors",
"xavior.nolazydoctors",
"jdalt.nomorelethaldamagethreshold",
"dvs.norandomrelations",
"avilmask.nonunopinata",
"vesper.notmyfault",
"vanillaexpanded.vbrewe",
"ltoddy.notaweapon",
"mehni.numbers",
"puremj.mjrimmods.nutrientpastedispensercommand",
"mlie.omnicoredrill",
"notfood.outfitted",
"packmulesextended.standard",
"owlchemist.packedsand",
"dubwise.dubsrimkit",
"vanillaexpanded.vaeaccessories",
"sgc.moreutilitypacks",
"vanillaexpanded.vwehw",
"turnovus.submod.backpacksarenotbelts",
"kongkim.panelsframes",
"kb.partyyourassoff",
"fed1splay.pawntargetfix",
"battlemage64.peoplecanchange",
"taranchuk.performanceoptimizer",
"wtfomgjohnny.perishable",
"owlchemist.permeableterrain",
"densevoid.hui.personalworkcat",
"owlchemist.wallutilities",
"owlchemist.perspectivebuildings",
"wemd.reinforceddoors",
"owlchemist.perspectivedoors",
"owlchemist.perspectiveeaves",
"owlchemist.perspectivetrees",
"telardo.pipettetool",
"dsw.plant24handsetableschedule",
"mlie.plantgrowthsync",
"mlie.pleasehaulperishables",
"usagirei.pocketsand",
"puremj.mjrimmods.potentialofyouth",
"simmin.powertools",
"uuugggg.replacestuff",
"haplo.powerswitch",
"etrt.practicalpemmican",
"thesepeople.ritualattachableoutcomes",
"mlie.preceptsandmemes",
"mlie.preceptsandmemesritualsmodule",
"tammybee.predatorhuntalert",
"pausbrak.predictabledeepdrills",
"lakuna.preparemoderately",
"mlie.prisoncommons",
"berryragepyre.prisonerrecreation",
"erdelf.prisonerransom",
"mlie.prisonersdonthavekeys",
"mlie.prisonersshouldfearturrets",
"mlie.prostheticnomissingbodyparts",
"longercfloor.proxyheat",
"mlie.qualityanddurability",
"rebelrabbit.qualitybionics",
"dninemfive.qualitycooldown",
"hatti.qualitybuilder",
"jgh.qualitybuilder",
"hatti.qualitysurgeon",
"reiquard.questexpirationcriticalalert",
"newcolonist.questthreatrebalance",
"sandy.rpgstyleinventory.avilmask.revamped",
"ratys.rtfuse",
"ratys.rtsolarflareshield",
"lvlvbnh.raisespecialistlimit",
"mastertea.randomplus",
"javierloustaunau.randyslist",
"brrainz.rangefinder",
"mlie.rats",
"n7huntsman.razorwire",
"mlie.regrowthexpandedworldgeneration",
"regrowth.botr.swampexpansion",
"mlie.turnonoffrepowered",
"mlie.repower.patchpack",
"mlie.readythoseguns",
"twistedpacifist.reasonablecomponents",
"bustedbunny.recylethis",
"hlx.reinforcedmechanoids2",
"freefolk.remedies",
"tikubonn.remotedetonator",
"mlie.repairworktype",
"stevezero.repairablegear",
"khamenman.armorracks",
"garethp.replacestuffcompatibility",
"ogliss.rescueejoinsplus",
"petetimessix.researchreinvented.steppingstones",
"scorpio.resourcedictionary",
"brrainz.reversecommands",
"geodesicdragon.rems",
"sindre0830.universum",
"sindre0830.rimnauts2",
"mlie.rimquest",
"spijkermenno.rimtwins",
"somewhereoutinspace.spaceports",
"river.tribal.mittens",
"river.tribal.shoes",
"telardo.romanceontherim",
"rooboid.birthmarksandblemishes",
"uuugggg.roomfood",
"mobius.royaltytweaks",
"arl85.ruinedbytemperaturefilter",
"roolo.runandgun",
"thelastbulletbender.saddleup",
"seohyeon.bestdrugpolicy",
"denev.saferpastedispenser",
"unreal.sameroomlovin",
"riiswa.scariacontamination",
"automatic.traderships",
"owlchemist.simplefx.smoke2",
"owlchemist.scatteredflames",
"owlchemist.scatteredstones",
"angryhornet.scenarioamender",
"roolo.searchanddestroy",
"mlie.seasonalweather",
"regrowth.botr.borealforestexpansion",
"regrowth.botr.temperateforestexpansion",
"regrowth.botr.tundraexpansion",
"owlchemist.seedspleaselite",
"mysticfelines.seedspleaselite.traderspatch",
"avilmask.selfdyeing",
"mlie.septictank",
"dninemfive.settlementdescription",
"victor.buymore",
"uuugggg.sharetheload",
"mlie.coalexpanded",
"vanillaexpanded.varme",
"shavius.vepatches",
"longercfloor.mortarfx",
"jamaicancastle.shoo",
"blues.shortcircuit",
"zilla.fertilityinfo",
"vanillaexpanded.vchemfuele",
"scpchemfuel.appliances",
"mysticfelines.simplefx.smoke2.patches",
"owlchemist.simplefx.splashes",
"owlchemist.simplefx.vapor",
"mrhydralisk.skilledslaughtering",
"bustedbunny.slaverebellionsimproved",
"hlx.ultratechalteredcarbon",
"sleepy068.morepermits",
"owlchemist.smartfarming",
"puremj.mjrimmods.smartmeditation",
"erdelf.minifyeverything",
"jaxe.smartminify",
"dhultgren.smarterconstruction",
"legodude17.smartdecon",
"hh.smokeleaf.lite",
"orion.therapy",
"weilbyte.snapout",
"nandonalt.snowytrees",
"dimonsever000.socialinterface.specific",
"sirrandoo.msf",
"mlie.somelikeitrotten",
"keve.sowingmanager",
"albion.sparklingworlds.addon.events",
"omgsplat.genesplit",
"vis.staticquality",
"scherub.stonecuttingextended",
"balistafreak.stopdropandroll",
"patientsomeone.storagetypecategories",
"mlie.stufflist",
"mlie.stuffmassmatters",
"simmin.stuffontables",
"xrushha.superbprosthetics",
"mlie.suppression",
"mlie.tabsorting",
"antaioz.tdiner",
"ucp.tabletopdecorations",
"divinorium.taintedrenaming",
"falconne.bwm",
"legodude17.htsb",
"sielfyr.tamewithkibble",
"balistafreak.standalonehotspring",
"icc.tov.hsa",
"neronix17.techtraversal",
"gwinnbleidd.researchtweaks",
"murmur.tcu",
"uuugggg.thepriceisright",
"hoboofserenity.thrumbohusbandry",
"cyanobot.toddlers",
"owlchemist.toggleablereadouts",
"owlchemist.toggleableshields",
"mosi.toxpopper",
"chaia.toxicfalloutprotectionsuit",
"inertialmage.toxicplants",
"meow.tradablemeals",
"meow.tradablestoneblocks",
"tradingcontrol.tad.rimworld.core",
"joseasoler.tradingoptions",
"wexman.trainingconsole1.2",
"katsudon.uibuttonretexture",
"marvinkosh.ughyougotme",
"owlchemist.undergroundpowerconduits",
"scorpio.shields",
"knight.useyourgun",
"ushanka.glitterworlduprising",
"ushanka.luciferiumexpansion",
"vaebootsandgloves.onskinpatch",
"shira.vaeapatch",
"argon.vcemp",
"seos.m.vfea.vault.s",
"seos.vfe.mechanoids.drones",
"seos.vfe.mechanoids.tab",
"spacemoth.vfeafunctionalhygiene",
"spacemoth.vfeaeternalvaults",
"bonible.vfemmakeshiftcombatrobots",
"gondragon.vfem.moatpatch",
"vanillaexpanded.vfefarming",
"shavius.oreno.vfe.dubsbadhygiene",
"vanillaexpanded.vfespacer",
"cixwow.vfespaceraddon",
"sflegion.vfeea.powerextract",
"hol.vfemcoaletc",
"bonible.machinesofwarvfem",
"tidal.morevanilla.textures",
"vfme.caravanpacks",
"sirvan.deepstorageretextured",
"haplo.miscellaneous.training",
"sirvan.misctrainingretexture",
"vanillaexpanded.basegeneration",
"mlie.vanillabooksexpandedexpanded",
"vanillaexpanded.vcookestews",
"vanillaexpanded.vee",
"lordkuper.fishingautomation",
"vanillaexpanded.vcefaddon",
"puremj.mjrimmods.vanillafixhaulafterslaughter",
"puremj.mjrimmods.vanillafixmortarshellloading",
"vanillaexpanded.vfeart",
"vanillaexpanded.vhe",
"vanillaexpanded.vieat",
"vanillaexpanded.viesas",
"vanillaexpanded.outposts",
"mlie.vanillaoutpostsexpandedexpandedranch",
"mrhydralisk.voeadditionaloutposts",
"vanillaexpanded.vplantsemore",
"vanillaracesexpanded.genie",
"vanillaracesexpanded.hussar",
"vanillaracesexpanded.saurid",
"vanillaexpanded.vanillasocialinteractionsexpanded",
"halituisamaricanous.vswbeam",
"halituisamaricanous.vswebolt",
"vse.perrypersistent",
"vanillaexpanded.vanillatradingexpanded",
"vanillaexpanded.vanillatraitsexpanded",
"mlie.variedbodysizes",
"vanillaexpanded.vwec",
"vanillaexpanded.vwenl",
"wvc.sergkart.itt.techtreepatch",
"wvc.sergkart.biotech.moremechanoidsworkmodes",
"bustedbunny.wakeup",
"ucp.walldecorations",
"zzz.wallvitalsmonitor",
"mlie.wallmountedbattery",
"victor.wallsaresolid",
"ogliss.wanderjoinsplus",
"ogliss.g223.wanderingcaravans",
"metalocif.warcasketpersonaweapons",
"mrblazzar.warmerpowerarmor",
"udderlyevelyn.waterfreezes",
"momo.updates.wateriscold",
"divinederivative.romance",
"mlie.wehadatrader",
"xaviien.weaponconditionmattersandsodoesquality",
"aelanna.weaponracks",
"bodlosh.weaponstats",
"kipsmods.adjustableabilitycooldowns",
"mlie.weaponssorter",
"gguake.ui.armorinfo",
"deivisis.weightprice",
"kikohi.whatsforsale",
"mehni.pickupandhaul",
"codeoptimist.jobsofopportunity",
"jelly.wildcultivation",
"miyamiya.wildplantpicker",
"mlie.wildfire",
"owlchemist.windows",
"dra.woodisnaturalfortrees",
"odeum.wmbp",
"bs.xenotypespawncontrol",
"mlie.zonetoschedule",
"papercrane1001.uvoee.trading",
"ap.enslavementprecept",
"sgc.quests",
"frozensnowfox.betterancientcomplexloot",
"frozensnowfox.bettercamploot",
"frozensnowfox.betterpawnlendingquest",
"frozensnowfox.betterspiketraps",
"owlchemist.ceilingutilities",
"nephlite.orbitaltradecolumn",
"frozensnowfox.efficientutilities",
"owlchemist.fridgeutilities",
"vanillaexpanded.ideo.dryads",
"frozensnowfox.frozensnowfoxtweaks",
"frozensnowfox.growableambrosia",
"vanillaexpanded.vfesecurity",
"frozensnowfox.nodefaultshelfstorage",
"fuu.punchattack",
"vanillaexpanded.vwetb",
"fuu.uncompromisingtribalfaction",
"fuu.usefulterror",
"harkon.veaddon.vanillaexpandedextraembrasures",
"calltradeships.kv.rw",
"kyd.drugsafetyfilters",
"kyd.evenmorefoodfilters",
"kittahkhan.grazeup",
"derekbickley.ltocolonygroupsfinal",
"dbl.textureaddons.lts.continued",
"limetreesnake.maintenance",
"mlie.concrete",
"khitrir.gunlinksaffectmortars",
"sr.modrimworld.factionalwar",
"sr.modrimworld.raidextension",
"rabisquare.realisticoregeneration",
"syrchalis.bulletcasings",
"syrchalis.doormats",
"syrchalis.metallicbatteries",
"syrchalis.setupcamp",
"smuffle.harvestorganspostmortem",
"heremeus.medicaldissection",
"frozensnowfox.complexjobs",
"ultraemailman.moremechanitormechs",
"wemd.fueledheaters",
"mlie.xndprofitableweapons",
"mlie.xndwatermilltweaks",
"pphhyy.toxweapons",
"sineswiper.xenobionicpatcher",
"krkr.rocketman",
"ludeon.rimworld",
"ludeon.rimworld.royalty",
"ludeon.rimworld.ideology",
"ludeon.rimworld.biotech",
        };
        public static HashSet<Type> defTypesToIgnore = new HashSet<Type>
        {
            typeof(FleckDef),
            typeof(Verse.BodyPartDef),
            typeof(Verse.ThingCategoryDef),
            typeof(Verse.BodyDef),
            typeof(RimWorld.HistoryEventDef),
            typeof(Verse.DesignatorDropdownGroupDef),
            typeof(Verse.ToolCapacityDef),
            typeof(Verse.BodyPartGroupDef),
            typeof(Verse.GeneCategoryDef),
            typeof(RimWorld.ConceptDef),
            typeof(RimWorld.ResearchTabDef),
            typeof(Verse.SpecialThingFilterDef),
            typeof(Verse.KeyBindingCategoryDef),
            typeof(Verse.StyleCategoryDef),
            typeof(Verse.KeyBindingDef),
            typeof(RimWorld.StatCategoryDef),
            typeof(Verse.TerrainAffordanceDef),
            typeof(RimWorld.GoodwillSituationDef),
        };

        [DebugOutput]
        public static void ModContentDefs()
        {
            var defs = new HashSet<Def>();
            foreach (var subType in typeof(Def).AllSubclasses())
            {
                if (subType.Assembly == typeof(Game).Assembly && !defTypesToIgnore.Contains(subType))
                {
                    var subDefs = ((IList)GenGeneric.InvokeStaticMethodOnGenericType(typeof(DefDatabase<>), subType, "get_AllDefsListForReading")).Cast<Def>();
                    defs.AddRange(subDefs.Where(x => x.modContentPack?.IsOfficialMod is false));
                    //&& modsToIgnore.Any(modToIgnore => modToIgnore.ToLower() == x.modContentPack?.PackageIdPlayerFacing.ToLower()) is false));
                }
            }
            defs.RemoveWhere(x => SkipDef(x));

            bool SkipDef(Def def)
            {
                if (defTypesToIgnore.Contains(def.GetType()))
                    return true;
                if (def.label.NullOrEmpty()) 
                    return true;
                if (def is ThingDef thingDef && (thingDef.IsBlueprint || thingDef.IsFrame || 
                    thingDef.mote != null || thingDef.IsCorpse || thingDef.IsMeat || thingDef.projectile != null))
                {
                    return true;
                }
                return false;
            }
            List<TableDataGetter<Def>> list = new List<TableDataGetter<Def>>
            {
                new TableDataGetter<Def>("defType", (Def d) => d.GetType().FullName),
                new TableDataGetter<Def>("defName", (Def d) => d.defName),
                new TableDataGetter<Def>("label", (Def d) => d.label + " - " + d.GetType().Name),
                new TableDataGetter<Def>("mod", (Def d) => d.modContentPack?.Name),
                new TableDataGetter<Def>("mod packageID", (Def d) => d.modContentPack?.PackageIdPlayerFacing),
                new TableDataGetter<Def>("description", (Def d) => d.description),
            };
            DebugTables.MakeTablesDialog(defs, list.ToArray());
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
            if (contentData.NullOrEmpty())
            {
                tmpDefs = defs.ToHashSet();
                List<string> output = new List<string>();
                foreach (var data in categoryValidators)
                {
                    OutputAmount(output, data.Key, data.Value);
                }
                contentData = "(Found defs: " + foundDefs.Sum(x => x.Value) + ") - " + string.Join(", ", output);
            }
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
