ViewTemplate = {};

local this = ViewTemplate;

local transform;
local gameObject;

function ViewTemplate.awake(obj)
	gameObject = obj;
	transform = obj.transform;
end

function ViewTemplate.start()
end

function ViewTemplate.update()
end

function ViewTemplate.ondestroy()
end

function ViewTemplate.InitView()
end