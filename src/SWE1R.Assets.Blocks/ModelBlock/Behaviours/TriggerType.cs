// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.Behaviours
{
    /// <summary>
    /// Number defining different trigger types.
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/everalert/swe1r-decomp/blob/4dfc136cfc46356fe66753c2fea5c068f86c5634/idapro/notes/Entity_Trig.txt#L78">
    ///       github.com - everalert/swe1r-decomp - Entity_Trig.txt</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public enum TriggerType : short
    {
        /// <summary>
        /// Shattering ice.
        /// <para>Planets: Ando Prime</para>
        /// </summary>
        ShatteringIce = 100,
        /// <summary>
        /// Rotating door. Rotates the door from 2nd trigger onward.
        /// <para>Tracks: Vengeance (Oovo IV), Executioner (Oovo IV)</para>
        /// </summary>
        RotatingDoor = 101,
        /// <summary>
        /// Shattering asteroid.
        /// <para>Planets: Oovo IV</para>
        /// </summary>
        ShatteringAsteroid = 102,
        /// <summary>
        /// Tent entry opening.
        /// <para>Planets: Ando Prime</para>
        /// </summary>
        TentEntryOpening = 103,
        /// <summary>
        /// Death beam.
        /// <para>Tracks: Vengeance (Oovo IV)</para>
        /// </summary>
        DeathBeam = 104,
        /// <summary>
        /// Falling pillar / camera shake.
        /// <para>Tracks: Andobi Mountain Run (Oovo IV)</para>
        /// </summary>
        FallingPillarCameraShake_AndobiMountainRun = 105,
        /// <summary>
        /// Tusken raiders.
        /// <para>Tracks: The Gauntlet (Oovo IV)</para>
        /// </summary>
        TuskenRaiders_TheGauntlet = 106,
        /// <summary>
        /// Unknown effect. (Red carpet)
        /// <para>Tracks: Executioner (Oovo IV)</para>
        /// </summary>
        RedCarpetUnkEffect = 107,
        /// <summary>
        /// Slalom flag.
        /// <para>Planets: Ando Prime</para>
        /// </summary>
        SlalomFlag = 108,
        /// <summary>
        /// Unused.
        /// Same as <see cref="ShatteringRock"/>.
        /// </summary>
        Unused_201 = 201,
        /// <summary>
        /// Shattering rock.
        /// <para>Tracks:
        ///   Boonta Training Course (Tatooine),
        ///   Executioner (Oovo IV),
        ///   The Boonta Classic (Tatooine),
        ///   Inferno (Baroonda)
        /// </para>
        /// </summary>
        ShatteringRock = 202,
        /// <summary>
        /// Tusken raiders.
        /// <para>Tracks: The Boonta Classic (Tatooine)</para>
        /// </summary>
        TuskenRaiders_TheBoontaClassic = 203,
        /// <summary>
        /// Ballooncraft.
        /// <para>Planets: Tatooine</para>
        /// </summary>
        Ballooncraft = 208,
        /// <summary>
        /// Flame.
        /// <para>Tracks: Grabvine Gateway (Baroonda) (cheeseland area), ...</para>
        /// </summary>
        Flame = 211,
        /// <summary>
        /// Shattering tree branch.
        /// <para>Planets: Baroonda</para>
        /// </summary>
        ShatteringTreeBranch = 212,
        /// <summary>
        /// Shortcut opening / camera shake.
        /// <para>Tracks: Grabvine Gateway (Baroonda)</para>
        /// </summary>
        ShortcutOpeningCameraShake = 213,
        /// <summary>
        /// Unknown effect. (Methane lake)
        /// <para>Tracks: Malastare 100 (Malastare)</para>
        /// </summary>
        MethaneLakeUnkEffect = 301,
        /// <summary>
        /// Animate trams.
        /// <para>Tracks: Aquilaris Classic (Aquilaris)</para>
        /// </summary>
        AnimateTrams = 304,
        /// <summary>
        /// Earthquake.
        /// <para>Tracks: Bumby's Breakers (Aquilaris)</para>
        /// </summary>
        Earthquake = 306,
        /// <summary>
        /// Doors. Moves doors from 2nd. trigger onward.
        /// <para>Tracks: Aquilaris Classic (Aquilaris)</para>
        /// </summary>
        MoveDoorsFrom2ndTriggerOnward = 307,
        /// <summary>
        /// Unknown effect.
        /// Top of the ramp leading to doors, not door trigger. (Maybe fish animation?)
        /// <para>Tracks: Aquilaris Classic (Aquilaris)</para>
        /// </summary>
        Unknown_308 = 308,
        /// <summary>
        /// Digger.
        /// <para>Planets: Mon Gazza</para>
        /// </summary>
        Digger = 310,
        /// <summary>
        /// Falling pillar / camera shake.
        /// <para>Tracks: Dug Derby (Malastare)</para>
        /// </summary>
        FallingPillarCameraShake_DugDerby = 312,
        /// <summary>
        /// Methane gas cloud spawner.
        /// <para>Tracks: Dug Derby (Malastare)</para>
        /// </summary>
        MethaneGasCloudSpawner = 314,
        /// <summary>
        /// Breaking the gold stuff while going over lava.
        /// <para>Tracks: Inferno (Baroonda)</para>
        /// </summary>
        BreakingTheGoldStuffWhileGoingOverLava = 501,
        /// <summary>
        /// Unknown effect.
        /// At the hairpin after the first straight.
        /// <para>Tracks: Inferno (Baroonda)</para>
        /// </summary>
        Unknown_503 = 503,
        /// <summary>
        /// Unused.
        /// Respawns pod at spline without death, and shows notification with garbage text.
        /// </summary>
        Unused_900 = 900,
    }
}
