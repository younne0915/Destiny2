print('启动GameInit.lua')

require "Download/xLuaLogic/Core/CtrlMgr"

GameInit = {};
local this = GameInit;

function GameInit.InitViews()
	require('Download/xLuaLogic/Modules/UIRoot/UIRootView');
	require('Download/xLuaLogic/Modules/Task/TaskView');
end


function GameInit.Init()
	this.InitViews();
	CtrlMgr.Init();
	GameInit.LoadView(CtrlNames.UIRootCtrl);
end

function GameInit.LoadView(type)
	local ctrl = CtrlMgr.GetCtrl(type);
	if ctrl ~= nil then
		ctrl.Awake();
	end
end