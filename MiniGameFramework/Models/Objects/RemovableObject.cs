using MiniGameFramework.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniGameFramework.Models.Objects
{
    public class RemovableObject : IWorldObject
    {
        private ILogger _logger;
        public RemovableObject(string name, string description, Position position, ILogger logger) 
        {
            Name = name;
            Description = description;
            ObjectPosition = position;
            _logger= logger;        
        }
        public string Name { get; set; }
        public string? Description { get; set ; }
        public Position? ObjectPosition { get; set ; }

        /// <summary>
        /// Picks an object if it is removable
        /// Removes it from world
        /// </summary>
        /// <returns>World object</returns>
        public IWorldObject? Pick()
        {
            World? world = World._instance;
            if (world != null)
                world.RemoveObjectFromWorld(this);
            else
                _logger.Log(TraceEventType.Error, "Cannot pick an object as world is not created");
            
            return this;
        }
    }
}
