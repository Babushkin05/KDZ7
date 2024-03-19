namespace StationsLib;
public class Station
{
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

    private string? nameOfStation_;

    public string? NameOfStation
    {
        get => nameOfStation_;
        set
        {
            if (NameOfStation != default(string?))
                throw new Exception("Name of station already setted.");
            nameOfStation_ = value;
        }
    }

    private string? line_;

    public string? Line
    {
        get => line_;
        set
        {
            if (Line != default(string?))
                throw new Exception("Name of line already setted.");
            line_ = value;
        }
    }

    private double longtitude_;

    public double Longitude_WGS84
    {
        get => longtitude_;
        set
        {
            if (Longitude_WGS84 != default(double))
                throw new Exception("Longtitude already was setted.");

            longtitude_ = value;
        }
    }

    private double latitude_;

    public double Latitude_WGS84
    {
        get => latitude_;
        set
        {
            if (Latitude_WGS84 != default(double))
                throw new Exception("Latitude already setted.");
            latitude_ = value;
        }
    }

    private string? admArea_;

    public string? AdmArea
    {
        get => admArea_;
        set
        {
            if (AdmArea != default(string?))
                throw new Exception("AdmArea already setted.");
            admArea_ = value;
        }
    }

    private string? district_;

    public string? District
    {
        get => district_;
        set
        {
            if (District != default(string?))
                throw new Exception("District already setted.");
            district_ = value;
        }
    }

    private int year_;

    public int Year
    {
        get => year_;
        set
        {
            if (Year != default(int))
                throw new Exception("Year already setted.");
            year_ = value;
        }
    }

    private string? month_;

    public string? Month
    {
        get => month_;
        set
        {
            if (Month != default(string?))
                throw new Exception("Month already setted.");
            month_ = value;
        }
    }

    private int global_id_;

    public int global_id
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

    private string? geodata_center_;

    public string? geodata_center
    {
        get => geodata_center_;
        set
        {
            if (geodata_center != default(string?))
                throw new Exception("Geodata center already setted");
            geodata_center_ = value;
        }
    }

    private string? geoarea_;

    public string? geoarea
    {
        get => geoarea_;
        set
        {
            if (geoarea != default(string?))
                throw new Exception("Geoarea already setted");
            geoarea_ = value;
        }
    }

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

    public Station(int id, string? nameOfStation, string line, double longtitude
        ,double latitude, string? admArea, string? district, int year, string? month,
        int globalId, string? geodataCenter, string? geoArea)
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

