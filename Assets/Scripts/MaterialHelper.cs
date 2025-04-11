using UnityEngine;
using UnityEngine.Rendering;

namespace CharlieCares.FruitMerge
{
    public class MaterialHelper : MonoBehaviour
    {
        public GameObject DebugGameObject;
        [Range(0f, 1f)]
        public float DebugOpacity = 0.5f;
        [ContextMenu("Debug Set GameObject Opacity")]
        public void DebugSetGameObjectOpacity()
        {
            SetGameObjectOpacity(DebugGameObject, DebugOpacity);
        }

        public static void SetMaterialOpacity(Material mat, float alpha)
        {
            if (mat.HasProperty("_Surface"))
            {
                mat.SetFloat("_Surface", 1); // 0 = Opaque, 1 = Transparent
                mat.SetOverrideTag("RenderType", "Transparent");
                mat.renderQueue = (int)RenderQueue.Transparent;

                // Enable blending
                mat.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                mat.DisableKeyword("_SURFACE_TYPE_OPAQUE");

                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
        }

        public static void SetRendererOpacity(Renderer renderer, float alpha)
        {
            foreach (var mat in renderer.materials)
            {
                SetMaterialOpacity(mat, alpha);
            }
        }

        public static void SetGameObjectOpacity(GameObject go, float alpha, bool includeChildren = true)
        {
            if (includeChildren)
            {
                foreach (var renderer in go.GetComponentsInChildren<Renderer>())
                    SetRendererOpacity(renderer, alpha);
            }
            else
            {
                if (go.TryGetComponent<Renderer>(out var renderer))
                    SetRendererOpacity(renderer, alpha);
            }
        }
    }
}