using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinkedListExtensions
{
	public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
	{
		var node = list.First;
		while(node != null)
		{
			yield return node;
			node = node.Next;
		}
	}
	
	public static void ShiftTransformStarting<T>(this LinkedList<T> list, LinkedListNode<T> start, Vector2 shift2D) where T : MonoBehaviour
	{
		Vector3 shift = new Vector3(shift2D.x, shift2D.y, 0);
		var current = start;
		while(current != null)
		{
			start.Value.transform.position += shift;
			current = current.Next;
		}
	}
	
	public static void ActionStarting<T>(this LinkedList<T> list, LinkedListNode<T> start, Action<T> action) 
	{
		var current = start;
		while(current != null)
		{
			action(current.Value);
			current = current.Next;
		}
	}
}
