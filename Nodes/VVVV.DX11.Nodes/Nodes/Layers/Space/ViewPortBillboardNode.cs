﻿#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;

using FeralTic;
using FeralTic.DX11;
using VVVV.DX11;
using FeralTic.DX11.Resources;
using SlimDX;
using SlimDX.Direct3D11;
#endregion usings

namespace VVVV.DX11.Nodes
{
    [PluginInfo(Name = "ViewportBillBoard", Category = "DX11.Layer", Version = "Advanced", Author = "vux")]
    public class ViewportBillBoardNode : AbstractDX11LayerSpaceNode
    {
        [Input("Transform In", IsSingle = true, Visibility = PinVisibility.OnlyInspector)]
        protected ISpread<Matrix> FTransformIn;

        [Input("Viewport", IsSingle = true)]
        protected ISpread<Viewport> vp;

        [Input("Double Scale", IsSingle = true, Order = 50)]
        protected ISpread<bool> FDoubleScale;

        [Input("Top Left", IsSingle = true, Order = 51)]
        protected ISpread<bool> FTopLeft;

        protected override void UpdateSettings(DX11RenderSettings settings)
        {
            float f = this.FDoubleScale[0] ? 2.0f : 1.0f;

            float w = vp[0].Width;
            float h = vp[0].Height;


            settings.View = Matrix.Identity;
            settings.Projection = Matrix.Scaling(f / w, f / h, 1.0f) * FTransformIn[0];

            if (FTopLeft[0])
            {
                float tx = w * 0.5f;
                float ty = h * 0.5f;
                settings.Projection = Matrix.Translation(-tx, ty, 0.0f) * settings.Projection;
            }

            settings.ViewProjection = settings.Projection;
        }
    }
}
