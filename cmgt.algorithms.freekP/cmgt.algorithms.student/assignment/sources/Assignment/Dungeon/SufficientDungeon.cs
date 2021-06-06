using GXPEngine;
using GXPEngine.OpenGL;
using System;
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

        for (int i = 0; i <= 10; i++)
        {
            Room toSplit = FindRoom(pMinimumRoomSize);
            if (toSplit == null) break;
            else SplitRoom(toSplit, pMinimumRoomSize);
        }

        PlaceDoors();

    }
    Room FindRoom(int pMinimumRoomSize)
    {
        List<Room> tempRooms = new List<Room>();


        for (int i = rooms.Count - 1; i >= 0; i--)
        {
            if (rooms[i].area.Width >= (pMinimumRoomSize * 2) || rooms[i].area.Height >= (pMinimumRoomSize * 2))
            {
                tempRooms.Add(rooms[i]);
            }
        }

        if (tempRooms.Count == 0) return null;

        return tempRooms[Utils.Random(0, tempRooms.Count)];

    }

    void SplitRoom(Room room, int pMinimumRoomSize)
    {
        // Split horizontaly
        if (room.area.Width <= room.area.Height)
        {
            int begin = room.area.Y + pMinimumRoomSize;
            int end = room.area.Y + room.area.Height - pMinimumRoomSize;
            int wallPos = Utils.Random(begin, end + 1);
            rooms.Add(new Room(new Rectangle(room.area.X, room.area.Y, room.area.Width, wallPos - room.area.Y + 1)));
            rooms.Add(new Room(new Rectangle(room.area.X, wallPos, room.area.Width, (room.area.Y + room.area.Height) - wallPos)));
            
        }

        //Split vertically
        else
        {
            int begin = room.area.X + pMinimumRoomSize;
            int end = room.area.X + room.area.Width - pMinimumRoomSize;
            int wallPos = Utils.Random(begin, end + 1);
            rooms.Add(new Room(new Rectangle(room.area.X, room.area.Y, wallPos - room.area.X + 1, room.area.Height)));
            rooms.Add(new Room(new Rectangle(wallPos, room.area.Y, (room.area.X + room.area.Width) - wallPos, room.area.Height)));
        }
        rooms.Remove(room);
    }

    void PlaceDoors()
    {
        for (int i = 0; i < rooms.Count-1; i++)
        {
            for (int j = i+1; j < rooms.Count; j++)
            {
               CheckSide(rooms[i], rooms[j]);               
            }
        }
    }

    private void CheckSide(Room roomA, Room roomB)
    {
        if (roomA.area.Right -1 == roomB.area.Left) CheckWallPositionY(roomA, roomB, roomA.area.Right-1);       // Check right side of room
        if (roomA.area.Left == roomB.area.Right - 1) CheckWallPositionY(roomB, roomA, roomA.area.X);            // Check left side of room
        if (roomA.area.Top == roomB.area.Bottom - 1) CheckWallPositionX(roomA, roomB, roomA.area.Y);            // Check top of room
        if (roomA.area.Bottom - 1 == roomB.area.Top) CheckWallPositionX(roomB, roomA, roomA.area.Bottom-1);     // Check bottom of room
    }

    void CheckWallPositionX(Room roomA, Room roomB, int height)
    {
        
        //A is more left than B
        if (roomA.area.Left < roomB.area.Left && roomB.area.X+1 < roomA.area.Right - 1) doors.Add(new Door(new Point(CalculatDoorPosHor(roomA,roomB), height)));
        

        //B is more left than A
        if (roomA.area.Left >= roomB.area.Left && roomA.area.X+1 < roomB.area.Right -1) doors.Add(new Door(new Point(CalculatDoorPosHor(roomB, roomA), height)));
    }

    void CheckWallPositionY(Room roomA, Room roomB, int width)
    {
        if (roomA.area.Y <= roomB.area.Y && roomB.area.Y + 1 < roomA.area.Bottom - 1) doors.Add(new Door(new Point(width, CalculatDoorPosVer(roomA, roomB))));
        if (roomA.area.Y > roomB.area.Y && roomA.area.Y + 1 < roomB.area.Bottom - 1) doors.Add(new Door(new Point(width, CalculatDoorPosVer(roomB, roomA))));
    }

    int CalculatDoorPosHor(Room roomA, Room roomB)
    {
        //Default (if A is more to the left than B)
        int leftWall = roomB.area.X +1;
        int rightWall = (roomA.area.Right < roomB.area.Right ? roomA.area.Right : roomB.area.Right) - 1;

        int doorPosX = Utils.Random(leftWall, rightWall);

        return doorPosX;

    }

    int CalculatDoorPosVer(Room roomA, Room roomB)
    {
        //default (if A is higher than B)
        int topWall = roomB.area.Y + 1;
        int bottomWall = (roomA.area.Bottom < roomB.area.Bottom ? roomA.area.Bottom : roomB.area.Bottom) - 1;

        int doorPosY = Utils.Random(topWall, bottomWall);

        return doorPosY;
    }

}


