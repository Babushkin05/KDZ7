﻿namespace StationsLib;
/// <summary>
/// Station class.
/// </summary>
public class Station
{
    // id os atation.
    private int id_;

    public int ID
    {
        get => id_;
        set
        {
            if (value < 0)
                throw new ArgumentException("Unable to set id < 1");
            if (ID != default(int))
                throw new Exception("Id already setted");
            id_ = value;
        }
    }

    // Name of station.
    private string? nameOfStation_;

    public string? NameOfStation
    {
        get => nameOfStation_;
        set => nameOfStation_ = value;
    }

    // Name of line.
    private string? line_;

    public string? Line
    {
        get => line_;
        set => line_ = value;
    }

    // Longtitude of station.
    private double longtitude_;

    public double Longitude_WGS84
    {
        get => longtitude_;
        set => longtitude_ = value;
    }

    // Latitude of station.
    private double latitude_;

    public double Latitude_WGS84
    {
        get => latitude_;
        set => latitude_ = value;
    }

    // AdmArea of station.
    private string? admArea_;

    public string? AdmArea
    {
        get => admArea_;
        set => admArea_ = value;
    }

    // District of station.
    private string? district_;

    public string? District
    {
        get => district_;
        set => district_ = value;
    }

    // Year of data.
    private int year_;

    public int Year
    {
        get => year_;
        set => year_ = value;
    }

    // Month of data.
    private string? month_;

    public string? Month
    {
        get => month_;
        set => month_ = value;
    }

    // Id of data.
    private System.Int64 global_id_;

    public System.Int64 global_id
    {
        get => global_id_;
        set
        {
            if (value < 0)
                throw new ArgumentNullException("Unable to set id less than 1");
            if (global_id != default(int))
                throw new Exception("Global id already setted.");
            global_id_ = value;
        }
    }

    // GeoData center.
    private string? geodata_center_;

    public string? geodata_center
    {
        get => geodata_center_;
        set => geodata_center_ = value;
    }

    //Geoarea.
    private string? geoarea_;

    public string? geoarea
    {
        get => geoarea_;
        set => geoarea_ = value;
    }

    // Empty constructor.
    public Station()
    {
        ID = 0;
        NameOfStation = "Default Name";
        Line = "Default Line";
        Longitude_WGS84 = 0;
        Latitude_WGS84 = 0;
        AdmArea = "Defalt AdmArea";
        District = "Defaul District";
        Year = 1980;
        Month = "January";
        global_id = 0;
        geodata_center = "";
        geoarea = "";
    }

    // Constructor.
    public Station(int id, string? nameOfStation, string line, double longtitude
        ,double latitude, string? admArea, string? district, int year, string? month,
        System.Int64 globalId, string? geodataCenter, string? geoArea)
    {
        ID = id;
        NameOfStation = nameOfStation;
        Line = line;
        Longitude_WGS84 = longtitude;
        Latitude_WGS84 = latitude;
        AdmArea = admArea;
        District = district;
        Year = year;
        Month = month;
        global_id = globalId;
        geodata_center = geodataCenter;
        geoarea = geoArea;
    }

    // Indexer.
    public object? this[string name]
    {
        get
        {
            return name switch
            {
                "ID" => ID,
                "NameOfStation" => NameOfStation,
                "Line" => Line,
                "Longitude_WGS84" => Longitude_WGS84,
                "Latitude_WGS84" => Latitude_WGS84,
                "AdmArea" => AdmArea,
                "District" => District,
                "Year" => Year,
                "Month" => Month,
                "global_id" => global_id,
                "geodata_center" => geodata_center,
                "geoarea" => geoarea,
                _ => throw new ArgumentException("There is no this field")
            };
        }

    }

}

