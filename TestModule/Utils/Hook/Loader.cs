using UnityEngine;

namespace SPTarkov.TestModule.Utils.Hook
{
    public class Loader<T> where T : MonoBehaviour
    {
        public static GameObject HookObject
        {
            get
            {
                GameObject result = GameObject.Find("TestModule Instance");

                if (result == null)
                {
                    result = new GameObject("TestModule Instance");
                    Object.DontDestroyOnLoad(result);
                }

                return result;
            }
        }

        public static T Load()
        {
            return HookObject.GetOrAddComponent<T>();
        }
    }
}
