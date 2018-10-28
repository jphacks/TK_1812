using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Response
{
    public Result[] results;
}

[Serializable]
public class Result
{
    public Geometry geometry;
    public string icon;
    public string name;
    public string place_id;
}

[Serializable]
public class Geometry
{
    public Location location;
}

[Serializable]
public class Location
{
    public double lat;
    public double lng;
}
