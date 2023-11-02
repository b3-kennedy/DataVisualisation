using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public Data(string ti, string mag, string dat, string cd, string mm, string al, string ts, string si, string ne, string ns,
        string dm, string ga, string ma, string de, string la, string lo, string loc, string con, string cou)
    {
        title = ti;
        magnitude = mag;
        date = dat;
        cdi = cd;
        mmi = mm;
        alert = al;
        tsunami = ts;
        sig = si;
        net = ne;
        nst = ns;
        dmin = dm;
        gap = ga;
        magType = ma;
        depth = de;
        latitude = la;
        longitude = lo;
        location = loc;
        continent = con;
        country = cou;

    }

    public string title;
    public string magnitude;
    public string date;
    public string cdi;
    public string mmi;
    public string alert;
    public string tsunami;
    public string sig;
    public string net;
    public string nst;
    public string dmin;
    public string gap;
    public string magType;
    public string depth;
    public string latitude;
    public string longitude;
    public string location;
    public string continent;
    public string country;
}
