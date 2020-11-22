using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TableConfig: SingletonBase<TableConfig>
{
    private Dictionary<Type, TableBase> tables = new Dictionary<Type, TableBase>
    {
        { typeof(EnvTable), new EnvTable() },
    };

    public TableBase GetTable(Type t)
    {
        return tables[t];
    }
}

public abstract class TableItemBase { }

public abstract class TableBase
{
    public abstract TableItemBase GetTableItem(int id);
}

