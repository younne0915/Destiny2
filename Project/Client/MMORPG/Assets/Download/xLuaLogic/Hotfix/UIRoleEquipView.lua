

xlua.hotfix
(
	CS.UIRoleEquipView, 'SetUI', function (self, data)
		self.m_JobId = data:GetValue(CS.ConstDefine.JobId);
        self.lblNickName.text = data:GetValue(CS.ConstDefine.NickName);
        self.lblLevel.text = string.format("Lv.%s", data:GetValue(CS.ConstDefine.Level));
        self.lblFighting.text = string.format("hha战斗力：<color='#ff0000'>{0}</color>", data:GetValue(CS.ConstDefine.Fighting));
	end
)