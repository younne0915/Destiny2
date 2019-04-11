require "download/xLuaLogic/Data/Create/LanguageEntity"

--数据访问
LanguageDBModel = { }

local this = LanguageDBModel;

local languageTable = { }; --定义表格

function LanguageDBModel.New()
    return this;
end

function LanguageDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Language.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        languageTable[#languageTable+1] = LanguageEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], gameDataTable.Data[i][2], gameDataTable.Data[i][3], gameDataTable.Data[i][4], gameDataTable.Data[i][5] );
    end

end

function LanguageDBModel.GetList()
    return languageTable;
end