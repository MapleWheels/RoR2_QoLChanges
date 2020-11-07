using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Extensions.Configuration;

namespace RoR2QoLRewrite.Configuration
{
    public interface IConfigBase
    {
        ConfigData<bool> Enabled { get; set; }
    }
}
