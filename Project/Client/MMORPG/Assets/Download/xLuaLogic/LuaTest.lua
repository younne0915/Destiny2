

function TestNumber(x)
	return x*10
end

function TestString(x)
	return string.format("大家好%s", x)
end

function TestTable()
	local myTable = {}
--[[	myTable[1] = "aa"
	myTable[2] = "22"
	
	print(table.concat(myTable, "|"))--]]
	
--	table.remove(myTable, 1)
	myTable[1] = "aa"
	myTable.x = "shenmegui"
	
	myTable["年龄"] = 12
	
	print(table.concat(myTable, "|"))
	
--[[	for key,v in ipairs(myTable) do
		print(v)
	end--]]
	
	for key,v in pairs(myTable) do
	print(v)
	end

end

TestTable()

--print('来到了Lua'..TestTable())