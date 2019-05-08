#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter((luaenv, translator) => {
			    
				translator.DelayWrapLoader(typeof(SocketDispatcher), SocketDispatcherWrap.__Register);
				
				translator.DelayWrapLoader(typeof(TransferData), TransferDataWrap.__Register);
				
				translator.DelayWrapLoader(typeof(LuaWindowBehaviour), LuaWindowBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.AutoPlay), DGTweeningAutoPlayWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.AxisConstraint), DGTweeningAxisConstraintWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Ease), DGTweeningEaseWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.LogBehaviour), DGTweeningLogBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.LoopType), DGTweeningLoopTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.PathMode), DGTweeningPathModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.PathType), DGTweeningPathTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.RotateMode), DGTweeningRotateModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ScrambleMode), DGTweeningScrambleModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenType), DGTweeningTweenTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.UpdateType), DGTweeningUpdateTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.DOTween), DGTweeningDOTweenWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.DOVirtual), DGTweeningDOVirtualWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.EaseFactory), DGTweeningEaseFactoryWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Tweener), DGTweeningTweenerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Tween), DGTweeningTweenWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Sequence), DGTweeningSequenceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenParams), DGTweeningTweenParamsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Core.ABSSequentiable), DGTweeningCoreABSSequentiableWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions>), DGTweeningCoreTweenerCore_3_UnityEngineVector3UnityEngineVector3DGTweeningPluginsOptionsVectorOptions_Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenExtensions), DGTweeningTweenExtensionsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenSettingsExtensions), DGTweeningTweenSettingsExtensionsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions), DGTweeningShortcutExtensionsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions43), DGTweeningShortcutExtensions43Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions46), DGTweeningShortcutExtensions46Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions50), DGTweeningShortcutExtensions50Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(LuaHelper), LuaHelperWrap.__Register);
				
				translator.DelayWrapLoader(typeof(LuaBehaviour), LuaBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XLuaTest.Pedding), XLuaTestPeddingWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XLuaTest.MyStruct), XLuaTestMyStructWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XLuaTest.MyEnum), XLuaTestMyEnumWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XLuaTest.NoGc), XLuaTestNoGcWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Coroutine_Runner), Coroutine_RunnerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.WaitForSeconds), UnityEngineWaitForSecondsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.WWW), UnityEngineWWWWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Foo1Parent), Foo1ParentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Foo2Parent), Foo2ParentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Foo1Child), Foo1ChildWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Foo2Child), Foo2ChildWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Foo), FooWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FooExtension), FooExtensionWrap.__Register);
				
				translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Object), UnityEngineObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector2), UnityEngineVector2Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector3), UnityEngineVector3Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector4), UnityEngineVector4Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Quaternion), UnityEngineQuaternionWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Color), UnityEngineColorWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Ray), UnityEngineRayWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Bounds), UnityEngineBoundsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Ray2D), UnityEngineRay2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.GameObject), UnityEngineGameObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Component), UnityEngineComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Behaviour), UnityEngineBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Transform), UnityEngineTransformWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Resources), UnityEngineResourcesWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.TextAsset), UnityEngineTextAssetWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Keyframe), UnityEngineKeyframeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.AnimationCurve), UnityEngineAnimationCurveWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.AnimationClip), UnityEngineAnimationClipWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.MonoBehaviour), UnityEngineMonoBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystem), UnityEngineParticleSystemWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.SkinnedMeshRenderer), UnityEngineSkinnedMeshRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Renderer), UnityEngineRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.Collections.Generic.List<int>), SystemCollectionsGenericList_1_SystemInt32_Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Debug), UnityEngineDebugWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Tutorial.DrivenClass), TutorialDrivenClassWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Tutorial.DrivenClass.TestEnumInner), TutorialDrivenClassTestEnumInnerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Tutorial.Calc), TutorialCalcWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Tutorial.DrivenClassExtensions), TutorialDrivenClassExtensionsWrap.__Register);
				
				
				translator.AddInterfaceBridgeCreator(typeof(InvokeLua.ICalc), InvokeLuaICalcBridge.__Create);
				
				translator.AddInterfaceBridgeCreator(typeof(XLuaTest.IExchanger), XLuaTestIExchangerBridge.__Create);
				
				translator.AddInterfaceBridgeCreator(typeof(System.Collections.IEnumerator), SystemCollectionsIEnumeratorBridge.__Create);
				
				translator.AddInterfaceBridgeCreator(typeof(CSCallLua.ItfD), CSCallLuaItfDBridge.__Create);
				
			});
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	public static partial class Utils
    {
	    
		delegate void __GEN_DELEGATE0( DG.Tweening.Tween t);
		
		delegate void __GEN_DELEGATE1( DG.Tweening.Tween t);
		
		delegate void __GEN_DELEGATE2( DG.Tweening.Tween t);
		
		delegate void __GEN_DELEGATE3( DG.Tweening.Tween t,  float to,  bool andPlay);
		
		delegate void __GEN_DELEGATE4( DG.Tweening.Tween t,  bool complete);
		
		delegate void __GEN_DELEGATE5( DG.Tweening.Tween t);
		
		delegate void __GEN_DELEGATE6( DG.Tweening.Tween t);
		
		delegate void __GEN_DELEGATE7( DG.Tweening.Tween t,  bool includeDelay);
		
		delegate void __GEN_DELEGATE8( DG.Tweening.Tween t,  bool includeDelay);
		
		delegate void __GEN_DELEGATE9( DG.Tweening.Tween t);
		
		delegate void __GEN_DELEGATE10( DG.Tweening.Tween t,  int waypointIndex,  bool andPlay);
		
		delegate UnityEngine.YieldInstruction __GEN_DELEGATE11( DG.Tweening.Tween t);
		
		delegate UnityEngine.YieldInstruction __GEN_DELEGATE12( DG.Tweening.Tween t);
		
		delegate UnityEngine.YieldInstruction __GEN_DELEGATE13( DG.Tweening.Tween t);
		
		delegate UnityEngine.YieldInstruction __GEN_DELEGATE14( DG.Tweening.Tween t,  int elapsedLoops);
		
		delegate UnityEngine.YieldInstruction __GEN_DELEGATE15( DG.Tweening.Tween t,  float position);
		
		delegate UnityEngine.Coroutine __GEN_DELEGATE16( DG.Tweening.Tween t);
		
		delegate int __GEN_DELEGATE17( DG.Tweening.Tween t);
		
		delegate float __GEN_DELEGATE18( DG.Tweening.Tween t);
		
		delegate float __GEN_DELEGATE19( DG.Tweening.Tween t,  bool includeLoops);
		
		delegate float __GEN_DELEGATE20( DG.Tweening.Tween t,  bool includeLoops);
		
		delegate float __GEN_DELEGATE21( DG.Tweening.Tween t,  bool includeLoops);
		
		delegate float __GEN_DELEGATE22( DG.Tweening.Tween t);
		
		delegate bool __GEN_DELEGATE23( DG.Tweening.Tween t);
		
		delegate bool __GEN_DELEGATE24( DG.Tweening.Tween t);
		
		delegate bool __GEN_DELEGATE25( DG.Tweening.Tween t);
		
		delegate bool __GEN_DELEGATE26( DG.Tweening.Tween t);
		
		delegate bool __GEN_DELEGATE27( DG.Tweening.Tween t);
		
		delegate UnityEngine.Vector3 __GEN_DELEGATE28( DG.Tweening.Tween t,  float pathPercentage);
		
		delegate float __GEN_DELEGATE29( DG.Tweening.Tween t);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE30( DG.Tweening.Sequence s,  DG.Tweening.Tween t);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE31( DG.Tweening.Sequence s,  DG.Tweening.Tween t);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE32( DG.Tweening.Sequence s,  DG.Tweening.Tween t);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE33( DG.Tweening.Sequence s,  float atPosition,  DG.Tweening.Tween t);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE34( DG.Tweening.Sequence s,  float interval);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE35( DG.Tweening.Sequence s,  float interval);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE36( DG.Tweening.Sequence s,  DG.Tweening.TweenCallback callback);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE37( DG.Tweening.Sequence s,  DG.Tweening.TweenCallback callback);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE38( DG.Tweening.Sequence s,  float atPosition,  DG.Tweening.TweenCallback callback);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE39( DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> t,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE40( DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> t,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE41( DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> t,  DG.Tweening.AxisConstraint axisConstraint,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE42( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> t,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE43( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> t,  DG.Tweening.AxisConstraint axisConstraint,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE44( DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> t,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE45( DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> t,  DG.Tweening.AxisConstraint axisConstraint,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE46( DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> t,  bool useShortest360Route);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE47( DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> t,  bool alphaOnly);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE48( DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> t,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE49( DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> t,  bool richTextEnabled,  DG.Tweening.ScrambleMode scrambleMode,  string scrambleChars);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE50( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> t,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE51( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> t,  DG.Tweening.AxisConstraint axisConstraint,  bool snapping);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE52( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t,  DG.Tweening.AxisConstraint lockPosition,  DG.Tweening.AxisConstraint lockRotation);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE53( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t,  bool closePath,  DG.Tweening.AxisConstraint lockPosition,  DG.Tweening.AxisConstraint lockRotation);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE54( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t,  UnityEngine.Vector3 lookAtPosition,  System.Nullable<UnityEngine.Vector3> forwardDirection,  System.Nullable<UnityEngine.Vector3> up);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE55( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t,  UnityEngine.Transform lookAtTransform,  System.Nullable<UnityEngine.Vector3> forwardDirection,  System.Nullable<UnityEngine.Vector3> up);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE56( DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t,  float lookAhead,  System.Nullable<UnityEngine.Vector3> forwardDirection,  System.Nullable<UnityEngine.Vector3> up);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE57( UnityEngine.AudioSource target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE58( UnityEngine.AudioSource target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE59( UnityEngine.Camera target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE60( UnityEngine.Camera target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE61( UnityEngine.Camera target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE62( UnityEngine.Camera target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE63( UnityEngine.Camera target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE64( UnityEngine.Camera target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE65( UnityEngine.Camera target,  UnityEngine.Rect endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE66( UnityEngine.Camera target,  UnityEngine.Rect endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE67( UnityEngine.Camera target,  float duration,  float strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE68( UnityEngine.Camera target,  float duration,  UnityEngine.Vector3 strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE69( UnityEngine.Camera target,  float duration,  float strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE70( UnityEngine.Camera target,  float duration,  UnityEngine.Vector3 strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE71( UnityEngine.Light target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE72( UnityEngine.Light target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE73( UnityEngine.Light target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE74( UnityEngine.LineRenderer target,  DG.Tweening.Color2 startValue,  DG.Tweening.Color2 endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE75( UnityEngine.Material target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE76( UnityEngine.Material target,  UnityEngine.Color endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE77( UnityEngine.Material target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE78( UnityEngine.Material target,  float endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE79( UnityEngine.Material target,  float endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE80( UnityEngine.Material target,  UnityEngine.Vector2 endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE81( UnityEngine.Material target,  UnityEngine.Vector2 endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE82( UnityEngine.Material target,  UnityEngine.Vector2 endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE83( UnityEngine.Material target,  UnityEngine.Vector2 endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE84( UnityEngine.Material target,  UnityEngine.Vector4 endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE85( UnityEngine.Rigidbody target,  UnityEngine.Vector3 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE86( UnityEngine.Rigidbody target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE87( UnityEngine.Rigidbody target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE88( UnityEngine.Rigidbody target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE89( UnityEngine.Rigidbody target,  UnityEngine.Vector3 endValue,  float duration,  DG.Tweening.RotateMode mode);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE90( UnityEngine.Rigidbody target,  UnityEngine.Vector3 towards,  float duration,  DG.Tweening.AxisConstraint axisConstraint,  System.Nullable<UnityEngine.Vector3> up);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE91( UnityEngine.Rigidbody target,  UnityEngine.Vector3 endValue,  float jumpPower,  int numJumps,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE92( UnityEngine.TrailRenderer target,  float toStartWidth,  float toEndWidth,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE93( UnityEngine.TrailRenderer target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE94( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE95( UnityEngine.Transform target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE96( UnityEngine.Transform target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE97( UnityEngine.Transform target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE98( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE99( UnityEngine.Transform target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE100( UnityEngine.Transform target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE101( UnityEngine.Transform target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE102( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float duration,  DG.Tweening.RotateMode mode);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE103( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float duration,  DG.Tweening.RotateMode mode);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE104( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE105( UnityEngine.Transform target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE106( UnityEngine.Transform target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE107( UnityEngine.Transform target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE108( UnityEngine.Transform target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE109( UnityEngine.Transform target,  UnityEngine.Vector3 towards,  float duration,  DG.Tweening.AxisConstraint axisConstraint,  System.Nullable<UnityEngine.Vector3> up);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE110( UnityEngine.Transform target,  UnityEngine.Vector3 punch,  float duration,  int vibrato,  float elasticity,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE111( UnityEngine.Transform target,  UnityEngine.Vector3 punch,  float duration,  int vibrato,  float elasticity);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE112( UnityEngine.Transform target,  UnityEngine.Vector3 punch,  float duration,  int vibrato,  float elasticity);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE113( UnityEngine.Transform target,  float duration,  float strength,  int vibrato,  float randomness,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE114( UnityEngine.Transform target,  float duration,  UnityEngine.Vector3 strength,  int vibrato,  float randomness,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE115( UnityEngine.Transform target,  float duration,  float strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE116( UnityEngine.Transform target,  float duration,  UnityEngine.Vector3 strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE117( UnityEngine.Transform target,  float duration,  float strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE118( UnityEngine.Transform target,  float duration,  UnityEngine.Vector3 strength,  int vibrato,  float randomness);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE119( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float jumpPower,  int numJumps,  float duration,  bool snapping);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE120( UnityEngine.Transform target,  UnityEngine.Vector3 endValue,  float jumpPower,  int numJumps,  float duration,  bool snapping);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE121( UnityEngine.Transform target,  UnityEngine.Vector3[] path,  float duration,  DG.Tweening.PathType pathType,  DG.Tweening.PathMode pathMode,  int resolution,  System.Nullable<UnityEngine.Color> gizmoColor);
		
		delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE122( UnityEngine.Transform target,  UnityEngine.Vector3[] path,  float duration,  DG.Tweening.PathType pathType,  DG.Tweening.PathMode pathMode,  int resolution,  System.Nullable<UnityEngine.Color> gizmoColor);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE123( UnityEngine.Light target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE124( UnityEngine.Material target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE125( UnityEngine.Material target,  UnityEngine.Color endValue,  string property,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE126( UnityEngine.Transform target,  UnityEngine.Vector3 byValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE127( UnityEngine.Transform target,  UnityEngine.Vector3 byValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE128( UnityEngine.Transform target,  UnityEngine.Vector3 byValue,  float duration,  DG.Tweening.RotateMode mode);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE129( UnityEngine.Transform target,  UnityEngine.Vector3 byValue,  float duration,  DG.Tweening.RotateMode mode);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE130( UnityEngine.Transform target,  UnityEngine.Vector3 byValue,  float duration);
		
		delegate int __GEN_DELEGATE131( UnityEngine.Component target);
		
		delegate int __GEN_DELEGATE132( UnityEngine.Component target,  bool complete);
		
		delegate int __GEN_DELEGATE133( UnityEngine.Component target);
		
		delegate int __GEN_DELEGATE134( UnityEngine.Component target,  float to,  bool andPlay);
		
		delegate int __GEN_DELEGATE135( UnityEngine.Component target);
		
		delegate int __GEN_DELEGATE136( UnityEngine.Component target);
		
		delegate int __GEN_DELEGATE137( UnityEngine.Component target);
		
		delegate int __GEN_DELEGATE138( UnityEngine.Component target);
		
		delegate int __GEN_DELEGATE139( UnityEngine.Component target,  bool includeDelay);
		
		delegate int __GEN_DELEGATE140( UnityEngine.Component target,  bool includeDelay);
		
		delegate int __GEN_DELEGATE141( UnityEngine.Component target);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE142( UnityEngine.SpriteRenderer target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE143( UnityEngine.SpriteRenderer target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE144( UnityEngine.Rigidbody2D target,  UnityEngine.Vector2 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE145( UnityEngine.Rigidbody2D target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE146( UnityEngine.Rigidbody2D target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE147( UnityEngine.Rigidbody2D target,  float endValue,  float duration);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE148( UnityEngine.Rigidbody2D target,  UnityEngine.Vector2 endValue,  float jumpPower,  int numJumps,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE149( UnityEngine.SpriteRenderer target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE150( UnityEngine.CanvasGroup target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE151( UnityEngine.UI.Graphic target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE152( UnityEngine.UI.Graphic target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE153( UnityEngine.UI.Image target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE154( UnityEngine.UI.Image target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE155( UnityEngine.UI.Image target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE156( UnityEngine.UI.LayoutElement target,  UnityEngine.Vector2 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE157( UnityEngine.UI.LayoutElement target,  UnityEngine.Vector2 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE158( UnityEngine.UI.LayoutElement target,  UnityEngine.Vector2 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE159( UnityEngine.UI.Outline target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE160( UnityEngine.UI.Outline target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE161( UnityEngine.UI.Outline target,  UnityEngine.Vector2 endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE162( UnityEngine.RectTransform target,  UnityEngine.Vector2 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE163( UnityEngine.RectTransform target,  UnityEngine.Vector3 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE164( UnityEngine.RectTransform target,  UnityEngine.Vector2 endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE165( UnityEngine.RectTransform target,  UnityEngine.Vector2 punch,  float duration,  int vibrato,  float elasticity,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE166( UnityEngine.RectTransform target,  float duration,  float strength,  int vibrato,  float randomness,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE167( UnityEngine.RectTransform target,  float duration,  UnityEngine.Vector2 strength,  int vibrato,  float randomness,  bool snapping);
		
		delegate DG.Tweening.Sequence __GEN_DELEGATE168( UnityEngine.RectTransform target,  UnityEngine.Vector2 endValue,  float jumpPower,  int numJumps,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE169( UnityEngine.UI.Slider target,  float endValue,  float duration,  bool snapping);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE170( UnityEngine.UI.Text target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE171( UnityEngine.UI.Text target,  float endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE172( UnityEngine.UI.Text target,  string endValue,  float duration,  bool richTextEnabled,  DG.Tweening.ScrambleMode scrambleMode,  string scrambleChars);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE173( UnityEngine.UI.Graphic target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE174( UnityEngine.UI.Image target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE175( UnityEngine.UI.Text target,  UnityEngine.Color endValue,  float duration);
		
		delegate DG.Tweening.Tweener __GEN_DELEGATE176( UnityEngine.Audio.AudioMixer target,  string floatName,  float endValue,  float duration);
		
		delegate int __GEN_DELEGATE177( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE178( UnityEngine.Audio.AudioMixer target,  bool complete);
		
		delegate int __GEN_DELEGATE179( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE180( UnityEngine.Audio.AudioMixer target,  float to,  bool andPlay);
		
		delegate int __GEN_DELEGATE181( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE182( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE183( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE184( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE185( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE186( UnityEngine.Audio.AudioMixer target);
		
		delegate int __GEN_DELEGATE187( UnityEngine.Audio.AudioMixer target);
		
	    static Utils()
		{
		    extension_method_map = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
				{typeof(DG.Tweening.Tween), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE0(DG.Tweening.TweenExtensions.Complete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE1(DG.Tweening.TweenExtensions.Flip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE2(DG.Tweening.TweenExtensions.ForceInit)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE3(DG.Tweening.TweenExtensions.Goto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE4(DG.Tweening.TweenExtensions.Kill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE5(DG.Tweening.TweenExtensions.PlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE6(DG.Tweening.TweenExtensions.PlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE7(DG.Tweening.TweenExtensions.Restart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE8(DG.Tweening.TweenExtensions.Rewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE9(DG.Tweening.TweenExtensions.TogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE10(DG.Tweening.TweenExtensions.GotoWaypoint)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE11(DG.Tweening.TweenExtensions.WaitForCompletion)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE12(DG.Tweening.TweenExtensions.WaitForRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE13(DG.Tweening.TweenExtensions.WaitForKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE14(DG.Tweening.TweenExtensions.WaitForElapsedLoops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE15(DG.Tweening.TweenExtensions.WaitForPosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE16(DG.Tweening.TweenExtensions.WaitForStart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE17(DG.Tweening.TweenExtensions.CompletedLoops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE18(DG.Tweening.TweenExtensions.Delay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE19(DG.Tweening.TweenExtensions.Duration)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE20(DG.Tweening.TweenExtensions.Elapsed)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE21(DG.Tweening.TweenExtensions.ElapsedPercentage)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE22(DG.Tweening.TweenExtensions.ElapsedDirectionalPercentage)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE23(DG.Tweening.TweenExtensions.IsActive)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE24(DG.Tweening.TweenExtensions.IsBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE25(DG.Tweening.TweenExtensions.IsComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE26(DG.Tweening.TweenExtensions.IsInitialized)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE27(DG.Tweening.TweenExtensions.IsPlaying)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE28(DG.Tweening.TweenExtensions.PathGetPoint)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE29(DG.Tweening.TweenExtensions.PathLength)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Sequence), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE30(DG.Tweening.TweenSettingsExtensions.Append)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE31(DG.Tweening.TweenSettingsExtensions.Prepend)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE32(DG.Tweening.TweenSettingsExtensions.Join)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE33(DG.Tweening.TweenSettingsExtensions.Insert)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE34(DG.Tweening.TweenSettingsExtensions.AppendInterval)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE35(DG.Tweening.TweenSettingsExtensions.PrependInterval)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE36(DG.Tweening.TweenSettingsExtensions.AppendCallback)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE37(DG.Tweening.TweenSettingsExtensions.PrependCallback)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE38(DG.Tweening.TweenSettingsExtensions.InsertCallback)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE39(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE40(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE41(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE42(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE43(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE44(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE45(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE46(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE47(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE48(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE49(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE50(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE51(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions>), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE52(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE53(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE54(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE55(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE56(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AudioSource), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE57(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE58(DG.Tweening.ShortcutExtensions.DOPitch)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Camera), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE59(DG.Tweening.ShortcutExtensions.DOAspect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE60(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE61(DG.Tweening.ShortcutExtensions.DOFarClipPlane)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE62(DG.Tweening.ShortcutExtensions.DOFieldOfView)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE63(DG.Tweening.ShortcutExtensions.DONearClipPlane)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE64(DG.Tweening.ShortcutExtensions.DOOrthoSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE65(DG.Tweening.ShortcutExtensions.DOPixelRect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE66(DG.Tweening.ShortcutExtensions.DORect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE67(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE68(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE69(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE70(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Light), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE71(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE72(DG.Tweening.ShortcutExtensions.DOIntensity)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE73(DG.Tweening.ShortcutExtensions.DOShadowStrength)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE123(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.LineRenderer), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE74(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Material), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE75(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE76(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE77(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE78(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE79(DG.Tweening.ShortcutExtensions.DOFloat)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE80(DG.Tweening.ShortcutExtensions.DOOffset)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE81(DG.Tweening.ShortcutExtensions.DOOffset)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE82(DG.Tweening.ShortcutExtensions.DOTiling)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE83(DG.Tweening.ShortcutExtensions.DOTiling)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE84(DG.Tweening.ShortcutExtensions.DOVector)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE124(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE125(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Rigidbody), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE85(DG.Tweening.ShortcutExtensions.DOMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE86(DG.Tweening.ShortcutExtensions.DOMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE87(DG.Tweening.ShortcutExtensions.DOMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE88(DG.Tweening.ShortcutExtensions.DOMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE89(DG.Tweening.ShortcutExtensions.DORotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE90(DG.Tweening.ShortcutExtensions.DOLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE91(DG.Tweening.ShortcutExtensions.DOJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.TrailRenderer), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE92(DG.Tweening.ShortcutExtensions.DOResize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE93(DG.Tweening.ShortcutExtensions.DOTime)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Transform), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE94(DG.Tweening.ShortcutExtensions.DOMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE95(DG.Tweening.ShortcutExtensions.DOMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE96(DG.Tweening.ShortcutExtensions.DOMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE97(DG.Tweening.ShortcutExtensions.DOMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE98(DG.Tweening.ShortcutExtensions.DOLocalMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE99(DG.Tweening.ShortcutExtensions.DOLocalMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE100(DG.Tweening.ShortcutExtensions.DOLocalMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE101(DG.Tweening.ShortcutExtensions.DOLocalMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE102(DG.Tweening.ShortcutExtensions.DORotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE103(DG.Tweening.ShortcutExtensions.DOLocalRotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE104(DG.Tweening.ShortcutExtensions.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE105(DG.Tweening.ShortcutExtensions.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE106(DG.Tweening.ShortcutExtensions.DOScaleX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE107(DG.Tweening.ShortcutExtensions.DOScaleY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE108(DG.Tweening.ShortcutExtensions.DOScaleZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE109(DG.Tweening.ShortcutExtensions.DOLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE110(DG.Tweening.ShortcutExtensions.DOPunchPosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE111(DG.Tweening.ShortcutExtensions.DOPunchScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE112(DG.Tweening.ShortcutExtensions.DOPunchRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE113(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE114(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE115(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE116(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE117(DG.Tweening.ShortcutExtensions.DOShakeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE118(DG.Tweening.ShortcutExtensions.DOShakeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE119(DG.Tweening.ShortcutExtensions.DOJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE120(DG.Tweening.ShortcutExtensions.DOLocalJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE121(DG.Tweening.ShortcutExtensions.DOPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE122(DG.Tweening.ShortcutExtensions.DOLocalPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE126(DG.Tweening.ShortcutExtensions.DOBlendableMoveBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE127(DG.Tweening.ShortcutExtensions.DOBlendableLocalMoveBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE128(DG.Tweening.ShortcutExtensions.DOBlendableRotateBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE129(DG.Tweening.ShortcutExtensions.DOBlendableLocalRotateBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE130(DG.Tweening.ShortcutExtensions.DOBlendableScaleBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Component), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE131(DG.Tweening.ShortcutExtensions.DOComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE132(DG.Tweening.ShortcutExtensions.DOKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE133(DG.Tweening.ShortcutExtensions.DOFlip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE134(DG.Tweening.ShortcutExtensions.DOGoto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE135(DG.Tweening.ShortcutExtensions.DOPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE136(DG.Tweening.ShortcutExtensions.DOPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE137(DG.Tweening.ShortcutExtensions.DOPlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE138(DG.Tweening.ShortcutExtensions.DOPlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE139(DG.Tweening.ShortcutExtensions.DORestart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE140(DG.Tweening.ShortcutExtensions.DORewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE141(DG.Tweening.ShortcutExtensions.DOTogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.SpriteRenderer), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE142(DG.Tweening.ShortcutExtensions43.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE143(DG.Tweening.ShortcutExtensions43.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE149(DG.Tweening.ShortcutExtensions43.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Rigidbody2D), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE144(DG.Tweening.ShortcutExtensions43.DOMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE145(DG.Tweening.ShortcutExtensions43.DOMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE146(DG.Tweening.ShortcutExtensions43.DOMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE147(DG.Tweening.ShortcutExtensions43.DORotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE148(DG.Tweening.ShortcutExtensions43.DOJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.CanvasGroup), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE150(DG.Tweening.ShortcutExtensions46.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Graphic), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE151(DG.Tweening.ShortcutExtensions46.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE152(DG.Tweening.ShortcutExtensions46.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE173(DG.Tweening.ShortcutExtensions46.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Image), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE153(DG.Tweening.ShortcutExtensions46.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE154(DG.Tweening.ShortcutExtensions46.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE155(DG.Tweening.ShortcutExtensions46.DOFillAmount)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE174(DG.Tweening.ShortcutExtensions46.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.LayoutElement), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE156(DG.Tweening.ShortcutExtensions46.DOFlexibleSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE157(DG.Tweening.ShortcutExtensions46.DOMinSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE158(DG.Tweening.ShortcutExtensions46.DOPreferredSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Outline), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE159(DG.Tweening.ShortcutExtensions46.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE160(DG.Tweening.ShortcutExtensions46.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE161(DG.Tweening.ShortcutExtensions46.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.RectTransform), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE162(DG.Tweening.ShortcutExtensions46.DOAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE163(DG.Tweening.ShortcutExtensions46.DOAnchorPos3D)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE164(DG.Tweening.ShortcutExtensions46.DOSizeDelta)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE165(DG.Tweening.ShortcutExtensions46.DOPunchAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE166(DG.Tweening.ShortcutExtensions46.DOShakeAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE167(DG.Tweening.ShortcutExtensions46.DOShakeAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE168(DG.Tweening.ShortcutExtensions46.DOJumpAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Slider), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE169(DG.Tweening.ShortcutExtensions46.DOValue)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Text), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE170(DG.Tweening.ShortcutExtensions46.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE171(DG.Tweening.ShortcutExtensions46.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE172(DG.Tweening.ShortcutExtensions46.DOText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE175(DG.Tweening.ShortcutExtensions46.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Audio.AudioMixer), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE176(DG.Tweening.ShortcutExtensions50.DOSetFloat)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE177(DG.Tweening.ShortcutExtensions50.DOComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE178(DG.Tweening.ShortcutExtensions50.DOKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE179(DG.Tweening.ShortcutExtensions50.DOFlip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE180(DG.Tweening.ShortcutExtensions50.DOGoto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE181(DG.Tweening.ShortcutExtensions50.DOPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE182(DG.Tweening.ShortcutExtensions50.DOPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE183(DG.Tweening.ShortcutExtensions50.DOPlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE184(DG.Tweening.ShortcutExtensions50.DOPlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE185(DG.Tweening.ShortcutExtensions50.DORestart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE186(DG.Tweening.ShortcutExtensions50.DORewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE187(DG.Tweening.ShortcutExtensions50.DOTogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
			};
		}
	}
}
