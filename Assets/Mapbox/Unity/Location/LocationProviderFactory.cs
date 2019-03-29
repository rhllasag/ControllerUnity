#if NETFX_CORE && !UNITY_EDITOR
#define NETFX_CORE
#endif
#if BUILD_FOR_WP8 && !UNITY_EDITOR
#define BUILD_FOR_WP8
#endif
namespace Mapbox.Unity.Location
{
	using UnityEngine;
	using Mapbox.Unity.Map;
	using System.Text.RegularExpressions;
	using Mapbox.Unity.Utilities;
	using Mapbox.Utils;

	/// <summary>
	/// Singleton factory to allow easy access to various LocationProviders.
	/// This is meant to be attached to a game object.
	/// </summary>
	public class LocationProviderFactory : MonoBehaviour
	{
		[SerializeField]
		public AbstractMap mapManager;

		[SerializeField]
		[Tooltip("Provider using Unity's builtin 'Input.Location' service")]
		AbstractLocationProvider _deviceLocationProviderUnity;

		[SerializeField]
		[Tooltip("Custom native Android location provider. If this is not set above provider is used")]
		DeviceLocationProviderAndroidNative _deviceLocationProviderAndroid;

		[SerializeField]
		AbstractLocationProvider _editorLocationProvider;

		[SerializeField]
		AbstractLocationProvider _transformLocationProvider;

		[SerializeField]
		bool _dontDestroyOnLoad;


		/// <summary>
		/// The singleton instance of this factory.
		/// </summary>
		private static LocationProviderFactory _instance;
		public static LocationProviderFactory Instance
		{
			get
			{
				return _instance;
			}

			private set
			{
				_instance = value;
			}
		}

		ILocationProvider _defaultLocationProvider;

		/// <summary>
		/// The default location provider. 
		/// Outside of the editor, this will be a <see cref="T:Mapbox.Unity.Location.DeviceLocationProvider"/>.
		/// In the Unity editor, this will be an <see cref="T:Mapbox.Unity.Location.EditorLocationProvider"/>
		/// </summary>
		/// <example>
		/// Fetch location to set a transform's position:
		/// <code>
		///
		float scalex=200, scalez=200;
		public void OnUpdate(Vector2d location)
		 {
			Vector3 coordinates = Conversions.GeoToWorldPosition(location - mapManager.CenterLatitudeLongitude - new Vector2d(0.0007,0),
														mapManager.CenterLatitudeLongitude,
														mapManager.WorldRelativeScale).ToVector3xz();
			coordinates.x /= scalex;
			coordinates.z /= scalez;
			GameObject.Find("Drone").transform.position = GameObject.Find("Sphere").transform.position+coordinates;
		}
		/// </code>
		/// </example>
		public ILocationProvider DefaultLocationProvider
		{
			get
			{
				return _defaultLocationProvider;
			}
			set
			{
				_defaultLocationProvider = value;
			}
		}

		/// <summary>
		/// Returns the serialized <see cref="T:Mapbox.Unity.Location.TransformLocationProvider"/>.
		/// </summary>
		public ILocationProvider TransformLocationProvider
		{
			get
			{
				return _transformLocationProvider;
			}
		}

		/// <summary>
		/// Returns the serialized <see cref="T:Mapbox.Unity.Location.EditorLocationProvider"/>.
		/// </summary>
		public ILocationProvider EditorLocationProvider
		{
			get
			{
				return _editorLocationProvider;
			}
		}

		/// <summary>
		/// Returns the serialized <see cref="T:Mapbox.Unity.Location.DeviceLocationProvider"/>
		/// </summary>
		public ILocationProvider DeviceLocationProvider
		{
			get
			{
				return _deviceLocationProviderUnity;
			}
		}

		/// <summary>
		/// Create singleton instance and inject the DefaultLocationProvider upon initialization of this component. 
		/// </summary>
		protected virtual void Awake()
		{
			Debug.Log(GameObject.Find("Drone").transform.position);

			if (Instance != null)
			{
				//DestroyImmediate(gameObject);
				return;
			}
			Instance = this;

			if (_dontDestroyOnLoad)
			{
				DontDestroyOnLoad(gameObject);
			}

			InjectEditorLocationProvider();
			InjectDeviceLocationProvider();
			InjectDeviceLocationProviderWP8();
		}

		/// <summary>
		/// Injects the editor location provider.
		/// Depending on the platform, this method and calls to it will be stripped during compile.
		/// </summary>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		void InjectEditorLocationProvider()
		{
			Debug.LogFormat("LocationProviderFactory: Injected EDITOR Location Provider - {0}", _editorLocationProvider.GetType());
			DefaultLocationProvider = _editorLocationProvider;
		}

		/// <summary>
		/// Injects the device location provider.
		/// Depending on the platform, this method and calls to it will be stripped during compile.
		/// </summary>
		[System.Diagnostics.Conditional("NETFX_CORE")]
		void InjectDeviceLocationProvider()
		{
			Debug.LogFormat("LocationProviderFactory: Injected EDITOR Location Provider - {0}", _editorLocationProvider.GetType());
			DefaultLocationProvider = _editorLocationProvider;
		}
		[System.Diagnostics.Conditional("BUILD_FOR_WP8")]
		void InjectDeviceLocationProviderWP8()
		{
			Debug.LogFormat("LocationProviderFactory: Injected EDITOR Location Provider - {0}", _editorLocationProvider.GetType());
			DefaultLocationProvider = _editorLocationProvider;
		}
	}
}
