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

namespace VisualPinball.Engine.VPT.HitTarget
{
	public interface IHitTargetData
	{
		bool IsDropTarget { get; }
		bool IsLegacy { get; }
		float RotZ { get; }
		int TargetType { get; }
		float ScaleX { get; }
		float ScaleY { get; }
		float ScaleZ { get; }
		float PositionX { get; }
		float PositionY { get; }
		float PositionZ { get; }
	}
}