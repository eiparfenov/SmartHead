﻿/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Oculus.Interaction.PoseDetection.Editor.Model;
using System;
using System.Linq;
using UnityEditor;

namespace Oculus.Interaction.PoseDetection.Editor
{
    [Flags]
    public enum TransformFeatureFlags
    {
        WristUp = 1 << 0,
        WristDown = 1 << 1,
        PalmDown = 1 << 2,
        PalmUp = 1 << 3,
        PalmTowardsFace = 1 << 4,
        PalmAwayFromFace = 1 << 5,
        FingersUp = 1 << 6,
        FingersDown = 1 << 7,
        PinchClear = 1 << 8
    }

    [CustomPropertyDrawer(typeof(TransformFeatureConfigList))]
    public class TransformConfigEditor : FeatureListPropertyDrawer
    {
        protected override Enum FlagsToEnum(uint flags)
        {
            return (TransformFeatureFlags)flags;
        }

        protected override uint EnumToFlags(Enum flags)
        {
            return (uint)(TransformFeatureFlags)flags;
        }

        protected override string FeatureToString(int featureIdx)
        {
            return ((TransformFeature)featureIdx).ToString();
        }

        protected override FeatureStateDescription[] GetStatesForFeature(int featureIdx)
        {
            return TransformFeatureProperties.FeatureDescriptions[(TransformFeature)featureIdx].FeatureStates;
        }

        protected override FeatureConfigList CreateModel(SerializedProperty property)
        {
            var descriptions = TransformFeatureProperties.FeatureDescriptions
                .ToDictionary(p => (int)p.Key, p => p.Value);

            return new FeatureConfigList(property.FindPropertyRelative("_values"),
                descriptions);
        }
    }
}
