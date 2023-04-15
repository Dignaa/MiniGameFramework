// See https://aka.ms/new-console-template for more information


using MiniGameFramework.Configuration;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects;
using MiniGameFramework.Models.Items;
using System.Runtime.CompilerServices;

Config configuration = new Config();
configuration.ConfigureFromFile("../../../../config.xml");

DefenceItem item1 = new DefenceItem("Shield", "Protection", 25);
AttackItem item2 = new AttackItem("Hammer", "Bang", 20, 2);
List<Item> items = new List<Item>() { item1, item2};

Position position1 = new Position(12, 0);
Position position2 = new Position(15, 10);
Position position3 = new Position(15, 0);

WorldObject object1 = new WorldObject("Chest", true, true, items, position1);
WorldObject object2 = new WorldObject("Tree", false, true, items, position3);
Creature creature1 = new Creature("Bartol", position2, 15, 200, new List<Item>());

List<GameObject> objects = new List<GameObject>() { object1, object2, creature1 };


World world = World.CreateInstance(1000, 1000, objects);

HitResult hitItems = creature1.Hit(position2);

