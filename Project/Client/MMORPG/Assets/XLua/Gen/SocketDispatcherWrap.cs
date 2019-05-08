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
    public class SocketDispatcherWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(SocketDispatcher), L, translator, 0, 4, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEventHandler", _m_AddEventHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEventHandler", _m_RemoveEventHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispatch", _m_Dispatch);
			
			
			
			
			Utils.EndObjectRegister(typeof(SocketDispatcher), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(SocketDispatcher), L, __CreateInstance, 1, 1, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(SocketDispatcher));
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			Utils.EndClassRegister(typeof(SocketDispatcher), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			try {
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					SocketDispatcher __cl_gen_ret = new SocketDispatcher();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to SocketDispatcher constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            SocketDispatcher __cl_gen_to_be_invoked = (SocketDispatcher)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    
                    __cl_gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEventHandler(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            SocketDispatcher __cl_gen_to_be_invoked = (SocketDispatcher)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    ushort key = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    SocketDispatcher.OnActionHandler handler = translator.GetDelegate<SocketDispatcher.OnActionHandler>(L, 3);
                    
                    __cl_gen_to_be_invoked.AddEventHandler( key, handler );
                    
                    
                    
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
            
            
            SocketDispatcher __cl_gen_to_be_invoked = (SocketDispatcher)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    ushort key = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    SocketDispatcher.OnActionHandler handler = translator.GetDelegate<SocketDispatcher.OnActionHandler>(L, 3);
                    
                    __cl_gen_to_be_invoked.RemoveEventHandler( key, handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispatch(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            SocketDispatcher __cl_gen_to_be_invoked = (SocketDispatcher)translator.FastGetCSObj(L, 1);
            
            
            try {
                
                {
                    ushort key = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    byte[] param = LuaAPI.lua_tobytes(L, 3);
                    
                    __cl_gen_to_be_invoked.Dispatch( key, param );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			    translator.Push(L, SocketDispatcher.Instance);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
