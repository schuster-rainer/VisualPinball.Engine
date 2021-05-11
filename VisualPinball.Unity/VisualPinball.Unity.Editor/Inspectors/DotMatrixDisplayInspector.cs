﻿// Visual Pinball Engine
// Copyright (C) 2021 freezy and VPE Team
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

// ReSharper disable CheckNamespace
// ReSharper disable CompareOfFloatsByEqualityOperator

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VisualPinball.Unity.Editor
{
	[CustomEditor(typeof(DotMatrixDisplayAuthoring)), CanEditMultipleObjects]
	public class DotMatrixDisplayInspector : DisplayInspector
	{
		[NonSerialized] private DotMatrixDisplayAuthoring _mb;
		[NonSerialized] private DotMatrixDisplayAuthoring[] _mbs;

		private void OnEnable()
		{
			_mb = target as DotMatrixDisplayAuthoring;
			base.OnEnable();
		}

		public override void OnInspectorGUI()
		{
			_mb.Id = EditorGUILayout.TextField("Id", _mb.Id);
			_mbs = targets.Select(t => t as DotMatrixDisplayAuthoring).ToArray();

			base.OnInspectorGUI();

			var width = EditorGUILayout.IntField("Columns", _mb.Width);
			if (width != _mb.Width) {
				_mb.Width = width;
			}

			var height = EditorGUILayout.IntField("Rows", _mb.Height);
			if (height != _mb.Height) {
				_mb.Height = height;
			}

			var dotSize = EditorGUILayout.Slider("Dot Size", _mb.DotSize, 0.05f, 1.0f);
			if (dotSize != _mb.DotSize) {
				RecordUndo("Change DMD Dot Size", this);
				foreach (var mb in _mbs) {
					mb.DotSize = dotSize;
				}
			}

			var sharpness = EditorGUILayout.Slider("Dot Sharpness", _mb.Sharpness, 0.0f, 0.5f);
			if (sharpness != _mb.Sharpness) {
				RecordUndo("Change DMD Dot Sharpness", this);
				foreach (var mb in _mbs) {
					mb.Sharpness = sharpness;
				}
			}
		}

		[MenuItem("GameObject/Visual Pinball/Dot Matrix Display", false, 12)]
		private static void CreateDmdGameObject()
		{
			var go = new GameObject {
				name = "Dot Matrix Display"
			};

			if (Selection.activeGameObject != null) {
				go.transform.parent = Selection.activeGameObject.transform;

			} else {
				go.transform.localPosition = new Vector3(0f, 0.36f, 1.1f);
				go.transform.localScale = new Vector3(GameObjectScale, GameObjectScale, GameObjectScale);
			}

			var dmd = go.AddComponent<DotMatrixDisplayAuthoring>();
			dmd.UpdateDimensions(128, 32);

		}
	}
}
