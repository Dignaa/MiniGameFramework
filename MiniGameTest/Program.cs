using MiniGameFramework.Configuration;
using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects.Creatures;
using MiniGameFramework.Models.Items;
using MiniGameFramework.Models.Objects;

string path = "../../../../config.xml";
//Setup configuration
Logger logger = Config.ConfigureLogger(path);
Config.ConfigureFromFile(path, logger);

//Create world instance
World world = World.CreateInstance(null, null, null, null, logger);



//Create test positions
Position position1 = new Position(12, 0);
Position position2 = new Position(13, 0);
Position position3 = new Position(15, 10);
Position position4 = new Position(12, 5);

//Create test items
IWorldObject item1 = new DefenceObject("Shield", "Protection", 25, position1);
IWorldObject item2 = new AttackObject("Hammer", "Bang", 20, 2, position2);

//Create test game objects/add positions and inventories

var chest = new LootableCompositeObject("Chest", "Chest full with things", position1, logger);

chest.Add(item1);
chest.Add(item2);

IWorldObject object1 = new LootableCompositeObject("box", "A very nice box",  position1, logger);
IWorldObject object2 = new RemovableObject("Tree", "Maple", position3, logger);
Creature creature1 = new Creature("Bartol",  logger, position2, 15, 200);
Creature creature2 = new Creature("Tea", logger, position1, 20, 170);

//Create list of game objects for the world
world.AddObjectToWorld(object1);
world.AddObjectToWorld(object2);
world.AddObjectToWorld(chest);
world.AddCreatureToWorld(creature1);
world.AddCreatureToWorld(creature2);

Console.WriteLine("******** ALL OBJECTS ***********");
world.WorldObjects.ForEach(o => Console.WriteLine(o.Name));
world.Creatures.ForEach(o => Console.WriteLine(o.Name));
Console.WriteLine("*****************************");
//Test hit
(List<IWorldObject>? looted, List<IWorldObject>? removed) hitItems = creature1.Hit(position1);

(List<IWorldObject>? looted, List<IWorldObject> ?removed) hitItems2 = creature1.Hit(position3);

Console.WriteLine("*** Returned from hit ****");

if(hitItems.looted != null)
    hitItems.looted.ForEach(i => Console.WriteLine(i.Name));
if (hitItems.removed != null)
    hitItems.removed.ForEach(i=> Console.WriteLine(i.Name));
Console.WriteLine( position1.GetDistance(position1,position4));

Console.WriteLine("******** OBJECTS AFTER HIT ***********");
world.WorldObjects.ForEach(o => Console.WriteLine(o.Name));
world.Creatures.ForEach(o => Console.WriteLine(o.Name));


Console.WriteLine("*******Creatures Items after fight***********");
creature1.Inventory?.Items.ForEach(i=> Console.WriteLine(i.Name));

while((creature1.IsDead == false) && (creature2.IsDead == false))
{
    creature1.Hit(creature2);
    Console.WriteLine(creature2.Name + creature2.Health + creature2.State);
    creature2.Hit(creature1);
    Console.WriteLine(creature1.Name + creature1.Health + creature1.State);
}