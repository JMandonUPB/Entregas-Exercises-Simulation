using System;
using UnityEngine;

[Serializable]

struct MyVector2D
{
    public float x;
    public float y;

    public float magnitude{get{return Mathf.Sqrt(x * x + y * y);}}

    public MyVector2D normalized
    {
        get
        {
            if (magnitude <= 0.0001f)
            {
                return new MyVector2D(0, 0);
            }

            return new MyVector2D(x / magnitude, y / magnitude);
        }
    }

    public MyVector2D(float x, float y) //para construir mi vectorCustom
    {
        this.x = x;
        this.y = y;
    }

    public void Normalized()
    {
        float tolerance = 0.0001f;
        if (magnitude <= tolerance)
        {
            x = 0; y = 0;
            return;
        }

        x /= magnitude; y /= magnitude;
    }

    public MyVector2D Sum(MyVector2D a)
    {
        return new MyVector2D(this.x + a.x, this.y + a.y);
    }
    public MyVector2D Sub(MyVector2D a)
    {
        return new MyVector2D(this.x - a.x, this.y - a.y);
    }
    public MyVector2D Scale(float a)
    {
        return new MyVector2D(this.x * a, this.y * a);
    }

    public MyVector2D div(float a)
    {
        return new MyVector2D(x / a, y / a);
    }

    public static MyVector2D operator /(float b, MyVector2D a)
    {
        return a.div(b);
    }

    public static MyVector2D operator /(MyVector2D a, float b)
    {
        return a.div(b);
    }

    public void Draw(Color color)
    {
        Debug.DrawLine(Vector3.zero, new Vector3(x, y, 0), color);
    }

    public override string ToString()
    {
        return $"[{x},{y}]";
    }

    public void Draw(MyVector2D newOrigin, Color color)
    {
        Debug.DrawLine(new Vector3(newOrigin.x, newOrigin.y), new Vector3(newOrigin.x + x, newOrigin.y + y), color);
    }

    public static MyVector2D operator +(MyVector2D a, MyVector2D b)
    {
        return new MyVector2D(a.x + b.x, a.y + b.y);
    }

    public static MyVector2D operator -(MyVector2D a, MyVector2D b)
    {
        return new MyVector2D(a.x - b.x, a.y - b.y);
    }

    public static MyVector2D operator *(MyVector2D a, float b)
    {
        return new MyVector2D(a.x * b, a.y * b);
    }

    public static MyVector2D operator *(float b, MyVector2D a)
    {
        return new MyVector2D(a.x * b, a.y * b);
    }

    public static implicit operator Vector3(MyVector2D a)
    {
        return new Vector3(a.x, a.y, 0);
    }
    public static implicit operator MyVector2D(Vector3 a)
    {
        return new MyVector2D(a.x, a.y);
    }
}