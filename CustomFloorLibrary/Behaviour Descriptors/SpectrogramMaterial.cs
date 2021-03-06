using UnityEngine;

namespace CustomFloorPlugin
{
    public class SpectrogramMaterial : MonoBehaviour
    {
        [Header("The Array property (uniform float arrayName[64])")]
        public string PropertyName;
        [Header("The global intensity (float valueName)")]
        public string AveragePropertyName;

        // ----

        private Renderer renderer;
        private dynamic spectrogramData;

        public void setData(dynamic newData)
        {
            spectrogramData = newData;
        }

        void Start()
        {
            renderer = gameObject.GetComponent<Renderer>();
        }

        void Update()
        {
            if (spectrogramData != null && renderer != null)
            {
                float average = 0.0f;
                for (int i = 0; i < 64; i++)
                {
                    average += spectrogramData.ProcessedSamples[i];
                }
                average /= 64.0f;

                foreach (Material mat in renderer.materials)
                {
                    mat.SetFloatArray(PropertyName, spectrogramData.ProcessedSamples);
                    mat.SetFloat(AveragePropertyName, average);
                }
            }
        }
    }
}
