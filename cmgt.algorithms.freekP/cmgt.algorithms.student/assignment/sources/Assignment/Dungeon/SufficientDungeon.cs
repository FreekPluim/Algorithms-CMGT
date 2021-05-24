using GXPEngine;
using GXPEngine.OpenGL;
using System.Collections.Generic;
using System.Drawing;

/**
 * An example of a dungeon implementation.  
 * This implementation places two rooms manually but your implementation has to do it procedurally.
 */
class SufficientDungeon : Dungeon
{
	public SufficientDungeon(Size pSize) : base(pSize) { }

	/**
	 * This method overrides the super class generate method to implement a two-room dungeon with a single door.
	 * The good news is, it's big enough to house an Ogre and his ugly children, the bad news your implementation
	 * should generate the dungeon procedurally, respecting the pMinimumRoomSize.
	 * 
	 * Hints/tips: 
	 * - start by generating random rooms in your own Dungeon class and placing random doors.
	 * - playing/experiment freely is the key to all success
	 * - this problem can be solved both iteratively or recursively
	 */

	protected override void generate(int pMinimumRoomSize)
	{
		rooms.Add(new Room(new Rectangle(0, 0, size.Width, size.Height)));

        for (int i = rooms.Count - 1; i >= 0; i--)
        {
            if (rooms[i].area.Width >= (pMinimumRoomSize * 2) || rooms[i].area.Height >= (pMinimumRoomSize * 2))
            {
                if (rooms[i].area.Width <= rooms[i].area.Height)
                {
                    int begin = rooms[i].area.Y + pMinimumRoomSize;
                    int end = rooms[i].area.Y + rooms[i].area.Height - pMinimumRoomSize;
                    int wallPos = Utils.Random(begin, end + 1);
                    rooms.Add(new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y, rooms[i].area.Width, wallPos - rooms[i].area.Y + 1)));
                    rooms.Add(new Room(new Rectangle(rooms[i].area.X, wallPos, rooms[i].area.Width, (rooms[i].area.Y + rooms[i].area.Height) - wallPos)));
                }
                else
                {
                    int begin = rooms[i].area.X + pMinimumRoomSize;
                    int end = rooms[i].area.X + rooms[i].area.Width - pMinimumRoomSize;
                    int wallPos = Utils.Random(begin, end + 1);
                    rooms.Add(new Room(new Rectangle(rooms[i].area.X, rooms[i].area.Y, wallPos - rooms[i].area.X + 1, rooms[i].area.Height)));
                    rooms.Add(new Room(new Rectangle(wallPos, rooms[i].area.Y, (rooms[i].area.X + rooms[i].area.Width) - wallPos, rooms[i].area.Height)));
                }
            }
            rooms.RemoveAt(i);
        }
        
        Room FindRoom()
        {

            return null;
        }

        void SplitRoom()
        {

        }
        //Current problem:
        //It only go's thru the itteration once.

        //and a door in the middle wall with a random y position
        //TODO:experiment with changing the location and the Pens.White below
        //doors.Add(new Door(new Point(size.Width / 2, size.Height / 2 + Utils.Random(-5, 5))));
    }
}

