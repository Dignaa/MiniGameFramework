using MiniGameFramework.Configuration;
using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects.Creatures;
using MiniGameFramework.Models.Items;
using MiniGameFramework.Models.Objects;
using MiniGameTest.Creatures;

string path = "../../../../config.xml";
//Setup configuration
IConfig config = new Config();
ILogger logger = config.ConfigureFromFile(path);

//Create world instance
World world = World.CreateInstance(null, null, null, null, logger);

//Create test positions
Position position1 = new Position(12, 0);
Position position2 = new Position(14, 0);

//Create test items
IWorldObject item1 = new DefenceObject("Shield", "Protection", 25, position1);
IWorldObject item2 = new AttackObject("Hammer", "Bang", 30, 2, position2);
IWorldObject item3 = new AttackObject("Sword", "Auch", 35, 3, position1);

//Create test game objects/add positions and inventories
var chest = new LootableCompositeObject("Chest", "Chest full with things", position1, logger);
chest.Add(item1);
chest.Add(item2);

LootableCompositeObject object1 = new LootableCompositeObject("box", "A very nice box",  position2, logger);
object1.Add(item3);
IWorldObject object2 = new RemovableObject("Tree", "Maple", position2, logger);
ModifiedCreature creature1 = new ModifiedCreature("Bartol",  logger, position2, 15, 190);
ModifiedCreature creature2 = new ModifiedCreature("Tea", logger, position1, 20, 180);

//Create list of game objects for the world
world.AddObjectToWorld(object1);
world.AddObjectToWorld(object2);
world.AddObjectToWorld(chest);
world.AddCreatureToWorld(creature1);
world.AddCreatureToWorld(creature2);



Console.WriteLine("******** ALL OBJECTS IN THE WORLD ***********");
world.WorldObjects.ForEach(o => Console.WriteLine(o.Name));
world.Creatures.ForEach(o => Console.WriteLine(o.Name));

//Test hit
(List<IWorldObject>? looted, List<IWorldObject>? removed) hitItems = creature2.Hit(position1);

(List<IWorldObject>? looted, List<IWorldObject> ?removed) hitItems2 = creature1.Hit(position2);

Console.WriteLine("\n*** OBJECTS RETURNED FROM HITS ****");
if(hitItems.looted != null)
    hitItems.looted.ForEach(i => Console.WriteLine(i.Name));
if (hitItems.removed != null)
    hitItems.removed.ForEach(i=> Console.WriteLine(i.Name));

Console.WriteLine("\n******** ALL OBJECTS IN WORLD OBJECTS AFTER HIT ***********");
world.WorldObjects.ForEach(o => Console.WriteLine(o.Name));
world.Creatures.ForEach(o => Console.WriteLine(o.Name));


Console.WriteLine("\n*******CREATURES ITEMS IN INVENTORY AFTER HIT***********");
creature1.Inventory.Items.ForEach(i=> Console.WriteLine(i.Name));
creature2.Inventory.Items.ForEach(i => Console.WriteLine(i.Name));

Console.WriteLine("\n****TEST HITS*****");

creature2.PrimaryAttackObject = (AttackObject)item2;
creature1.Hit(creature2);
creature2.Move(new Position(1, 0));

while ((creature1.IsDead == false) && (creature2.IsDead == false))
{
    creature1.Hit(creature2);
    Console.WriteLine($"Creature name: {creature2.Name}     Remaining health: {creature2.Health}    Current state: {creature2.State}");
    creature2.Hit(creature1);
    Console.WriteLine($"Creature name: {creature1.Name}  Remaining health: {creature1.Health}    Current state: {creature1.State}");

}