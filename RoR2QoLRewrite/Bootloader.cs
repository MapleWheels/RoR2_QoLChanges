﻿using BepInEx;
using BepInEx.Extensions.Configuration;

using R2API;
using R2API.Networking;
using R2API.Utils;

using RoR2QoLRewrite.Configuration;
using RoR2QoLRewrite.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInDependency(BepInEx.Extensions.LibraryInfo.BepInDependencyID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod)]
    [BepInPlugin(ConVars.PackageName, ConVars.PluginName, ConVars.Version)]
    [R2APISubmoduleDependency(nameof(BuffAPI), nameof(ItemAPI), nameof(ResourcesAPI), nameof(NetworkingAPI))]
    public class Bootloader : BaseUnityPlugin
    {
        

        void Awake()
        {
        }

        void Start()
        {
        }
    }
}
