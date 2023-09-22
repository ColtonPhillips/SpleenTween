using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public int ActiveTweensCount;

        public List<Tween> Tweens = new();

        public static Spleen Instance;
       
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

        // reset all lists when scene loads to prevent tweens carrying over
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

        #region Tween Control
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

        #endregion

        #region Static Tween Functions

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
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Pos(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.position, to, duration, easing, (val) =>
            {
                target.transform.position = val;
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddPos(GameObject target, Vector3 increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween PosX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(val, target.transform.position.y, target.transform.position.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PosX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.x, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(val, target.transform.position.y, target.transform.position.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddPosX(GameObject target, float increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween PosY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, val, target.transform.position.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PosY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.y, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, val, target.transform.position.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddPosY(GameObject target, float increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween PosZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, val);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PosZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.z, to, duration, easing, (val) =>
            {
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, val);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddPosZ(GameObject target, float increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Local Position
        public static Tween LocPos(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localPosition = val;
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocPos(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localPosition, to, duration, easing, (val) =>
            {
                target.transform.localPosition = val;
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocPos(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.localPosition;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += val - from;
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localPosition;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocPosX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localPosition = new Vector3(val, target.transform.localPosition.y, target.transform.localPosition.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocPosX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localPosition.x, to, duration, easing, (val) =>
            {
                target.transform.localPosition = new Vector3(val, target.transform.localPosition.y, target.transform.localPosition.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocPosX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localPosition.x;

            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += new Vector3(val - from, 0, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localPosition.x;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocPosY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, val, target.transform.localPosition.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocPosY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localPosition.y, to, duration, easing, (val) =>
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, val, target.transform.localPosition.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocPosY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localPosition.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += new Vector3(0, val - from, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localPosition.y;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocPosZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, val);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocPosZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localPosition.z, to, duration, easing, (val) =>
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, val);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocPosZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localPosition.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += new Vector3(0, 0, val - from);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localPosition.z;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

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
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Scale(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localScale, to, duration, easing, (val) =>
            {
                target.transform.localScale = val;
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddScale(GameObject target, Vector3 increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween ScaleX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(val, target.transform.localScale.y, target.transform.localScale.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.x, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(val, target.transform.localScale.y, target.transform.localScale.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddScaleX(GameObject target, float increment, float duration, Ease easing)
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
            
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween ScaleY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, val, target.transform.localScale.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.y, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, val, target.transform.localScale.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddScaleY(GameObject target, float increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween ScaleZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, val);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.z, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, val);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddScaleZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(0, 0, val - from);
                from = val;
            });
            tween.StopIfNull(target);

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
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Rot(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.rotation.eulerAngles, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = val;
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddRot(GameObject target, Vector3 increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween RotX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(val, target.transform.rotation.y, target.transform.rotation.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween RotX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.x, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(val, target.transform.rotation.y, target.transform.rotation.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddRotX(GameObject target, float increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween RotY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, val, target.transform.rotation.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween RotY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.y, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, val, target.transform.rotation.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddRotY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.StopIfNull(target);

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
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween RotZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.z, to, duration, easing, (val) =>
            {
                target.transform.eulerAngles = new Vector3(target.transform.rotation.x, target.transform.rotation.y, val);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddRotZ(GameObject target, float increment, float duration, Ease easing)
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

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Local Rotation
        public static Tween LocRot(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = val;
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocRot(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localEulerAngles, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = val;
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocRot(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.localEulerAngles;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += val - from;
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localEulerAngles;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocRotX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = new Vector3(val, target.transform.localRotation.y, target.transform.localRotation.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocRotX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localEulerAngles.x, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = new Vector3(val, target.transform.localRotation.y, target.transform.localRotation.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocRotX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localEulerAngles.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += new Vector3(val - from, 0, 0);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localEulerAngles.x;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocRotY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, val, target.transform.localRotation.z);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocRotY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localEulerAngles.y, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, val, target.transform.localRotation.z);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocRotY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localEulerAngles.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.StopIfNull(target);

            tween._onStart += () =>
            {
                from = target.transform.localEulerAngles.y;
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

        public static Tween LocRotZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, target.transform.localRotation.y, val);
            });
            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween LocRotZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localEulerAngles.z, to, duration, easing, (val) =>
            {
                target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, target.transform.localRotation.y, val);
            });

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddLocRotZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localEulerAngles.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += new Vector3(0, 0, val - from);
                from = val;
            });

            tween._onStart += () =>
            {
                from = target.transform.localEulerAngles.z;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(target);

            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Audio
        public static Tween Vol(AudioSource source, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                source.volume = val;
            });
            tween.StopIfNull(source.gameObject);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Vol(AudioSource source, float to, float duration, Ease easing)
        {
            Value tween = new(source.volume, to, duration, easing, (val) =>
            {
                source.volume = val;
            });
            tween.StopIfNull(source.gameObject);

            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween AddVol(AudioSource source, float increment, float duration, Ease easing)
        {
            float from = source.volume;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                source.volume += val - from;
                from = val;
            });

            tween._onStart += () =>
            {
                from = source.volume;
                tween._from = from;
                tween._to = from + increment;

                if (tween._loopType == LoopType.Yoyo)
                {
                    increment = -increment;
                }
            };

            tween.StopIfNull(source.gameObject);

            Instance.Tweens.Add(tween);
            return tween;
        }


        #endregion

        #endregion
    }
}

