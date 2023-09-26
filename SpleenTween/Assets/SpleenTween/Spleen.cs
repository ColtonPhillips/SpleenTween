using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public static void StopTween(Tween tween)
        {
            SpleenTweenManager.Instance.Tweens.Remove(tween);
        }
        public static void StopAll()
        {
            SpleenTweenManager.Instance.Tweens.Clear();
        }
        public static void StopTweens(GameObject target)
        {
            for (int i = SpleenTweenManager.Instance.Tweens.Count - 1; i >= 0; i--)
            {
                Tween tween = SpleenTweenManager.Instance.Tweens[i];
                if (tween.target == target)
                    SpleenTweenManager.Instance.Tweens.RemoveAt(i);
            }
        }
        public static void AddTween(Tween tween)
        {
            SpleenTweenManager.Instance.Tweens.Add(tween);
        }

        #region Static Tween Functions

        #region Value
        public static Tween Value(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            Value tween = new(from, to, duration, easing, onUpdate);
            SpleenTweenManager.Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Value3(Vector3 from, Vector3 to, float duration, Ease easing, Action<Vector3> onUpdate)
        {
            Value3 tween = new(from, to, duration, easing, onUpdate);
            SpleenTweenManager.Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Position
        public static Tween Pos(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) => target.transform.position = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween Pos(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.position, to, duration, easing, (val) => target.transform.position = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddPos(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.position;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += val - from;
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.position;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween PosX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.position = new Vector3(val, target.transform.position.y, target.transform.position.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween PosX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.x, to, duration, easing, 
                (val) => target.transform.position = new Vector3(val, target.transform.position.y, target.transform.position.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddPosX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.position.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += new Vector3(val - from, 0, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if(tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.position.x;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween PosY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.position = new Vector3(target.transform.position.x, val, target.transform.position.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween PosY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.y, to, duration, easing, 
                (val) => target.transform.position = new Vector3(target.transform.position.x, val, target.transform.position.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddPosY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.position.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.position.y;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween PosZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween PosZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.position.z, to, duration, easing, 
                (val) => target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddPosZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.position.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.position += new Vector3(0, 0, val - from);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.position.z;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        #endregion

        #region Local Position
        public static Tween LocPos(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) => target.transform.localPosition = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocPos(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localPosition, to, duration, easing, (val) => target.transform.localPosition = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocPos(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.localPosition;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += val - from;
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localPosition;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            tween.StopIfNull(target);
            SpleenTweenManager.Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocPosX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localPosition = new Vector3(val, target.transform.localPosition.y, target.transform.localPosition.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocPosX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localPosition.x, to, duration, easing,
                (val) => target.transform.localPosition = new Vector3(val, target.transform.localPosition.y, target.transform.localPosition.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocPosX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localPosition.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += new Vector3(val - from, 0, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localPosition.x;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            tween.StopIfNull(target);
            SpleenTweenManager.Instance.Tweens.Add(tween);
            return tween;
        }

        public static Tween LocPosY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localPosition = new Vector3(target.transform.localPosition.x, val, target.transform.localPosition.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocPosY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localPosition.y, to, duration, easing, 
                (val) => target.transform.localPosition = new Vector3(target.transform.localPosition.x, val, target.transform.localPosition.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocPosY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localPosition.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localPosition.y;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween LocPosZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocPosZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localPosition.z, to, duration, easing, 
                (val) => target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocPosZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localPosition.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localPosition += new Vector3(0, 0, val - from);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localPosition.z;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        #endregion

        #region Scale
        public static Tween Scale(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) => target.transform.localScale = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween Scale(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localScale, to, duration, easing, (val) => target.transform.localScale = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddScale(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.localScale;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += val - from;
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localScale;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween ScaleX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(val, target.transform.localScale.y, target.transform.localScale.z);
            });
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween ScaleX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.x, to, duration, easing, (val) =>
            {
                target.transform.localScale = new Vector3(val, target.transform.localScale.y, target.transform.localScale.z);
            });
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddScaleX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(val - from, 0, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localScale.x;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween ScaleY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localScale = new Vector3(target.transform.localScale.x, val, target.transform.localScale.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween ScaleY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.y, to, duration, easing, 
                (val) => target.transform.localScale = new Vector3(target.transform.localScale.x, val, target.transform.localScale.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddScaleY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localScale.y;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween ScaleZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween ScaleZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localScale.z, to, duration, easing, 
                (val) => target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddScaleZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localScale.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localScale += new Vector3(0, 0, val - from);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localScale.z;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        #endregion

        #region Rotation
        public static Tween Rot(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) => target.transform.eulerAngles = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween Rot(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.rotation.eulerAngles, to, duration, easing, (val) => target.transform.eulerAngles = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddRot(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.eulerAngles;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += val - from;
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.eulerAngles;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween RotX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.eulerAngles = new Vector3(val, target.transform.rotation.y, target.transform.rotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween RotX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.x, to, duration, easing, 
                (val) => target.transform.eulerAngles = new Vector3(val, target.transform.rotation.y, target.transform.rotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddRotX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(val - from, 0, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.eulerAngles.x;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween RotY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.eulerAngles = new Vector3(target.transform.rotation.x, val, target.transform.rotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween RotY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.y, to, duration, easing, 
                (val) => target.transform.eulerAngles = new Vector3(target.transform.rotation.x, val, target.transform.rotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddRotY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.eulerAngles.y;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween RotZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing,
                (val) => target.transform.eulerAngles = new Vector3(target.transform.rotation.x, target.transform.rotation.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween RotZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.rotation.eulerAngles.z, to, duration, easing, 
                (val) => target.transform.eulerAngles = new Vector3(target.transform.rotation.x, target.transform.rotation.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddRotZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.eulerAngles.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.eulerAngles += new Vector3(0, 0, val - from);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.eulerAngles.z;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        #endregion

        #region Local Rotation
        public static Tween LocRot(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(from, to, duration, easing, (val) => target.transform.localEulerAngles = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocRot(GameObject target, Vector3 to, float duration, Ease easing)
        {
            Value3 tween = new(target.transform.localEulerAngles, to, duration, easing, (val) => target.transform.localEulerAngles = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocRot(GameObject target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.transform.localEulerAngles;
            Value3 tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += val - from;
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localEulerAngles;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween LocRotX(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localEulerAngles = new Vector3(val, target.transform.localRotation.y, target.transform.localRotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocRotX(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localEulerAngles.x, to, duration, easing, 
                (val) => target.transform.localEulerAngles = new Vector3(val, target.transform.localRotation.y, target.transform.localRotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocRotX(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localEulerAngles.x;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += new Vector3(val - from, 0, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localEulerAngles.x;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween LocRotY(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, val, target.transform.localRotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocRotY(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localEulerAngles.y, to, duration, easing, 
                (val) => target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, val, target.transform.localRotation.z));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocRotY(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localEulerAngles.y;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += new Vector3(0, val - from, 0);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localEulerAngles.y;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }

        public static Tween LocRotZ(GameObject target, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, 
                (val) => target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, target.transform.localRotation.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween LocRotZ(GameObject target, float to, float duration, Ease easing)
        {
            Value tween = new(target.transform.localEulerAngles.z, to, duration, easing, 
                (val) => target.transform.localEulerAngles = new Vector3(target.transform.localRotation.x, target.transform.localRotation.y, val));
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        public static Tween AddLocRotZ(GameObject target, float increment, float duration, Ease easing)
        {
            float from = target.transform.localEulerAngles.z;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                target.transform.localEulerAngles += new Vector3(0, 0, val - from);
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = target.transform.localEulerAngles.z;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, target);
        }
        #endregion

        #region Audio
        public static Tween Vol(AudioSource source, float from, float to, float duration, Ease easing)
        {
            Value tween = new(from, to, duration, easing, (val) => source.volume = val);
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, source.gameObject);
        }
        public static Tween Vol(AudioSource source, float to, float duration, Ease easing)
        {
            Value tween = new(source.volume, to, duration, easing, (val) =>
            {
                source.volume = val;
            });
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, source.gameObject);
        }
        public static Tween AddVol(AudioSource source, float increment, float duration, Ease easing)
        {
            float from = source.volume;
            Value tween = new(from, increment, duration, easing, (val) =>
            {
                source.volume += val - from;
                from = val;
            });
            tween.onStart += () =>
            {
                if (tween.loopType != LoopType.Rewind)
                {
                    from = source.volume;
                    tween.from = from;
                    tween.to = from + increment;
                }

                if (tween.loopType == LoopType.Yoyo)
                    increment = -increment;
            };
            return SpleenTweenManager.StartTweenAndAddNullCheck(tween, source.gameObject);
        }

        #endregion

        #endregion
    }
}

