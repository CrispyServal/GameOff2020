using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvTableItem: TableItemBase
{
    public int id;
    public string name;
}

public class EnvTable : TableBase
{
    private List<EnvTableItem> list = new List<EnvTableItem>
    {
        new EnvTableItem { id = 0, name = "Temp" },
    };
    public override TableItemBase GetTableItem(int id)
    {
        for (int i = 0; i < list.Count; i ++)
        {
            if (list[i].id == id)
                return list[i];
        }
        return null;
    }
}
