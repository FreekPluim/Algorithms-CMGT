﻿using System.Diagnostics;
using System.Drawing;
using GXPEngine;
using GXPEngine.OpenGL;
using System;
using System.Collections.Generic;

/**
 * An example of a dungeon nodegraph implementation.
 * 
 * This implementation places only three nodes and only works with the SampleDungeon.
 * Your implementation has to do better :).
 * 
 * It is recommended to subclass this class instead of NodeGraph so that you already 
 * have access to the helper methods such as getRoomCenter etc.
 * 
 * TODO:
 * - Create a subclass of this class, and override the generate method, see the generate method below for an example.
 */
class SufficientDungeonNodeGraph : NodeGraph
{
	protected Dungeon _dungeon;

	public SufficientDungeonNodeGraph(Dungeon pDungeon) : base((int)(pDungeon.size.Width * pDungeon.scale), (int)(pDungeon.size.Height * pDungeon.scale), (int)pDungeon.scale / 3)
	{
		Debug.Assert(pDungeon != null, "Please pass in a dungeon.");

		_dungeon = pDungeon;
	}

	List<Node> roomNodes = new List<Node>();
	List<Node> doorNodes = new List<Node>();

	protected override void generate()
	{
		/*//Generate nodes, in this sample node graph we just add to nodes manually
		//of course in a REAL nodegraph (read:yours), node placement should 
		//be based on the rooms in the dungeon

		//We assume (bad programming practice 1-o-1) there are two rooms in the given dungeon.
		//The getRoomCenter is a convenience method to calculate the screen space center of a room
		nodes.Add(new Node(getRoomCenter(_dungeon.rooms[0])));
		nodes.Add(new Node(getRoomCenter(_dungeon.rooms[1])));
		//The getDoorCenter is a convenience method to calculate the screen space center of a door
		nodes.Add(new Node(getDoorCenter(_dungeon.doors[0])));

		//create a connection between the two rooms and the door...
		AddConnection(nodes[0], nodes[2]);
		AddConnection(nodes[1], nodes[2]);*/
		PlaceNodes();
	}

	void PlaceNodes()
    {
		SetNodes();
		/* for (int i = 0; i < _dungeon.rooms.Count + _dungeon.doors.Count; i++)
		 {


		 }*/

		/*for (int i = 0; i < _dungeon.doors.Count - 1; i++)
		{
			if (i < roomNodes.Count) nodes.Add(roomNodes[i]);
			if (i < doorNodes.Count) nodes.Add(doorNodes[i]);

			for (int j = i + 1; j < _dungeon.rooms.Count + _dungeon.doors.Count; j++)
			{
				AddConnection(nodes[i], nodes[j]);
			}
		}*/

		for (int i = 0; i < _dungeon.doors.Count - 1; i++)
		{
			//Make 1 room
			nodes.Add(new Node(getDoorCenter(_dungeon.doors[i])));



		}
	}

    private void CheckRooms(int i, int j)
    {
		/*if (_dungeon.rooms[i].area.Left == _dungeon.rooms[i].area.X) nodes.Add(doorNodes[j]);
        if (_dungeon.rooms[i].area.Right-1 == _dungeon.rooms[i].area.X) nodes.Add(doorNodes[j]);
		if (_dungeon.rooms[i].area.Top == _dungeon.rooms[i].area.Y) nodes.Add(doorNodes[j]);
		if (_dungeon.rooms[i].area.Bottom-1 == _dungeon.rooms[i].area.Y) nodes.Add(doorNodes[j]);*/
	}

    void SetNodes()
    {
		
		for (int i = 0; i < _dungeon.rooms.Count; i++)
		{
			//nodes.Add(new Node(getRoomCenter(_dungeon.rooms[i])));
			roomNodes.Add(new Node(getRoomCenter(_dungeon.rooms[i])));
		}

		for (int i = 0; i < _dungeon.doors.Count; i++)
		{
			doorNodes.Add(new Node(getDoorCenter(_dungeon.doors[i])));
		}
	}

	/**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given room you can use for your nodes in this class
	 */
		protected Point getRoomCenter(Room pRoom)
	{
		float centerX = ((pRoom.area.Left + pRoom.area.Right) / 2.0f) * _dungeon.scale;
		float centerY = ((pRoom.area.Top + pRoom.area.Bottom) / 2.0f) * _dungeon.scale;
		return new Point((int)centerX, (int)centerY);
	}

	/**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given door you can use for your nodes in this class
	 */
	protected Point getDoorCenter(Door pDoor)
	{
		return getPointCenter(pDoor.location);
	}

	/**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given point you can use for your nodes in this class
	 */
	protected Point getPointCenter(Point pLocation)
	{
		float centerX = (pLocation.X + 0.5f) * _dungeon.scale;
		float centerY = (pLocation.Y + 0.5f) * _dungeon.scale;
		return new Point((int)centerX, (int)centerY);
	}

}
