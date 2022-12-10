#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PogapogaEditor.RightClick
{
    public class SkinnedMeshBoneObjectSelector : MonoBehaviour
    {
        /// <summary>
        /// SkinnedMeshRendereのbonesの保持しているGameObjectを選択します
        /// </summary>
        [MenuItem("GameObject/PogapogaTools/SkinnedMeshRendereBoneObjectSelector", validate = false, priority = int.MinValue)]
        public static void SelectSkinnedMeshRendereBoneObject()
        {
            List<GameObject> _gameObjects = new List<GameObject>();
            GameObject[] _selectGameObject = Selection.gameObjects;
            
            foreach (GameObject _gameObject in _selectGameObject)
            {
                SkinnedMeshRenderer _skinnedMeshRenderer;
                Transform[] _bones;
                int _nullCount = 0;

                _skinnedMeshRenderer = _gameObject.GetComponent<SkinnedMeshRenderer>();
                // SkinnedMeshRendereがない場合にはスキップする
                if (_skinnedMeshRenderer == null)
                {
                    continue;
                }

                Debug.Log($"SkinnedMeshRendere({_skinnedMeshRenderer.name})の保持しているGameObjectを選択します");
                _bones = _skinnedMeshRenderer.bones;

                // Bonesがnullのときはスキップする
                if (_bones == null)
                {
                    Debug.LogWarning($"{_skinnedMeshRenderer.name}のbonesがnullです");
                    continue;
                }
                // Bonesが0のとき
                if (_bones.Length == 0)
                {
                    Debug.Log($"{_skinnedMeshRenderer.name}のboneが0です");
                    continue;
                }

                for (int i = 0; i < _bones.Length; i++)
                {
                    if (_bones[i] == null)
                    {
                        _nullCount++;
                        continue;
                    }
                    _gameObjects.Add(_bones[i].gameObject);
                    Debug.Log($"{_bones[i].name}");
                }
                if (_nullCount > 0)
                {
                    Debug.LogWarning($"{_skinnedMeshRenderer.name}のboneの内{_nullCount}の項目がnullになっています");
                } 
                Debug.Log($"{_skinnedMeshRenderer.name} bones:{_bones.Length}");
                Selection.objects = _gameObjects.ToArray();
            }
        }

        /// <summary>
        /// ツールの実行が可能かどうかの判定
        /// </summary>
        [MenuItem("GameObject/PogapogaTools/SkinnedMeshRendereBoneObjectSelector", validate = true, priority = int.MinValue)]
        static bool Validate()
        {
            foreach (GameObject _gameObject in Selection.gameObjects)
            {
                SkinnedMeshRenderer _skinnedMeshRenderer = _gameObject.GetComponent<SkinnedMeshRenderer>();
                if (_skinnedMeshRenderer != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
#endif