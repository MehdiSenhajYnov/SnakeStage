using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public static class ExtensionMethods
{
    public static async void RotateToSmooth(this Transform transform, Direction direction, float timeToPerform = 1f)
    {
        float angle = 0f;

        switch (direction)
        {
            case Direction.Up:
                angle = 0f;
                break;

            case Direction.Down:
                angle = 180f;
                break;

            case Direction.Left:
                angle = 90f;
                break;

            case Direction.Right:
                angle = -90f;
                break;
        }

        
        Quaternion RotatoToDir = Quaternion.Euler(new Vector3(0f, 0f, angle));

        float timeElapsed = 0f;
        while (timeElapsed < timeToPerform)
        {
            if (!Application.isPlaying) return;
            if (transform == null) return;
            transform.rotation = Quaternion.Slerp(transform.rotation, RotatoToDir, timeElapsed / timeToPerform);
            timeElapsed += Time.deltaTime;
            await Task.Yield();
        }
        transform.rotation = RotatoToDir;
    }
    public static void RotateTo(this Transform transform, Direction direction)
    {
        float angle = 0f;

        switch (direction)
        {
            case Direction.Up:
                angle = 0f;
                break;

            case Direction.Down:
                angle = 180f;
                break;

            case Direction.Left:
                angle = 90f;
                break;

            case Direction.Right:
                angle = -90f;
                break;
        }

        Quaternion RotatoToDir = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = RotatoToDir;
    }
    public static bool isOpposite(this Direction direction, Direction D2)
    {
        if (direction == Direction.Up && D2 == Direction.Down) return true;
        if (direction == Direction.Left && D2 == Direction.Right) return true;
        if (direction == Direction.Right && D2 == Direction.Left) return true;
        if (direction == Direction.Down && D2 == Direction.Up) return true;

        return false;
    }
    public static bool isRight(this Direction direction, Direction D2)
    {
        if (direction == Direction.Up && D2 == Direction.Right) return true;
        if (direction == Direction.Right && D2 == Direction.Down) return true;
        if (direction == Direction.Down && D2 == Direction.Left) return true;
        if (direction == Direction.Left && D2 == Direction.Up) return true;
        return false;
    }
    public static bool isLeft(this Direction direction, Direction D2)
    {
        if (direction == Direction.Up && D2 == Direction.Left) return true;
        if (direction == Direction.Left && D2 == Direction.Down) return true;
        if (direction == Direction.Down && D2 == Direction.Right) return true;
        if (direction == Direction.Right && D2 == Direction.Up) return true;
        return false;
    }
    public static bool isHorizontal(this Direction direction)
    {
        if (direction == Direction.Left || direction == Direction.Right) return true;
        return false;
    }
    public static bool isVertical(this Direction direction)
    {
        if (direction == Direction.Up || direction == Direction.Down) return true;
        return false;
    }


}
