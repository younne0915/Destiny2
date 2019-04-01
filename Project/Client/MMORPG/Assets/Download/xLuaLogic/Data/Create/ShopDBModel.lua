require "download/xLuaLogic/Data/Create/ShopEntity"

--数据访问
ShopDBModel = { }

local this = ShopDBModel;

local shopTable = { }; --定义表格

function ShopDBModel.New()
    return this;
end

function ShopDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Shop.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        shopTable[#shopTable+1] = ShopEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]) );
    end

end

function ShopDBModel.GetList()
    return shopTable;
end

function ShopDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #shopTable, 1 do
        if (shopTable[i].Id == id) then
            ret = shopTable[i];
            break;
        end
    end
    return ret;
end