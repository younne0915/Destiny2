mymetatable = {}
--mytable = setmetatable({key1 = "value1"}, { __newindex = mymetatable })
mytable = setmetatable({key1 = "value1"}, { __newindex = mymetatable })
print('Test ==============')
print(mytable.key1)

mytable.newkey = "新值2"
print(mytable.newkey,mymetatable.newkey)

mytable.key1 = "新值1"
print(mytable.key1,mymetatable.newkey1)
print('Test ==============')

--value1
--新值2 nil
--新值1 nil

