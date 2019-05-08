#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class LuaHelperWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(LuaHelper), L, translator, 0, 5, 3, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetData", _m_GetData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateMemoryStream", _m_CreateMemoryStream);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendProto", _m_SendProto);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddListener", _m_AddListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEventHandler", _m_RemoveEventHandler);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiSceneCtrl", _g_get_uiSceneCtrl);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiViewUtil", _g_get_uiViewUtil);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "loaderMgr", _g_get_loaderMgr);
            
			
			Utils.EndObjectRegister(typeof(LuaHelper), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(LuaHelper), L, __CreateInstance, 1, 0, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(LuaHelper));
			
			
			Utils.EndClassRegister(typeof(LuaHelper), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			try {
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					LuaHelper __cl_gen_ret = new LuaHelper();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LuaHelper constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetData(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    
                        GameDataTableToLua __cl_gen_ret = __cl_gen_to_be_invoked.GetData( name );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateMemoryStream(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
			int __gen_param_count = LuaAPI.lua_gettop(L);
            
            try {
                if(__gen_param_count == 1) 
                {
                    
                        MMO_MemoryStream __cl_gen_ret = __cl_gen_to_be_invoked.CreateMemoryStream(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    byte[] buffer = LuaAPI.lua_tobytes(L, 2);
                    
                        MMO_MemoryStream __cl_gen_ret = __cl_gen_to_be_invoked.CreateMemoryStream( buffer );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaHelper.CreateMemoryStream!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendProto(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    byte[] buffer = LuaAPI.lua_tobytes(L, 2);
                    
                    __cl_gen_to_be_invoked.SendProto( buffer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddListener(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    ushort protoCode = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    SocketDispatcher.OnActionHandler listener = translator.GetDelegate<SocketDispatcher.OnActionHandler>(L, 3);
                    
                    __cl_gen_to_be_invoked.AddListener( protoCode, listener );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEventHandler(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    ushort protoCode = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    SocketDispatcher.OnActionHandler listener = translator.GetDelegate<SocketDispatcher.OnActionHandler>(L, 3);
                    
                    __cl_gen_to_be_invoked.RemoveEventHandler( protoCode, listener );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiSceneCtrl(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.uiSceneCtrl);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiViewUtil(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.uiViewUtil);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_loaderMgr(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                LuaHelper __cl_gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.loaderMgr);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
