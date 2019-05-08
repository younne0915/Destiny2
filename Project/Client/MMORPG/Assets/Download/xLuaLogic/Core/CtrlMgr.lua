--Lua控制器的管理器 作用就是注册所有的控制器

print('启动CtrlMgr.lua')

require "Download/xLuaLogic/Common/Define"

require "Download/xLuaLogic/Modules/UIRoot/UIRootCtrl"
require "Download/xLuaLogic/Modules/Task/TaskCtrl"

CtrlMgr = {};

local this = CtrlMgr;

--控制器列表
local ctrlList = {};

--初始化 往列表中添加所有的控制器
function CtrlMgr.Init()
	ctrlList[CtrlNames.UIRootCtrl] = UIRootCtrl.New();
	ctrlList[CtrlNames.TaskCtrl] = TaskCtrl.New();
	return this;
end

--根据控制器的名称 获取控制器
function CtrlMgr.GetCtrl(ctrlName)
	return ctrlList[ctrlName];
end