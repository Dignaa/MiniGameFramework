using MiniGameFramework.Configuration;
using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects;
using MiniGameFramework.Models.Items;

string path = "../../../../config.xml";
//Setup configuration
Logger logger = Config.ConfigureLogger(path);
Config.ConfigureFromFile(path, logger);

//Create world instance
World world = World.CreateInstance(null, null, null, logger);

//Create test items
DefenceItem item1 = new DefenceItem("Shield", "Protection", 25);
AttackItem item2 = new AttackItem("Hammer", "Bang", 20, 2);
Inventory inventory1 = new Inventory( new List<Item>() { item1, item2 });

//Create test positions
Position position1 = new Position(12, 0);
Position position2 = new Position(15, 10);
Position position3 = new Position(15, 0);
Position position4 = new Position(12, 0);

//Create test game objects/add positions and inventories
WorldObject object1 = new WorldObject("Chest", true, true,  position1, inventory1, logger);
WorldObject object2 = new WorldObject("Tree", false, false, position3, inventory1, logger);
Creature creature1 = new Creature("Bartol", position2, 15, 200, new Inventory(), null, null, logger);

//Create list of game objects for the world
object1.AddToWorld();
object2.AddToWorld();
creature1.AddToWorld();

Console.WriteLine("******** ALL OBJECTS ***********");
World._instance?.GameObjects.ForEach(o => Console.WriteLine(o.Name));
Console.WriteLine("*****************************");
//Test hit
HitResult hitItems = creature1.Hit(position3);

if(hitItems.HitReturnItems != null)
    hitItems.HitReturnItems.ForEach(i => Console.WriteLine(i.Name));

if (hitItems.HitReturnObject != null)
    Console.WriteLine(hitItems.HitReturnObject.Name);

HitResult hitItems2 = creature1.Hit(position1);

if (hitItems.HitReturnItems != null)
    hitItems2.HitReturnItems?.ForEach(i => Console.WriteLine(i.Name));

if (hitItems.HitReturnObject != null)
    Console.WriteLine(hitItems2.HitReturnObject?.Name);

Console.WriteLine( position1.GetDistance(position1,position4));

Console.WriteLine("******** OBJECTS AFTER HIT ***********");
World._instance?.GameObjects.ForEach(o => Console.WriteLine(o.Name));

Console.WriteLine("*******Creatures Items after fight***********");
creature1.Inventory?.Items.ForEach(i=> Console.WriteLine(i.Name));