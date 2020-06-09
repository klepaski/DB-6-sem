using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.IO;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000)]


public struct SqlUserDefinedType1 : INullable, IBinarySerialize
{
    String name;        ///холодильник, морозильник, теплица, парильная
    String owner;
    int temper;
    int ploshad;
    


    public override string ToString()
    {
        return name;
        //return $"{name}, владелец: {owner}, площадь: {ploshad}, температура: {temper}.";
    }

    public bool IsNull
    {
        get
        {
            return _null;
        }
    }

    public static SqlUserDefinedType1 Null
    {
        get
        {
            SqlUserDefinedType1 h = new SqlUserDefinedType1();
            h._null = true;
            return h;
        }
    }

    public static SqlUserDefinedType1 Parse(SqlString s)
    {
        string[] b = s.Value.Split(' ');
        if (s.IsNull)
            return Null;

        SqlUserDefinedType1 u = new SqlUserDefinedType1
        {
            name = b[0],
            owner = b[1],
            ploshad = Convert.ToInt32(b[2]),
            temper = Convert.ToInt32(b[3])
        };
        return u;
    }

    public string Method1()
    {
        return string.Empty;
    }

    public static SqlString Method2()
    {
        return new SqlString("");
    }

    public void Read(BinaryReader r)
    {
        name = r.ReadString();
    }

    public void Write(BinaryWriter w)
    {
        w.Write($"Складское помещение: {name}, владелец: {owner}, площадь: {ploshad}, температура: {temper}.");
    }

    public int _var1;

    private bool _null;
}