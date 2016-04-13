using UnityEngine;
using System.Collections;

namespace Bigfoot
{
    /// <summary>
    /// Level config Interface. Provides a common interface for every game that needs to send information when loading a level in another scene. 
    /// You should inherit from this Interface and add whatever you need for your game.
    /// The General Event System will automaticlly recognize it and will send it if using LoadLevelWithConfig
    /// </summary>
    public interface ISceneDTO
    {

    }
}