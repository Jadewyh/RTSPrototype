//using UnityEngine;

//public enum CommandType
//{
//  ATTACK,
//  MOVE
//}

//public class Command
//{
//  public Command(CommandType t, Unit obj, Vector3 pos)
//  {
//    type = t;
//    target = obj;
//    destination = pos;
//  }

//  public Command(CommandType t, Unit obj)
//  {
//    type = t;
//    target = obj;
//    destination = Vector3.zero;
//  }

//  public Command(CommandType t, Vector3 pos)
//  {
//    type = t;
//    target = null;
//    destination = pos;
//  }

//  public CommandType type;
//  public Unit target;
//  public Vector3 destination;

//  public static implicit operator Unit(Command c)
//  {
//    return c.target;
//  }

//  public static implicit operator Vector3(Command c)
//  {
//    return c.destination;
//  }

//}