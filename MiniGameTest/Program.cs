using MiniGameFramework.Configuration;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects;
using MiniGameFramework.Models.Items;


//Setup configuration
Config configuration = new Config();
configuration.ConfigureFromFile("../../../../config.xml");

//Create test items
DefenceItem item1 = new DefenceItem("Shield", "Protection", 25);
AttackItem item2 = new AttackItem("Hammer", "Bang", 20, 2);
List<Item> items = new List<Item>() { item1, item2};

//Create test positions
Position position1 = new Position(12, 0);
Position position2 = new Position(null, null);
Position position3 = new Position(15, 0);
Position position4 = new Position(12, 0);

//Create test game objects/add positions and inventories
WorldObject object1 = new WorldObject("Chest", true, true, items, position1);
WorldObject object2 = new WorldObject("Tree", false, true, items, position3);
Creature creature1 = new Creature("Bartol", position2, 15, 200, new List<Item>());

//Create list of game objects for the world
List<GameObject> objects = new List<GameObject>() { object1, object2, creature1 };

//Create world instance
World world = World.CreateInstance(null, null, objects);

//Test hit
HitResult hitItems = creature1.Hit(position3);

if(hitItems.HitItems != null)
    hitItems.HitItems.ForEach(i => Console.WriteLine(i.Name));

if (hitItems.HitObject != null)
    Console.WriteLine(hitItems.HitObject.Name);

HitResult hitItems2 = creature1.Hit(position1);

if (hitItems.HitItems != null)
    hitItems2.HitItems?.ForEach(i => Console.WriteLine(i.Name));

if (hitItems.HitObject != null)
    Console.WriteLine(hitItems2.HitObject?.Name);

Console.WriteLine(position1.GetDistance(position1, position2));