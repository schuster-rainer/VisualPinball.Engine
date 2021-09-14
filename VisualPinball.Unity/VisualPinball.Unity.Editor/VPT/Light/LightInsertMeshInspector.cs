// Visual Pinball Engine
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

// ReSharper disable AssignmentInConditionalExpression

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VisualPinball.Engine.Math;
using VisualPinball.Engine.VPT.Light;

namespace VisualPinball.Unity.Editor
{
	[CustomEditor(typeof(LightInsertMeshComponent)), CanEditMultipleObjects]
	public class LightInsertMeshInspector : MeshInspector<LightData, LightComponent, LightInsertMeshComponent>, IDragPointsInspector
	{
		private DragPointsInspectorHelper _dragPointsInspectorHelper;

		private SerializedProperty _insertHeightProperty;
		private SerializedProperty _positionZProperty;

		public bool DragPointsActive => true;

		protected override void OnEnable()
		{
			base.OnEnable();

			_dragPointsInspectorHelper = new DragPointsInspectorHelper(MeshComponent.MainComponent, this);
			_dragPointsInspectorHelper.OnEnable();

			_insertHeightProperty = serializedObject.FindProperty(nameof(LightInsertMeshComponent.InsertHeight));
			_positionZProperty = serializedObject.FindProperty(nameof(LightInsertMeshComponent.PositionZ));
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_dragPointsInspectorHelper.OnDisable();
		}

		public override void OnInspectorGUI()
		{
			if (HasErrors()) {
				return;
			}

			BeginEditing();

			OnPreInspectorGUI();

			PropertyField(_insertHeightProperty, rebuildMesh: true);
			PropertyField(_positionZProperty, updateTransforms: true);

			_dragPointsInspectorHelper.OnInspectorGUI(this);

			base.OnInspectorGUI();

			EndEditing();
		}

		private void OnSceneGUI()
		{
			_dragPointsInspectorHelper.OnSceneGUI(this);
		}

		#region Dragpoint Tooling

		public DragPointData[] DragPoints { get => MeshComponent.DragPoints; set => MeshComponent.DragPoints = value; }
		public Vector3 EditableOffset => new Vector3(-MeshComponent.MainComponent.Position.x, -MeshComponent.MainComponent.Position.y, MeshComponent.PositionZ);
		public Vector3 GetDragPointOffset(float ratio) => Vector3.zero;
		public bool PointsAreLooping => true;
		public IEnumerable<DragPointExposure> DragPointExposition => new[] { DragPointExposure.Smooth, DragPointExposure.Texture };
		public ItemDataTransformType HandleType => ItemDataTransformType.TwoD;

		#endregion
	}
}