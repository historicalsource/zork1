namespace ZorkSharp.Data;

using ZorkSharp.Core;
using ZorkSharp.World;

/// <summary>
/// Builds the game world data
/// This is a representative sample of the Zork I world
/// </summary>
public class WorldBuilder
{
    private readonly GameWorld _world;

    public WorldBuilder(GameWorld world)
    {
        _world = world;
    }

    public void BuildWorld()
    {
        CreateRooms();
        CreateObjects();
        PlaceObjects();
    }

    private void CreateRooms()
    {
        // West of House
        _world.AddRoom(new Room
        {
            Id = "WEST-OF-HOUSE",
            Name = "West of House",
            Description = "You are standing in an open field west of a white house, with a boarded front door.",
            LongDescription = "You are standing in an open field west of a white house, with a boarded front door.\nThere is a small mailbox here.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "NORTH-OF-HOUSE" },
                [Direction.South] = new RoomExit { DestinationRoomId = "SOUTH-OF-HOUSE" },
                [Direction.West] = new RoomExit { DestinationRoomId = "FOREST-1" },
                [Direction.East] = new RoomExit { CustomMessage = "The door is boarded and you can't remove the boards." }
            }
        });

        // North of House
        _world.AddRoom(new Room
        {
            Id = "NORTH-OF-HOUSE",
            Name = "North of House",
            Description = "You are facing the north side of a white house.",
            LongDescription = "You are facing the north side of a white house. There is no door here, and all the windows are boarded up. To the north a narrow path winds through the trees.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "FOREST-2" },
                [Direction.South] = new RoomExit { DestinationRoomId = "WEST-OF-HOUSE" },
                [Direction.East] = new RoomExit { DestinationRoomId = "BEHIND-HOUSE" },
                [Direction.West] = new RoomExit { DestinationRoomId = "WEST-OF-HOUSE" }
            }
        });

        // South of House
        _world.AddRoom(new Room
        {
            Id = "SOUTH-OF-HOUSE",
            Name = "South of House",
            Description = "You are facing the south side of a white house.",
            LongDescription = "You are facing the south side of a white house. There is no door here, and all the windows are boarded.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "WEST-OF-HOUSE" },
                [Direction.South] = new RoomExit { DestinationRoomId = "FOREST-3" },
                [Direction.East] = new RoomExit { DestinationRoomId = "BEHIND-HOUSE" },
                [Direction.West] = new RoomExit { DestinationRoomId = "WEST-OF-HOUSE" }
            }
        });

        // Behind House
        _world.AddRoom(new Room
        {
            Id = "BEHIND-HOUSE",
            Name = "Behind House",
            Description = "You are behind the white house.",
            LongDescription = "You are behind the white house. A path leads into the forest to the east. In one corner of the house there is a small window which is slightly ajar.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "NORTH-OF-HOUSE" },
                [Direction.South] = new RoomExit { DestinationRoomId = "SOUTH-OF-HOUSE" },
                [Direction.East] = new RoomExit { DestinationRoomId = "CLEARING" },
                [Direction.West] = new RoomExit { DestinationRoomId = "NORTH-OF-HOUSE" },
                [Direction.In] = new RoomExit { DestinationRoomId = "KITCHEN" }
            }
        });

        // Kitchen
        _world.AddRoom(new Room
        {
            Id = "KITCHEN",
            Name = "Kitchen",
            Description = "You are in the kitchen of the white house.",
            LongDescription = "You are in the kitchen of the white house. A table seems to have been used recently for the preparation of food. A passage leads to the west and a dark staircase can be seen leading upward. A dark chimney leads down and to the east is a small window which is open.",
            Flags = RoomFlags.Light,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.West] = new RoomExit { DestinationRoomId = "LIVING-ROOM" },
                [Direction.Up] = new RoomExit { DestinationRoomId = "ATTIC" },
                [Direction.Down] = new RoomExit { CustomMessage = "You would need to be smaller to fit down the chimney." },
                [Direction.Out] = new RoomExit { DestinationRoomId = "BEHIND-HOUSE" }
            }
        });

        // Living Room
        _world.AddRoom(new Room
        {
            Id = "LIVING-ROOM",
            Name = "Living Room",
            Description = "You are in the living room.",
            LongDescription = "You are in the living room. There is a doorway to the east, a wooden door with strange gothic lettering to the west, which appears to be nailed shut, a trophy case, and a large oriental rug in the center of the room.",
            Flags = RoomFlags.Light,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "KITCHEN" },
                [Direction.West] = new RoomExit { CustomMessage = "The door is nailed shut." },
                [Direction.Down] = new RoomExit { DestinationRoomId = "CELLAR" }
            }
        });

        // Cellar
        _world.AddRoom(new Room
        {
            Id = "CELLAR",
            Name = "Cellar",
            Description = "You are in a dark and damp cellar.",
            LongDescription = "You are in a dark and damp cellar with a narrow passageway leading north, and a crawlway to the south. On the west is the bottom of a steep metal ramp which is unclimbable.",
            Flags = RoomFlags.None, // Dark room!
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "TROLL-ROOM" },
                [Direction.South] = new RoomExit { DestinationRoomId = "MAZE-1" },
                [Direction.Up] = new RoomExit { DestinationRoomId = "LIVING-ROOM" }
            }
        });

        // Attic
        _world.AddRoom(new Room
        {
            Id = "ATTIC",
            Name = "Attic",
            Description = "This is the attic.",
            LongDescription = "This is the attic. The only exit is a stairway leading down.",
            Flags = RoomFlags.Light,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.Down] = new RoomExit { DestinationRoomId = "KITCHEN" }
            }
        });

        // Troll Room
        _world.AddRoom(new Room
        {
            Id = "TROLL-ROOM",
            Name = "Troll Room",
            Description = "This is a small room with passages to the east and south.",
            LongDescription = "This is a small room with passages to the east and south and a forbidding hole leading west. Bloodstains and deep scratches (perhaps made by an axe) mar the walls.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "EAST-WEST-PASSAGE" },
                [Direction.South] = new RoomExit { DestinationRoomId = "CELLAR" },
                [Direction.West] = new RoomExit { DestinationRoomId = "MAZE-1" }
            }
        });

        // East West Passage
        _world.AddRoom(new Room
        {
            Id = "EAST-WEST-PASSAGE",
            Name = "East-West Passage",
            Description = "This is a narrow east-west passageway.",
            LongDescription = "This is a narrow east-west passageway. There is a narrow stairway leading down at the north end of the room.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "ROUND-ROOM" },
                [Direction.West] = new RoomExit { DestinationRoomId = "TROLL-ROOM" },
                [Direction.Down] = new RoomExit { DestinationRoomId = "CHASM" }
            }
        });

        // Round Room
        _world.AddRoom(new Room
        {
            Id = "ROUND-ROOM",
            Name = "Round Room",
            Description = "This is a circular stone room.",
            LongDescription = "This is a circular stone room with passages in all directions. Several of them have unfortunately been blocked by cave-ins.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "NORTH-SOUTH-PASSAGE" },
                [Direction.South] = new RoomExit { DestinationRoomId = "LOUD-ROOM" },
                [Direction.East] = new RoomExit { DestinationRoomId = "ENGRAVINGS-CAVE" },
                [Direction.West] = new RoomExit { DestinationRoomId = "EAST-WEST-PASSAGE" }
            }
        });

        // Add a few forest rooms
        _world.AddRoom(new Room
        {
            Id = "FOREST-1",
            Name = "Forest",
            Description = "This is a forest.",
            LongDescription = "This is a forest, with trees in all directions. To the east, there appears to be sunlight.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "WEST-OF-HOUSE" },
                [Direction.North] = new RoomExit { DestinationRoomId = "FOREST-2" },
                [Direction.South] = new RoomExit { DestinationRoomId = "FOREST-3" },
                [Direction.West] = new RoomExit { DestinationRoomId = "FOREST-1" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "FOREST-2",
            Name = "Forest",
            Description = "This is a dimly lit forest.",
            LongDescription = "This is a dimly lit forest, with large trees all around.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.South] = new RoomExit { DestinationRoomId = "NORTH-OF-HOUSE" },
                [Direction.North] = new RoomExit { DestinationRoomId = "FOREST-2" },
                [Direction.East] = new RoomExit { DestinationRoomId = "FOREST-2" },
                [Direction.West] = new RoomExit { DestinationRoomId = "FOREST-1" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "FOREST-3",
            Name = "Forest",
            Description = "This is a dimly lit forest.",
            LongDescription = "This is a dimly lit forest, with large trees all around.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "SOUTH-OF-HOUSE" },
                [Direction.South] = new RoomExit { DestinationRoomId = "FOREST-3" },
                [Direction.East] = new RoomExit { DestinationRoomId = "FOREST-3" },
                [Direction.West] = new RoomExit { DestinationRoomId = "FOREST-1" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "CLEARING",
            Name = "Clearing",
            Description = "You are in a clearing.",
            LongDescription = "You are in a clearing, with a forest surrounding you on all sides. A path leads south.",
            Flags = RoomFlags.Light | RoomFlags.Outside,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.South] = new RoomExit { DestinationRoomId = "BEHIND-HOUSE" },
                [Direction.North] = new RoomExit { DestinationRoomId = "FOREST-2" },
                [Direction.East] = new RoomExit { DestinationRoomId = "FOREST-2" },
                [Direction.West] = new RoomExit { DestinationRoomId = "FOREST-2" }
            }
        });

        // Maze and other rooms
        _world.AddRoom(new Room
        {
            Id = "MAZE-1",
            Name = "Maze",
            Description = "You are in a maze of twisty little passages, all alike.",
            LongDescription = "You are in a maze of twisty little passages, all alike.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "CELLAR" },
                [Direction.South] = new RoomExit { DestinationRoomId = "MAZE-1" },
                [Direction.East] = new RoomExit { DestinationRoomId = "MAZE-1" },
                [Direction.West] = new RoomExit { DestinationRoomId = "MAZE-1" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "CHASM",
            Name = "Chasm",
            Description = "You are on the edge of a chasm.",
            LongDescription = "You are on the east edge of a chasm, the bottom of which cannot be seen. A narrow passage goes north, and the path you are on continues to the east.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "GALLERY" },
                [Direction.East] = new RoomExit { DestinationRoomId = "RESERVOIR" },
                [Direction.Up] = new RoomExit { DestinationRoomId = "EAST-WEST-PASSAGE" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "GALLERY",
            Name = "Gallery",
            Description = "This is an art gallery.",
            LongDescription = "This is an art gallery. Most of the paintings which were here have been stolen by vandals with exceptional taste. The vandals left through either the north or west exits.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "MAZE-1" },
                [Direction.South] = new RoomExit { DestinationRoomId = "CHASM" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "RESERVOIR",
            Name = "Reservoir",
            Description = "You are in a large reservoir.",
            LongDescription = "You are in a large reservoir. A path goes west. A white stone in the center of the reservoir is visible above the water.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.West] = new RoomExit { DestinationRoomId = "CHASM" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "NORTH-SOUTH-PASSAGE",
            Name = "North-South Passage",
            Description = "This is a north-south passage.",
            LongDescription = "This is a high north-south passage, which forks to the northeast.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "CHASM" },
                [Direction.South] = new RoomExit { DestinationRoomId = "ROUND-ROOM" },
                [Direction.NorthEast] = new RoomExit { DestinationRoomId = "GALLERY" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "LOUD-ROOM",
            Name = "Loud Room",
            Description = "This is a large room.",
            LongDescription = "This is a large room with a ceiling which cannot be detected from the ground. There is a narrow passage from east to west and a stone stairway leading upward. The room is extremely noisy. In fact, it is difficult to hear yourself think.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "ROUND-ROOM" },
                [Direction.East] = new RoomExit { DestinationRoomId = "ROUND-ROOM" },
                [Direction.West] = new RoomExit { DestinationRoomId = "DAMP-CAVE" },
                [Direction.Up] = new RoomExit { DestinationRoomId = "DEEP-CANYON" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "DAMP-CAVE",
            Name = "Damp Cave",
            Description = "This is a damp cave.",
            LongDescription = "This is a small cave. The walls are damp and water is dripping from the ceiling. The only exits are to the south and to the east.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "LOUD-ROOM" },
                [Direction.South] = new RoomExit { DestinationRoomId = "WHITE-CLIFFS-BEACH" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "WHITE-CLIFFS-BEACH",
            Name = "White Cliffs Beach",
            Description = "You are on a beach.",
            LongDescription = "You are on a rocky, narrow strip of beach beside the Cliffs. A narrow path leads north along the shore.",
            Flags = RoomFlags.Light,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.North] = new RoomExit { DestinationRoomId = "DAMP-CAVE" },
                [Direction.South] = new RoomExit { CustomMessage = "The beach is narrow and the cliff walls impassable." }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "ENGRAVINGS-CAVE",
            Name = "Engravings Cave",
            Description = "You are in a cave with engravings on the walls.",
            LongDescription = "You have entered a low cave with passages leading northwest and east. There are old engravings on the walls here.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.West] = new RoomExit { DestinationRoomId = "ROUND-ROOM" },
                [Direction.NorthWest] = new RoomExit { DestinationRoomId = "DOME-ROOM" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "DOME-ROOM",
            Name = "Dome Room",
            Description = "You are in a domed room.",
            LongDescription = "You are at the periphery of a large dome, which forms the ceiling of another room below. Protecting you from a precipitous drop is a wooden railing which circles the dome.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.SouthEast] = new RoomExit { DestinationRoomId = "ENGRAVINGS-CAVE" },
                [Direction.Down] = new RoomExit { DestinationRoomId = "TORCH-ROOM" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "TORCH-ROOM",
            Name = "Torch Room",
            Description = "This is a large room.",
            LongDescription = "This is a large room with a prominent doorway leading to a down staircase. To the west is a narrow twisting tunnel, with a doorway to the east.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.West] = new RoomExit { DestinationRoomId = "TEMPLE" },
                [Direction.East] = new RoomExit { DestinationRoomId = "NORTH-SOUTH-PASSAGE" },
                [Direction.Down] = new RoomExit { DestinationRoomId = "TEMPLE" },
                [Direction.Up] = new RoomExit { DestinationRoomId = "DOME-ROOM" }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "TEMPLE",
            Name = "Temple",
            Description = "This is the north end of a large temple.",
            LongDescription = "This is the north end of a large temple. On the east wall is an ancient inscription, probably a prayer in a long-forgotten language. Below the prayer is a staircase leading down. The west wall is solid rock. To the south is a dark chimney.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "TORCH-ROOM" },
                [Direction.Down] = new RoomExit { DestinationRoomId = "ALTAR" },
                [Direction.South] = new RoomExit { CustomMessage = "The chimney is too dark to enter without a light source." }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "ALTAR",
            Name = "Altar",
            Description = "This is the altar.",
            LongDescription = "This is the south end of a large temple. In front of you is what appears to be an altar. In one corner is a small hole in the floor which leads into darkness. You probably could not get back up it.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.Up] = new RoomExit { DestinationRoomId = "TEMPLE" },
                [Direction.Down] = new RoomExit { CustomMessage = "You might be able to get down, but you couldn't get back up." }
            }
        });

        _world.AddRoom(new Room
        {
            Id = "DEEP-CANYON",
            Name = "Deep Canyon",
            Description = "You are in a deep canyon.",
            LongDescription = "You are on the south edge of a deep canyon. Passages lead off to the east, north and northwest. You can hear the sound of flowing water from below.",
            Flags = RoomFlags.None,
            Exits = new Dictionary<Direction, IRoomExit>
            {
                [Direction.East] = new RoomExit { DestinationRoomId = "RESERVOIR" },
                [Direction.North] = new RoomExit { DestinationRoomId = "CHASM" },
                [Direction.NorthWest] = new RoomExit { DestinationRoomId = "LOUD-ROOM" },
                [Direction.Down] = new RoomExit { DestinationRoomId = "LOUD-ROOM" }
            }
        });
    }

    private void CreateObjects()
    {
        // Mailbox
        _world.AddObject(new GameObject
        {
            Id = "MAILBOX",
            Name = "small mailbox",
            Description = "A small mailbox. It is closed.",
            Synonyms = new[] { "mailbox", "box" },
            Adjectives = new[] { "small" },
            Flags = ObjectFlags.Visible | ObjectFlags.Container | ObjectFlags.Openable,
            Size = 5,
            Capacity = 10
        });

        // Leaflet
        _world.AddObject(new GameObject
        {
            Id = "LEAFLET",
            Name = "leaflet",
            Description = "\"WELCOME TO ZORK!\n\nZORK is a game of adventure, danger, and low cunning. In it you will explore some of the most amazing territory ever seen by mortals. No computer should be without one!\"",
            Synonyms = new[] { "leaflet", "pamphlet", "mail" },
            Adjectives = new[] { },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Readable,
            Size = 2,
            Value = 0
        });

        // Brass Lantern
        _world.AddObject(new GameObject
        {
            Id = "LANTERN",
            Name = "brass lantern",
            Description = "A battery-powered brass lantern.",
            Synonyms = new[] { "lantern", "lamp", "light" },
            Adjectives = new[] { "brass" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Light,
            Size = 5,
            Value = 0
        });

        // Rope
        _world.AddObject(new GameObject
        {
            Id = "ROPE",
            Name = "rope",
            Description = "A large coil of rope.",
            Synonyms = new[] { "rope", "coil" },
            Adjectives = new[] { "large" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible,
            Size = 8,
            Value = 0
        });

        // Sword
        _world.AddObject(new GameObject
        {
            Id = "SWORD",
            Name = "elvish sword",
            Description = "A sword of Elvish workmanship.",
            Synonyms = new[] { "sword", "blade" },
            Adjectives = new[] { "elvish", "elven" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Weapon,
            Size = 6,
            Value = 0
        });

        // Painting
        _world.AddObject(new GameObject
        {
            Id = "PAINTING",
            Name = "painting",
            Description = "A beautiful painting of an old man sitting on a throne.",
            Synonyms = new[] { "painting", "picture" },
            Adjectives = new[] { "beautiful", "old" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure,
            Size = 12,
            Value = 14
        });

        // Egg
        _world.AddObject(new GameObject
        {
            Id = "EGG",
            Name = "jeweled egg",
            Description = "There is a large emerald encrusted egg here. It is covered with precious jewels and is beautifully crafted.",
            Synonyms = new[] { "egg" },
            Adjectives = new[] { "jeweled", "emerald" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure | ObjectFlags.Container | ObjectFlags.Openable,
            Size = 5,
            Capacity = 5,
            Value = 50
        });

        // Canary
        _world.AddObject(new GameObject
        {
            Id = "CANARY",
            Name = "canary",
            Description = "A cheerful little canary. It's singing a merry tune.",
            Synonyms = new[] { "canary", "bird" },
            Adjectives = new[] { "little", "cheerful", "small" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible,
            Size = 2,
            Value = 0
        });

        // Sack
        _world.AddObject(new GameObject
        {
            Id = "SACK",
            Name = "brown sack",
            Description = "A large brown sack, smelling of hot peppers.",
            Synonyms = new[] { "sack", "bag" },
            Adjectives = new[] { "brown", "large" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Container | ObjectFlags.IsOpen,
            Size = 8,
            Capacity = 20,
            Value = 0
        });

        // Garlic
        _world.AddObject(new GameObject
        {
            Id = "GARLIC",
            Name = "clove of garlic",
            Description = "A clove of garlic.",
            Synonyms = new[] { "garlic", "clove" },
            Adjectives = new[] { },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible,
            Size = 2,
            Value = 0
        });

        // Lunch
        _world.AddObject(new GameObject
        {
            Id = "LUNCH",
            Name = "lunch",
            Description = "A hot pepper sandwich on a sesame seed bun.",
            Synonyms = new[] { "lunch", "sandwich", "food" },
            Adjectives = new[] { "hot", "pepper" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Edible,
            Size = 3,
            Value = 0
        });

        // Water
        _world.AddObject(new GameObject
        {
            Id = "WATER",
            Name = "quantity of water",
            Description = "Some water.",
            Synonyms = new[] { "water" },
            Adjectives = new[] { },
            Flags = ObjectFlags.Takeable | ObjectFlags.Drinkable,
            Size = 4,
            Value = 0
        });

        // Bottle
        _world.AddObject(new GameObject
        {
            Id = "BOTTLE",
            Name = "glass bottle",
            Description = "A glass bottle.",
            Synonyms = new[] { "bottle" },
            Adjectives = new[] { "glass" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Container,
            Size = 4,
            Capacity = 8,
            Value = 0
        });

        // Trophy Case
        _world.AddObject(new GameObject
        {
            Id = "TROPHY-CASE",
            Name = "trophy case",
            Description = "An antique trophy case, quite useful for storing valuables.",
            Synonyms = new[] { "case", "trophy" },
            Adjectives = new[] { "trophy", "antique" },
            Flags = ObjectFlags.Visible | ObjectFlags.Container | ObjectFlags.Surface,
            Size = 100,
            Capacity = 200,
            Value = 0
        });

        // Rug
        _world.AddObject(new GameObject
        {
            Id = "RUG",
            Name = "oriental rug",
            Description = "A beautiful oriental rug. There appears to be something under it.",
            Synonyms = new[] { "rug", "carpet" },
            Adjectives = new[] { "oriental", "beautiful", "large" },
            Flags = ObjectFlags.Visible,
            Size = 50,
            Value = 0
        });

        // Trap Door (under rug)
        _world.AddObject(new GameObject
        {
            Id = "TRAP-DOOR",
            Name = "trap door",
            Description = "A closed trap door. It's locked.",
            Synonyms = new[] { "door", "trapdoor", "trap" },
            Adjectives = new[] { "trap" },
            Flags = ObjectFlags.Visible | ObjectFlags.Openable | ObjectFlags.Locked,
            Size = 100,
            Value = 0
        });

        // Table
        _world.AddObject(new GameObject
        {
            Id = "TABLE",
            Name = "table",
            Description = "A wooden kitchen table.",
            Synonyms = new[] { "table" },
            Adjectives = new[] { "wooden", "kitchen" },
            Flags = ObjectFlags.Visible | ObjectFlags.Surface,
            Size = 100,
            Capacity = 50,
            Value = 0
        });

        // Window
        _world.AddObject(new GameObject
        {
            Id = "WINDOW",
            Name = "window",
            Description = "A small window, slightly ajar.",
            Synonyms = new[] { "window" },
            Adjectives = new[] { "small" },
            Flags = ObjectFlags.Visible | ObjectFlags.Openable | ObjectFlags.IsOpen,
            Size = 100,
            Value = 0
        });

        // Torch
        _world.AddObject(new GameObject
        {
            Id = "TORCH",
            Name = "torch",
            Description = "A wooden torch, burning brightly.",
            Synonyms = new[] { "torch" },
            Adjectives = new[] { "wooden", "burning" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Light | ObjectFlags.Flammable,
            Size = 4,
            Value = 0
        });

        // Matches
        _world.AddObject(new GameObject
        {
            Id = "MATCHES",
            Name = "matchbook",
            Description = "A matchbook with a few matches left.",
            Synonyms = new[] { "matches", "matchbook" },
            Adjectives = new[] { },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible,
            Size = 1,
            Value = 0
        });

        // Knife
        _world.AddObject(new GameObject
        {
            Id = "KNIFE",
            Name = "knife",
            Description = "A sharp kitchen knife.",
            Synonyms = new[] { "knife", "blade" },
            Adjectives = new[] { "sharp", "kitchen" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Weapon,
            Size = 3,
            Value = 0
        });

        // Nasty Knife (treasure)
        _world.AddObject(new GameObject
        {
            Id = "NASTY-KNIFE",
            Name = "nasty knife",
            Description = "A vicious-looking stiletto, with a jewel-encrusted hilt.",
            Synonyms = new[] { "knife", "stiletto" },
            Adjectives = new[] { "nasty", "vicious", "jeweled" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Weapon | ObjectFlags.Treasure,
            Size = 4,
            Value = 35
        });

        // Candlestick (treasure)
        _world.AddObject(new GameObject
        {
            Id = "CANDLESTICK",
            Name = "pair of candles",
            Description = "A beautiful pair of brass candlesticks.",
            Synonyms = new[] { "candles", "candlestick", "candlesticks" },
            Adjectives = new[] { "brass", "beautiful", "pair" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure,
            Size = 6,
            Value = 25
        });

        // Trident (treasure)
        _world.AddObject(new GameObject
        {
            Id = "TRIDENT",
            Name = "crystal trident",
            Description = "A beautiful crystal trident, probably a display piece.",
            Synonyms = new[] { "trident", "fork" },
            Adjectives = new[] { "crystal", "beautiful" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure | ObjectFlags.Weapon,
            Size = 8,
            Value = 40
        });

        // Chalice (treasure)
        _world.AddObject(new GameObject
        {
            Id = "CHALICE",
            Name = "jeweled chalice",
            Description = "A beautiful jeweled chalice.",
            Synonyms = new[] { "chalice", "cup" },
            Adjectives = new[] { "jeweled", "beautiful" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure | ObjectFlags.Container,
            Size = 6,
            Capacity = 10,
            Value = 55
        });

        // Coins (treasure)
        _world.AddObject(new GameObject
        {
            Id = "COINS",
            Name = "pile of coins",
            Description = "A pile of old zorkmid coins.",
            Synonyms = new[] { "coins", "coin", "pile", "zorkmids" },
            Adjectives = new[] { "old", "pile" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure,
            Size = 3,
            Value = 10
        });

        // Trunk (treasure container)
        _world.AddObject(new GameObject
        {
            Id = "TRUNK",
            Name = "large trunk",
            Description = "A large wooden trunk, probably used by pirates.",
            Synonyms = new[] { "trunk", "chest" },
            Adjectives = new[] { "large", "wooden", "pirate" },
            Flags = ObjectFlags.Visible | ObjectFlags.Container | ObjectFlags.Openable,
            Size = 50,
            Capacity = 100,
            Value = 0
        });

        // Sapphire (treasure)
        _world.AddObject(new GameObject
        {
            Id = "SAPPHIRE",
            Name = "sapphire bracelet",
            Description = "A beautiful sapphire bracelet.",
            Synonyms = new[] { "bracelet", "sapphire" },
            Adjectives = new[] { "sapphire", "beautiful" },
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible | ObjectFlags.Treasure,
            Size = 2,
            Value = 45
        });
    }

    private void PlaceObjects()
    {
        // Place objects in their starting locations
        PlaceObject("MAILBOX", "WEST-OF-HOUSE");
        PlaceObject("LEAFLET", "MAILBOX");

        PlaceObject("SACK", "KITCHEN");
        PlaceObject("GARLIC", "SACK");
        PlaceObject("LUNCH", "SACK");

        PlaceObject("TROPHY-CASE", "LIVING-ROOM");
        PlaceObject("RUG", "LIVING-ROOM");

        PlaceObject("TABLE", "KITCHEN");
        PlaceObject("WINDOW", "KITCHEN");
        PlaceObject("KNIFE", "TABLE");

        PlaceObject("ROPE", "ATTIC");
        PlaceObject("MATCHES", "ATTIC");
        PlaceObject("NASTY-KNIFE", "ATTIC");

        PlaceObject("LANTERN", "LIVING-ROOM");
        PlaceObject("SWORD", "LIVING-ROOM");

        PlaceObject("PAINTING", "GALLERY");
        PlaceObject("EGG", "GALLERY");
        PlaceObject("CANARY", "EGG");

        PlaceObject("BOTTLE", "BEHIND-HOUSE");
        PlaceObject("WATER", "RESERVOIR");

        PlaceObject("TORCH", "TORCH-ROOM");
        PlaceObject("CANDLESTICK", "ALTAR");

        PlaceObject("TRUNK", "RESERVOIR");
        PlaceObject("TRIDENT", "TRUNK");
        PlaceObject("CHALICE", "TRUNK");

        PlaceObject("COINS", "MAZE-1");
        PlaceObject("SAPPHIRE", "ENGRAVINGS-CAVE");
    }

    private void PlaceObject(string objectId, string locationId)
    {
        var obj = _world.GetObject(objectId);
        if (obj == null) return;

        obj.LocationId = locationId;

        // If location is a room, add to room items
        var room = _world.GetRoom(locationId);
        if (room != null)
        {
            room.Items.Add(objectId);
            return;
        }

        // If location is a container object, add to container
        var container = _world.GetObject(locationId);
        container?.Contents.Add(objectId);
    }
}
