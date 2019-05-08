
UIRootCtrl = {};

local this = UIRootCtrl;

local root;
local transform;
local gameObject;


function UIRootCtrl.New()
	return this;
end

function UIRootCtrl.Awake()
	print('主界面 启动了');
	--CS.LuaHelper.ViewUtil:LoadUIRoot(this.OnCreate);
    CS.LuaHelper.Instance.uiSceneCtrl:LoadSceneUI(UIRootCtrl.OnCreate,"Download/Prefab/xLuaUIPrefab/UIRootView");
end

function UIRootCtrl.OnCreate(obj)
	print(obj.name);
	
	UIRootView.btnOpenTask.onClick:AddListener(this.OnBtnOpenTaskClick);
end

function UIRootCtrl.OnBtnOpenTaskClick()
	GameInit.LoadView(CtrlNames.TaskCtrl);
end