using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public int ActiveTweensCount;

        readonly List<Tween> Tweens = new();

        static Spleen Instance;
       
        #region Initialization
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        private void OnEnable() => SceneManager.activeSceneChanged += StopAllTweens;
        private void OnDisable() => SceneManager.activeSceneChanged -= StopAllTweens;

        //reset all lists when scene loads to prevent tweens continuing after scene load
        void StopAllTweens(Scene s1, Scene s2)
        {
            Tweens.Clear();
        }
        //create a new instance when the game starts
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Instance = new GameObject("SpleenTweenManager").AddComponent<Spleen>();
        }
        #endregion

        private void Update()
        {
            RunTweens();

            ActiveTweensCount = Tweens.Count;
        }

        #region TweenRunners
        void RunTweens()
        {
            for (int i = 0; i < Tweens.Count; i++)
            {
                Tween tween = Tweens[i];
                if (!tween.Tweening())
                {
                    Tweens.RemoveAt(i);
                }
            }
        }

        public static void StopTween(Tween tween)
        {
            Instance.Tweens.Remove(tween);
        }
        public static void StopAll()
        {
            Instance.Tweens.Clear();
        }
        public static void StopTweens(GameObject target)
        {
            for (int i = Instance.Tweens.Count - 1; i >= 0; i--)
            {
                Tween tween = Instance.Tweens[i];
                if (tween._target == target)
                    Instance.Tweens.RemoveAt(i);
            }
        }

        static void AddNullCheck(Tween tween, GameObject obj)
        {
            tween._nullCheck += () =>
            {
                if (obj == null)
                    tween._targetIsNull = true;
            };
            tween._target = obj;
        }

        #endregion

        #region TweenFunctions

        #region Value
        public static Tween Value(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            Value tween = new(from, to, duration, easing, onUpdate);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Value3(Vector3 from, Vector3 to, float duration, Ease easing, Action<Vector3> onUpdate)
        {
            Value3 tween = new(from, to, duration, easing, onUpdate);
            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Position
        public static Tween Pos(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = val;
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Pos(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.position, to, duration, easing, (val) =>
            {
                target.transform.position = val;
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementPos(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.position;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += val - from;
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.position;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween PosX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(val, target.transform.position.y, target.transform.position.z);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PosX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.x, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(val, target.transform.position.y, target.transform.position.z);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementPosX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.position.x;

            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += new Vector3(val - from, 0, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.position.x;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween PosY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, val, target.transform.position.z);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PosY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.y, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, val, target.transform.position.z);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementPosY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.position.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += new Vector3(0, val - from, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.position.y;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween PosZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, val);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PosZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.z, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, val);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementPosZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.position.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += new Vector3(0, 0, val - from);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.position.z;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Scale
        public static Tween Scale(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = val;
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Scale(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localScale, to, duration, easing, (val) =>
            {
                target.transform.localScale = val;
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementScale(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.localScale;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += val - from;
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localScale;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween ScaleX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(val, target.transform.localScale.y, target.transform.localScale.z);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.x, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(val, target.transform.localScale.y, target.transform.localScale.z);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementScaleX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(val - from, 0, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localScale.x;
                tween._from = from;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };
            
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween ScaleY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, val, target.transform.localScale.z);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.y, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, val, target.transform.localScale.z);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementScaleY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(0, val - from, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localScale.y;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween ScaleZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, val);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.z, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, val);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementScaleZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(0, 0, val - from);
                from = val;
            });
            AddNullCheck(tween, target);

            tween._onStart += () =>
            {
                from = target.transform.localScale.z;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Rotation
        public static Tween Rot(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = val;
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Rot(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.rotation.eulerAngles, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = val;
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementRot(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.rotation.eulerAngles;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += val - from;
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.eulerAngles;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween RotX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(val, target.transform.rotation.y, target.transform.rotation.z);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween RotX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.x, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(val, target.transform.rotation.y, target.transform.rotation.z);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementRotX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(val - from, 0, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.eulerAngles.x;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween RotY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, val, target.transform.rotation.z);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween RotY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.y, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, val, target.transform.rotation.z);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementRotY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(0, val - from, 0);
                from = val;
            });
            AddNullCheck(tween, target);

            tween._onStart += () =>
            {
                from = target.transform.eulerAngles.y;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween RotZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, target.transform.rotation.y, val);
            });
            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween RotZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.z, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, target.transform.rotation.y, val);
            });

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween IncrementRotZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(0, 0, val - from);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.eulerAngles.z;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            AddNullCheck(tween, target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #endregion
    }
}

