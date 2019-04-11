GoodsEquipView = {};

local this = GoodsEquipView;

local transform;
local gameObject;

function GoodsEquipView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.InitView();
end

function GoodsEquipView.InitView()
	--�ҵ�UI���
	
	this.lblGoodsName = transform:Find("lblGoodsName"):GetComponent("UnityEngine.UI.Text"); --��Ʒ����
	this.lblLevel = transform:Find("lblLevel"):GetComponent("UnityEngine.UI.Text"); --��Ʒʹ�õȼ�
	this.lblGoodsType = transform:Find("lblGoodsType"):GetComponent("UnityEngine.UI.Text"); --��Ʒ��������
	this.imgIcon = transform:Find("GoodsIcon/imgIcon"):GetComponent("UnityEngine.UI.Image"); --��Ʒͼ��
	
	this.imgJewel1 = transform:Find("imgJewelContainer/imgJewelHole1/imgJewel"):GetComponent("UnityEngine.UI.Image"); --��ʯ1
	this.imgJewel2 = transform:Find("imgJewelContainer/imgJewelHole2/imgJewel"):GetComponent("UnityEngine.UI.Image"); --��ʯ2
	this.imgJewel3 = transform:Find("imgJewelContainer/imgJewelHole3/imgJewel"):GetComponent("UnityEngine.UI.Image"); --��ʯ3
	this.imgJewel4 = transform:Find("imgJewelContainer/imgJewelHole4/imgJewel"):GetComponent("UnityEngine.UI.Image"); --��ʯ4
	
	this.attrContent = transform:Find("Scroll View/Viewport/Content"); --��������
	
	this.btnPutOn = transform:Find("btnPutOn"):GetComponent("UnityEngine.UI.Button"); --����
	this.txtPutOn = transform:Find("btnPutOn/Text"):GetComponent("UnityEngine.UI.Text"); --�������°�ť�ı�
	this.btnSell = transform:Find("btnSell"):GetComponent("UnityEngine.UI.Button"); --����
end

function GoodsEquipView.start()

end

function GoodsEquipView.update()

end

function GoodsEquipView.ondestroy()
	GoodsEquipCtrl.OnDestroy();
end