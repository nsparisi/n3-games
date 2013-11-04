using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class TexturePropertiesConverter : ScriptableObject{

    [MenuItem("Tools/Texture/Make Bilinear")]
    static void ChangeToBilinear()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.filterMode = FilterMode.Bilinear;

            AssetDatabase.ImportAsset(path);
        }
    }

	[MenuItem("Tools/Texture/Change PVRTC to ATC")]
    static void ChangeTextureType1()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            if (textureImporter.textureFormat == TextureImporterFormat.PVRTC_RGBA4)
            {
                textureImporter.textureFormat = TextureImporterFormat.ATC_RGBA8;

                AssetDatabase.ImportAsset(path);
            }
			else if (textureImporter.textureFormat == TextureImporterFormat.PVRTC_RGBA2)
            {
                textureImporter.textureFormat = TextureImporterFormat.ATC_RGBA8;

                AssetDatabase.ImportAsset(path);
            }
			else if (textureImporter.textureFormat == TextureImporterFormat.PVRTC_RGB4)
            {
                textureImporter.textureFormat = TextureImporterFormat.ATC_RGB4;

                AssetDatabase.ImportAsset(path);
            }
			else if (textureImporter.textureFormat == TextureImporterFormat.PVRTC_RGB2)
            {
                textureImporter.textureFormat = TextureImporterFormat.ATC_RGB4;

                AssetDatabase.ImportAsset(path);
            }
        }
    }
	
	[MenuItem("Tools/Texture/Set Import Settings for ATC")]
	static void CompressForATC()
	{
		Object[] textures = GetSelectedTextures();
		Selection.objects = new Object[0];
		foreach(Texture2D texture in textures)
		{
			string path = AssetDatabase.GetAssetPath(texture);
			TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
			CompressCompressedForAndroid(textureImporter, TextureImporterFormat.ATC_RGBA8, TextureImporterFormat.ATC_RGBA8);
			AssetDatabase.ImportAsset(path);
		}
	}
	
	[MenuItem("Tools/Texture/Set Import Settings for DXT")]
	static void CompressForDXT()
	{
		Object[] textures = GetSelectedTextures();
		Selection.objects = new Object[0];
		foreach(Texture2D texture in textures)
		{
			string path = AssetDatabase.GetAssetPath(texture);
			TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
			CompressCompressedForAndroid(textureImporter, TextureImporterFormat.DXT5, TextureImporterFormat.DXT5);
			AssetDatabase.ImportAsset(path);
		}
	}
	
	[MenuItem("Tools/Texture/Set Import Settings for Power VR")]
	static void CompressForPowerVR()
	{
		Object[] textures = GetSelectedTextures();
		Selection.objects = new Object[0];
		foreach(Texture2D texture in textures)
		{
			string path = AssetDatabase.GetAssetPath(texture);
			TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
			CompressCompressedForAndroid(textureImporter, TextureImporterFormat.PVRTC_RGBA4, TextureImporterFormat.PVRTC_RGBA2);
			AssetDatabase.ImportAsset(path);
		}
	}
	
	static void CompressCompressedForAndroid(TextureImporter importer, TextureImporterFormat alphaHigh, TextureImporterFormat alphaLow)
	{
		TextureImporterFormat format = importer.textureFormat;
		
		switch(format)
		{
			case TextureImporterFormat.PVRTC_RGBA4:
				format = alphaHigh;
				importer.SetPlatformTextureSettings("Android", 1024, format);
				break;
			case TextureImporterFormat.PVRTC_RGBA2:
				format = alphaLow;
				importer.SetPlatformTextureSettings("Android", 1024, format);
				break;
			default:
				break;
		}
	}
	
	[MenuItem("Tools/Texture/Set Import Settings for Opaque Compressed As ETC")]
	static void CompressOpaquesAsETC()
	{
		foreach(Texture2D texture in GetSelectedTextures())
		{
			string path = AssetDatabase.GetAssetPath(texture);
			TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
			CompressCompressedOpaques(textureImporter, TextureImporterFormat.ETC_RGB4);
			AssetDatabase.ImportAsset(path);
		}
	}
	
	static void CompressCompressedOpaques(TextureImporter importer, TextureImporterFormat opaqueFormat)
	{
		TextureImporterFormat format = importer.textureFormat;
		if (format == TextureImporterFormat.PVRTC_RGB4 || format == TextureImporterFormat.PVRTC_RGB2)
		{
			importer.SetPlatformTextureSettings("Android", 1024, opaqueFormat);
		}
	}

    [MenuItem("Tools/Texture/Change ATCRGBA8 to PVRTCRGBA4")]
    static void ChangeTextureType2()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter.textureFormat == TextureImporterFormat.ATC_RGBA8)
            {
                textureImporter.textureFormat = TextureImporterFormat.PVRTC_RGBA4;

                AssetDatabase.ImportAsset(path);
            }
        }
    }
	
	 [MenuItem("Tools/Texture/Compress to 8-bit ATC (for Adreno chipsets)")]
    static void ForNook()
    {
		
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 1024;
            textureImporter.textureFormat = TextureImporterFormat.ATC_RGBA8;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }
    
     [MenuItem("Tools/Texture/Uncompress to ARBG32")]
    static void UncompressForNook()
    {
		
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 2048;
            textureImporter.textureFormat = TextureImporterFormat.ARGB32;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }
	
	 [MenuItem("Tools/Texture/Alpha8")]
	static void alpha8()
	{
		Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 1024;
            textureImporter.textureFormat = TextureImporterFormat.Alpha8;
			textureImporter.grayscaleToAlpha = true;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
	}
	
    [MenuItem("Tools/Texture/Compress to RGBA 16-bit - DOODADS")]
    static void ChangeTextureType()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 1024;
            textureImporter.textureFormat = TextureImporterFormat.ARGB16;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }
	
	[MenuItem("Tools/Texture/Compress to RGBA 16-bit, Bilinear - NPCs and Battlers")]
    static void ChangeTextureTypeB()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 1024;
            textureImporter.textureFormat = TextureImporterFormat.ARGB16;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }
	
	[MenuItem("Tools/Texture/Compress to RGB 16-bit - Tilesets")]
    static void ChangeTextureType4()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.mipmapEnabled = false;
            textureImporter.maxTextureSize = 1024;
            textureImporter.textureFormat = TextureImporterFormat.RGB16;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }
	
    [MenuItem("Tools/Texture/Compress PVRTC RGBA 4-bit, Bilinear - Portraits")]
    static void CompressTextureType()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.textureFormat = TextureImporterFormat.PVRTC_RGBA4;
			textureImporter.mipmapEnabled = false;
			textureImporter.normalmap = false;
			textureImporter.npotScale = TextureImporterNPOTScale.ToLarger;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }

    [MenuItem("Tools/Texture/Compress PVRTC RGBA 2-bit, Bilinear - Battle Animations")]
    static void CompressAsHardAsYouCan()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.textureFormat = TextureImporterFormat.PVRTC_RGBA2;
			textureImporter.mipmapEnabled = false;
			textureImporter.normalmap = false;
			textureImporter.npotScale = TextureImporterNPOTScale.ToLarger;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }    	
    }
	
	 [MenuItem("Tools/Texture/Compress PVRTC RGB 2-bit, Bilinear - Battle Backgrounds and Journal Maps")]
    static void CompressBattleBGsMaps()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.anisoLevel = 0;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.textureFormat = TextureImporterFormat.PVRTC_RGB2;
			textureImporter.mipmapEnabled = false;
			textureImporter.normalmap = false;
			textureImporter.npotScale = TextureImporterNPOTScale.ToLarger;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }    	
    }
	 
	  [MenuItem("Tools/Texture/Convert Powerof2 to nearest")]
    static void Powerof2()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            //Debug.Log("path: " + path);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            //all of our desired settings
			textureImporter.npotScale = TextureImporterNPOTScale.ToNearest;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Repeat;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }
	
    [MenuItem("Tools/Texture/Wrap Mode: Clamp")]
    static void ClampTexs()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            TextureImporterSettings st = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(st);
            st.wrapMode = TextureWrapMode.Clamp;
            textureImporter.SetTextureSettings(st);
            AssetDatabase.ImportAsset(path);
        }
    }

    static Object[] GetSelectedTextures()
    {
        return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    }

    private const string iPhoneName = "iPhone";
    

	
	[MenuItem("Tools/Material Shader Change")]
	static void ChangeShader()
	{
        Object[] objects = Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets);
		
		foreach(Material mat in objects)
		{
			mat.shader = Shader.Find(@"Mobile/Particles/Alpha Blended");
//			mat.shader = Shader.Find(@"Unlit/Unlit");
		}
	}


    [MenuItem("Tools/Texture/Find DXT")]
    static void FindDXT()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        List<Object> list = new List<Object>();
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            bool bad = false;
            if (textureImporter.textureFormat == TextureImporterFormat.DXT1 ||
                textureImporter.textureFormat == TextureImporterFormat.DXT5)
                bad = true;

            int size;
            TextureImporterFormat format;
            textureImporter.GetPlatformTextureSettings(RuntimePlatform.Android.ToString(), out size, out format);

            if (format == TextureImporterFormat.DXT1 ||
                format == TextureImporterFormat.DXT5)
                bad = true;

            if (bad)
            {
                list.Add(texture);
            }
        }

        Selection.objects = list.ToArray();
    }
}
